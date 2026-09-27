using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class pong_ball : MonoBehaviour
{
    public float speed = 5f;
    public Text score;
    public int[] scores = new int[2];

    private Vector2 dir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        // Make the collider solid (not a trigger)
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void Start()
    {
        dir = Vector2.one.normalized;
    }

    private void FixedUpdate()
    {
        Vector2 nextPosition = rb.position + dir * speed * Time.fixedDeltaTime;

        if ((nextPosition.y < -3.9f && dir.y < 0f) ||
            (nextPosition.y > 5.9f && dir.y > 0f))
        {
            dir.y *= -1f;
        }

        if (nextPosition.x < -8.5f && dir.x < 0f)
        {
            rb.position = Vector2.zero;
            dir.x = Mathf.Abs(dir.x);
            scores[0]++;
            score.text = scores[1] + " - " + scores[0];
            return;
        }

        if (nextPosition.x > 8.5f && dir.x > 0f)
        {
            rb.position = Vector2.zero;
            dir.x = -Mathf.Abs(dir.x);
            scores[1]++;
            score.text = scores[1] + " - " + scores[0];
            return;
        }

        rb.MovePosition(nextPosition);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Pong_bar paddle = collision.collider.GetComponentInParent<Pong_bar>();

        if (paddle == null || !paddle.CompareTag("Paddle"))
        {
            return;
        }

        if ((paddle.isHumanPlayer && dir.x < 0f) ||
            (!paddle.isHumanPlayer && dir.x > 0f))
        {
            dir.x *= -1f;
        }
    }
}