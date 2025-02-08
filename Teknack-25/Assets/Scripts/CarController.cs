using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public FixedJoystick Joystick;
    Rigidbody2D rb;
    Vector2 move;
    public float moveSpeed = 5f;
    
    // Rear drifting factor: set to a value below 1 (e.g. 0.95) to reduce lateral grip
    public float rearDriftFactor = 0.95f;
    // Assign a transform (an empty GameObject positioned at the rear axle) in the inspector
    public Transform rearWheel;

    public static bool PointerDown = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        move.x = Joystick.Horizontal;
        move.y = Joystick.Vertical;

        // Rotate the car so it faces the direction of input (if there is any)
        if (move.sqrMagnitude > 0.001f)
        {
            float angle = Mathf.Atan2(move.x, move.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, -angle);
        }
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        
        // Apply the basic movement from joystick input
        Vector2 targetVelocity = move * moveSpeed;
        rb.velocity = targetVelocity;

        // --- REAR WHEEL DRIFTING EFFECT ---
        // Ensure we have a rearWheel transform assigned
        if (rearWheel != null)
        {
            // Get the velocity at the rear wheel's position
            Vector2 rearWheelVelocity = rb.GetPointVelocity(rearWheel.position);
            // Compute the lateral component at the rear wheel along the car's right vector
            Vector2 rearLateralVelocity = transform.right * Vector2.Dot(rearWheelVelocity, transform.right);
            // Scale the lateral velocity by the drift factor:
            // If rearDriftFactor < 1, this reduces (damps) the lateral speed.
            Vector2 desiredRearLateralVelocity = rearLateralVelocity * rearDriftFactor;
            // Calculate the difference between desired and current lateral velocities
            Vector2 lateralVelocityChange = desiredRearLateralVelocity - rearLateralVelocity;
            // Apply a corrective impulse at the rear wheel's position to reduce lateral grip
            rb.AddForceAtPosition(lateralVelocityChange * rb.mass, rearWheel.position, ForceMode2D.Impulse);
        }
    }
}
