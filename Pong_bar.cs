using UnityEngine;

public class Pong_bar : MonoBehaviour
{
    public bool isHumanPlayer = true;
    public float speed = 10f;
    public Transform ball;

    [SerializeField] private float minY = -4.5f;
    [SerializeField] private float maxY = 4.5f;

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