// using UnityEngine;

// public class CarController3 : MonoBehaviour
// {
//     public float acceleration = 10f;
//     public float maxSpeed = 5f;
//     public float turnSpeed = 200f;
//     public float deadzone = 0.2f; // New: Deadzone to ignore small inputs

//     private Rigidbody2D rb;
//     public Joystick joystick;

//     void Start()
//     {
//         rb = GetComponent<Rigidbody2D>();
//         rb.drag = 2f; // Optional: Adds natural slowdown
//     }

//     void FixedUpdate()
//     {
//         // Get joystick input with deadzone
//         float moveInput = Mathf.Abs(joystick.Vertical) > deadzone ? joystick.Vertical : 0f;
//         float turnInput = Mathf.Abs(joystick.Horizontal) > deadzone ? joystick.Horizontal : 0f;

//         // Apply ACCELERATION (not direct velocity)
//         Vector2 force = transform.up * moveInput * acceleration;
//         rb.AddForce(force);

//         // Clamp speed to maxSpeed
//         if (rb.velocity.magnitude > maxSpeed)
//         {
//             rb.velocity = rb.velocity.normalized * maxSpeed;
//         }

//         // ALWAYS allow rotation (even when stationary)
//         rb.rotation += -turnInput * turnSpeed * Time.fixedDeltaTime;
//     }
// }
using UnityEngine;

public class CarController3 : MonoBehaviour
{
    [Header("Car Settings")]
    public float acceleration = 10f;
    public float maxSpeed = 5f;
    public float turnSpeed = 200f;
    public float driftFactor = 0.9f;
    public float deadzone = 0.2f;

    private Rigidbody2D rb;
    public Joystick joystick;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 2f; // Natural slowdown
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    private void ApplyEngineForce()
    {
        float moveInput = Mathf.Abs(joystick.Vertical) > deadzone ? joystick.Vertical : 0f;

        // Apply acceleration force
        Vector2 force = transform.up * moveInput * acceleration;
        rb.AddForce(force);

        // Clamp speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Smooth Deceleration when no input
        if (moveInput == 0)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, Vector2.zero, Time.fixedDeltaTime * 3);
        }
    }

    private void ApplySteering()
    {
        float turnInput = Mathf.Abs(joystick.Horizontal) > deadzone ? joystick.Horizontal : 0f;
        float velocityMagnitude = GetVelocityMagnitude();

        // Adjust steering based on speed
        float turnFactor = Mathf.Lerp(1.0f, 0.5f, velocityMagnitude / maxSpeed);
        rb.rotation += -turnInput * turnSpeed * turnFactor * Time.fixedDeltaTime;
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        // Apply drift factor
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    private float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

    public bool IsTyreScreeching(out float lateralVelocity)
    {
        lateralVelocity = Mathf.Abs(Vector2.Dot(rb.velocity, transform.right));
        return lateralVelocity > 1.5f; // Screeching when sideways movement is high
    }
}
