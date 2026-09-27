using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(Rigidbody2D))]
public class Pong_bar : MonoBehaviour
{
    public bool isHumanPlayer = true;
    public float speed = 10f;
    public Transform ball;

    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0f;

        // Ensure the collider on the paddle matches settings expected by the ball
        Collider2D col = GetComponent<Collider2D>();
        col.isTrigger = true; // Set to true since your ball uses triggers
    }

    private void Start()
    {
        // Automatically enforce the "Paddle" tag so the ball's OnTriggerEnter2D finds it
        if (!CompareTag("Paddle"))
        {
            gameObject.tag = "Paddle";
        }
    }

    private void Update()
    {
        float direction = 0f;

        if (isHumanPlayer)
        {
            direction = Input.GetAxisRaw("Vertical");
        }
        else if (ball != null)
        {
            direction = Mathf.Sign(ball.position.y - transform.position.y);
        }

        Vector3 position = transform.position;
        position.y = Mathf.Clamp(
            position.y + direction * speed * Time.deltaTime,
            minY,
            maxY
        );

        transform.position = position;
    }
}