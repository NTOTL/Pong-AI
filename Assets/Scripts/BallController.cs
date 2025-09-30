using UnityEngine;
using UnityEngine.InputSystem;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;   

    public float maxInitalAngle = 0.6f;
    public float ballSpeed = 5f;
    public float speedMultiplier = 1.1f;

    private float startX = 0f;
    private float maxStartY = 4f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        GameManager.Instance.OnReset += ResetBall; // Receive the notification and fire event handler        
    }

    private void ResetBall()
    {
        ResetBallPosition();
        Serve();
    }

    public void ResetBallPosition()
    {
        float posY = Random.Range(-maxStartY, maxStartY);
        Vector2 position = new Vector2(startX, posY);
        transform.position = position;
    }

    // Serve the ball
    public void Serve()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector2 direction = Vector2.left;
        if (Random.value < 0.5) direction = Vector2.right;
        direction.y = Random.Range(-maxInitalAngle, maxInitalAngle);
        rb.linearVelocity = direction * ballSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {               
        if (collision != null)
        {
            string tag = collision.tag;
            GameManager.Instance.OnScoreZoneReached(tag);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PaddleController paddle = collision.collider.GetComponent<PaddleController>();
        if (paddle)
        {
            rb.linearVelocity *= speedMultiplier;
        }
    }
}
