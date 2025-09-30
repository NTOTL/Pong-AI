using System;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Rigidbody2D rb;
    public int playerId;
    public float moveSpeed = 2f;

    private Vector2 startPosition;    

    private void OnEnable()
    {
        startPosition = transform.position;
        GameManager.Instance.OnReset += ResetPaddlePosition; // 
    }

    private void ResetPaddlePosition()
    {
        transform.position = startPosition;
    }

    private void Update()
    {
        if (playerId == 2 && GameManager.Instance.IsPlayerVsAI())
        {
            // call AI move
            MoveAI();
        }
        else
        {
            float value = GetAxisValue();
            MovePaddle(value);
        }
    }

    private void MoveAI()
    {
        Debug.Log($"Move AI with id {playerId}");
    }

    private float GetAxisValue()
    {
        float axisValue = 0f;
        switch (playerId)
        {
            case 1:
                axisValue = Input.GetAxis("PaddlePlayer1");
                break;
            case 2:
                axisValue = Input.GetAxis("PaddlePlayer2");
                break;
        }

        return axisValue;
    }

    private void MovePaddle(float value)
    {
        Vector2 velocity = rb.linearVelocity;
        velocity.y = value * moveSpeed;
        rb.linearVelocity = velocity;
    }
}
