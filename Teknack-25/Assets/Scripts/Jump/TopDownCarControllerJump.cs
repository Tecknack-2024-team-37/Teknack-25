using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarControllerJump : MonoBehaviour
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
    Collider2D carCollider;

    void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carCollider = GetComponentInChildren<Collider2D>();
    }

    void FixedUpdate()
    {
        ApplyEngineForce();
        ApplySteering();
        KillOrthogonalVelocity();
    }

    void ApplyEngineForce()
    {
        if (isJumping && accelerationInput < 0)
            accelerationInput = 0;

        velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;
        if (velocityVsUp < -maxSpeed * 0.5f && accelerationInput < 0) 
            return;
        if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed && accelerationInput > 0 && !isJumping)
            return;

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

        if(isJumping)
            return  false;

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
        float jumpDuration = carRigidbody2D.velocity.magnitude * 0.1f;

        jumpHeightScale = jumpHeightScale * carRigidbody2D.velocity.magnitude * 0.03f;
        jumpHeightScale = Mathf.Clamp(jumpHeightScale, 0.0f, 1.0f);
        
        //Disable collisions
        //carCollider.enabled = false;

        //Push the object forward as we passed a jump
        carRigidbody2D.AddForce(carRigidbody2D.velocity.normalized * jumpPushScale * 3.14f, ForceMode2D.Impulse);

        Vector3 originalScale = carSpriteRenderer.transform.localScale;
        var originalScaleShadow = carShadowRenderer.transform.localPosition;
        var originalCarPosition = carShadowRenderer.transform.localPosition;
        
        while(isJumping)
        {
            float jumpCompletedPercentage = (Time.time - jumpStartTime) / jumpDuration;
            jumpCompletedPercentage = Mathf.Clamp01(jumpCompletedPercentage);
            var localscale1 = Vector3.one + Vector3.one * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
            carSpriteRenderer.transform.localScale = new Vector3(0.2785205f,0.2785205f,2f);

            //carShadowRenderer.transform.localScale = new Vector3(0.2785205f,0.2785205f,2f);
            carSpriteRenderer.transform.localScale = new Vector3(1, 1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;
            carShadowRenderer.transform.localPosition = new Vector3(1, -1, 0.0f) * 3 * jumpCurve.Evaluate(jumpCompletedPercentage) * jumpHeightScale;

            if(jumpCompletedPercentage == 1.0f)
                break;

            yield return null;
        }

        // //Check if landing is ok or not
        // if (Physics2D.OverlapCircle(transform.position, 1.5f))
        // {
        //     //Something is below the car so we need to jump again
        //     isJumping = false;

        //     //add a small jump and push the car forward a bit
        //     Jump(0.2f, 0.6f);
        // }
        // else 
        // {
         carSpriteRenderer.transform.localScale = originalScale;
         carSpriteRenderer.transform.localPosition = originalCarPosition;
         carShadowRenderer.transform.localPosition = originalScaleShadow;
         carShadowRenderer.transform.localScale = originalScale;

         //We are safe to land, so enable collider
         //carCollider.enabled = true;

         isJumping = false;
        //}
    }

    //Detect jump trigger 

    void OnTriggerEnter2D(Collider2D collider2d)
    {
        if (collider2d.CompareTag("Jump"))
        {
            //Get the jumo data from the jump
            JumpData jumpData = collider2d.GetComponent<JumpData>();
            Jump(jumpData.jumpHeightScale, jumpData.jumpPushScale);
        }
    }
}




