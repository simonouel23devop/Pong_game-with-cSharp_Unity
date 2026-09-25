using UnityEngine;

public class Pong_bar : MonoBehaviour
{
    public bool isHumanPlayer = true;
    public float speed = 10f;
    public Transform ball;

    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

    void Update()
    {
        float direction = 0f;

        if (isHumanPlayer)
        {
            // Supports W/S, Up/Down, and any other configured Vertical input.
            direction = Input.GetAxisRaw("Vertical");
        }
        else if (ball != null)
        {
            direction = Mathf.Sign(ball.position.y - transform.position.y);
        }

        if (transform.position.y > 5) // Check if the bar is out of bounds
        {
            transform.position = new Vector3(transform.position.x, 5, 0); // Reset position to center if out of bounds
        }
        if (transform.position.y < -5) // Check if the bar is out of bounds
        {
            transform.position = new Vector3(transform.position.x, 5, 0); // Reset position to center if out of bounds
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