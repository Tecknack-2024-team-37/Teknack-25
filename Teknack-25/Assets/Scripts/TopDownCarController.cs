using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;

    [Header("Sprites")]
    public SpriteRenderer carSpriteRenderer;
    public SpriteRenderer carShadowRenderer;

    [Header("Jumping")]
    public AnimationCurve jumpCurve;
    //public ParticleSystem landingParticleSystem;

    // Local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle = 0;
    public float velocityVsUp = 0;

    bool isJumping = false;

    // Components
    Rigidbody2D carRigidbody2D;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0) return;
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0) return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0) return;

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

    void ApplySteering()
    {
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        // Reverse the steering direction when moving backward
        float reverseMultiplier = (velocityVsUp < 0) ? -1f : 1f;

        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor * reverseMultiplier;

        carRigidbody2D.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        float modifiedDriftFactor = (accelerationInput == 0) ? driftFactor * 0.5f : driftFactor;

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * modifiedDriftFactor;
    }

    float GetLateralVelocity()
    {
        //return how fast the car id mving
        return Vector2.Dot(transform.right, carRigidbody2D.velocity);
    }

    public bool IsTyreScreeching(out float lateralVelocity, out bool isBraking)
    {
        lateralVelocity = GetLateralVelocity();
        isBraking = false;

        if (accelerationInput < 0 && velocityVsUp >0)
        {
            isBraking = true;
            return true;
        }

        if (Mathf.Abs(GetLateralVelocity()) > 2.0f)
        {
            return true;
        }

        return false;
    }

    public void SetInputVector(Vector2 inputVector)
    {
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    public void Jump(float jumpHeightScale, float jumpPushScale)
    {
        if(!isJumping)
            StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
    }

    private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
    {
        isJumping = true;

        float jumpStartTime = Time.time;
        float jumpDuration = 2;
        Vector3 originalScale = carSpriteRenderer.transform.localScale;
        var originalScaleShadow = carShadowRenderer.transform.localPosition;
        var originalCarPosition = carShadowRenderer.transform.localPosition;
        while(isJumping)
        {
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);
            var localscale1 = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
            carSpriteRenderer.transform.localScale = new Vector3(0.2785205f,0.2785205f,2f);
//
            //carShadowRenderer.transform.localScale = new Vector3(0.2785205f,0.2785205f,2f);
            carSpriteRenderer.transform.localScale = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
            carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            if(jumpCompletedPercentage == 1.0f)
                break;

            yield return null;
        }

       carSpriteRenderer.transform.localScale = originalScale;
        //carSpriteRenderer.transform.localPosition = originalCarPosition;
        carShadowRenderer.transform.localPosition = originalScaleShadow;
        carShadowRenderer.transform.localScale = originalScale;

        isJumping = false;
    }
}




