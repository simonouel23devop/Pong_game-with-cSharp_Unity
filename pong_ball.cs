using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
public class pong_ball : MonoBehaviour
{
    public float speed = 5f;

    private Vector2 dir;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        GetComponent<Collider2D>().isTrigger = true;
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
            return;
        }

        if (nextPosition.x > 8.5f && dir.x > 0f)
        {
            rb.position = Vector2.zero;
            dir.x = -Mathf.Abs(dir.x);
            return;
        }

        rb.MovePosition(nextPosition);
    }

    private void OnTriggerEnter2D(Collider2D other)
{
    Pong_bar paddle = other.GetComponentInParent<Pong_bar>();

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