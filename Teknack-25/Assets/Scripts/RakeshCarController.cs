using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RakeshCarController : MonoBehaviour
{
    public float acceleration = 20.0f;  // Quick acceleration
    public float maxSpeed = 18.0f;      // Balanced max speed
    public float baseTurnSpeed = 1200.0f; // **Extremely high for super-tight turns**
    public float gripFactor = 0.8f;     // Lower grip for better rotation
    public float turnGripReduction = 0.7f; // More grip loss while turning
    public float drag = 1.2f;           // Helps smooth movement
    public float minTurnSpeed = 0.1f;   // Allows turning even when almost stopped

    private Rigidbody2D rb;
    private float moveInput;
    private float turnInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = drag;
        rb.angularDrag = 2.0f;  // **Less angular drag for easier turning**
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Vertical");  // W/S
        turnInput = Input.GetAxis("Horizontal"); // A/D
    }

    void FixedUpdate()
    {
        // Acceleration logic
        if (moveInput != 0)
        {
            rb.AddForce(transform.up * moveInput * acceleration, ForceMode2D.Force);
        }

        // Limit max speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // **Apply extreme turning power**
        float speedFactor = Mathf.Clamp(rb.velocity.magnitude / maxSpeed, minTurnSpeed, 1.0f);
        float turnSpeed = baseTurnSpeed * speedFactor * (Mathf.Abs(moveInput) > 0.1f ? 1.0f : 1.8f); 

        // Apply even sharper turns
        if (Mathf.Abs(turnInput) > 0.1f)
        {
            rb.AddTorque(-turnInput * turnSpeed * Time.fixedDeltaTime, ForceMode2D.Force);

            // Reduce grip significantly while turning (helps with pivoting)
            rb.velocity *= (1 - turnGripReduction * Time.fixedDeltaTime);
        }

        // Simulate grip (reduce sideways sliding)
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 sideVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);
        rb.velocity = forwardVelocity + sideVelocity * (1 - Time.fixedDeltaTime * gripFactor);
    }
}
