using UnityEngine;

public class AI : MonoBehaviour
{
    public Transform pong_ball;
    public float speed = 10f;

    private void FixedUpdate()
    {
        if (pong_ball == null)
        {
            return;
        }

        Vector3 position = transform.position;
        position.y = Mathf.MoveTowards(
            position.y,
            pong_ball.position.y,
            speed * Time.fixedDeltaTime
        );
        transform.position = position;
    }
}
