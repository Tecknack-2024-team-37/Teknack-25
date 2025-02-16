using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    // Input
    public FixedJoystick Joystick;
    private Vector2 inputVector = Vector2.zero;

    // Local variables
    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    // Components
    private Rigidbody2D carRigidbody2D;

    public static bool PointerDown = false;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Get joystick input
        inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical); // Inverted horizontal control

        // Set acceleration and steering input
        accelerationInput = inputVector.magnitude; // Always move forward
        steeringInput = inputVector.x;

        // Adjust rotation based on joystick direction
        if (inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
            rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
        }
    }

    private void FixedUpdate()
    {
        if (PointerDown)
        {
            carRigidbody2D.velocity = Vector2.zero;
        }
        else
        {
            ApplyEngineForce();
            ApplySteering();
            KillOrthogonalVelocity();
        }
    }

    private void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed) return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed) return;

        if (accelerationInput == 0)
        {
            carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 5.0f, Time.fixedDeltaTime * 5);
            carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * 5);
        }
        else
        {
            carRigidbody2D.drag = 0.1f;
        }

        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }

    private void ApplySteering()
    {
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    private void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        float modifiedDriftFactor = (accelerationInput == 0) ? driftFactor * 0.5f : driftFactor;
        carRigidbody2D.velocity = forwardVelocity + rightVelocity * modifiedDriftFactor;
    }

   public bool IsTyreScreeching(out float lateralVelocity, out bool isBraking)
{
    lateralVelocity = Vector2.Dot(transform.right, carRigidbody2D.velocity);
    isBraking = false;

    bool isScreeching = Mathf.Abs(lateralVelocity) > 2.0f;

    if (isScreeching)
    {
        Debug.Log("Tires screeching!");
    }

    return isScreeching;
}
public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

}
