using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeshCarController : MonoBehaviour
{
    public float acceleration = 12.0f;  // Higher acceleration for faster speed gain
    public float maxSpeed = 18.0f;      // Increased max speed
    public float turnSpeed = 180.0f;    // Smoother turning
    public float drag = 0.5f;           // Reduces speed gradually when no input

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on this GameObject.");
        }
        rb.drag = drag;  // Apply natural deceleration
    }

    void Update()
    {
        // Get movement input from keyboard (W/S for forward/backward, A/D for left/right)
        moveInput = Input.GetAxis("Vertical"); // W = 1, S = -1
        turnInput = Input.GetAxis("Horizontal"); // A = -1, D = 1
    }

    void FixedUpdate()
    {
        // Apply force for acceleration instead of setting velocity directly
        if (moveInput != 0)
        {
            rb.AddForce(transform.up * moveInput * acceleration, ForceMode2D.Force);
        }

        // Limit max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Allow turning even when stationary
        float rotationAmount = -turnInput * turnSpeed * Time.fixedDeltaTime;
        rb.MoveRotation(rb.rotation + rotationAmount);
    }
}


