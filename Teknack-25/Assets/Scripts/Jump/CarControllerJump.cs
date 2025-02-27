// // using System.Collections;
// // using UnityEngine;
// // using UnityEngine.Tilemaps; 

// // public class CarControllerJump : MonoBehaviour
// // {
// //     [Header("Car settings")]
// //     public float driftFactor = 0.95f;
// //     public float accelerationFactor = 30.0f;
// //     public float turnFactor = 3.5f;
// //     public float maxSpeed = 20;
// //     public float stoppingSpeed = 3f; // New: Controls how fast the car stops

// //     [Header("Sprites")]
// //     public SpriteRenderer carSpriteRenderer;
// //     public SpriteRenderer carShadowRenderer;

// //     [Header("Jumping")]
// //     public AnimationCurve jumpCurve;

// //      // Add these new variables for tilemap jumping
// //     [Header("Tilemap Jumping")]
// //     public float tilemapJumpHeight = 0.5f;  // Default jump height for tilemap jumps
// //     public float tilemapJumpPush = 1.0f;    // Default push force for tilemap jumps

// //     // Input
// //     public FixedJoystick Joystick;
// //     private Vector2 inputVector = Vector2.zero;

// //     // Local variables
// //     private float accelerationInput = 0;
// //     private float steeringInput = 0;
// //     private float rotationAngle = 0;
// //     private float velocityVsUp = 0;
// //     private bool isJumping = false;
// //     private bool shouldStop = false; // New: Track if we should be stopping

// //     // Components
// //     private Rigidbody2D carRigidbody2D;

// //     public static bool PointerDown = false;

// //     private void Awake()
// //     {
// //         carRigidbody2D = GetComponent<Rigidbody2D>();
// //     }

// //     private void Update()
// //     {
// //         // Get joystick input
// //         inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical);

// //         // Set acceleration and steering input
// //         accelerationInput = inputVector.magnitude;
// //         steeringInput = inputVector.x;

// //         // Update shouldStop based on input and jumping state
// //         shouldStop = PointerDown && !isJumping;

// //         // Adjust rotation based on joystick direction
// //         if (inputVector.sqrMagnitude > 0.01f)
// //         {
// //             float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
// //             rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
// //         }
// //     }

// //     private void FixedUpdate()
// //     {
// //         if (shouldStop)
// //         {
// //             // Gradually reduce velocity
// //             carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * stoppingSpeed);
            
// //             // If almost stopped, fully stop
// //             if (carRigidbody2D.velocity.magnitude < 0.1f)
// //             {
// //                 carRigidbody2D.velocity = Vector2.zero;
// //             }
// //         }
// //         else
// //         {
// //             ApplyEngineForce();
// //             ApplySteering();
// //             KillOrthogonalVelocity();
// //         }
// //     }

// //     private void ApplyEngineForce()
// //     {
// //         velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

// //         if (velocityVsUp > maxSpeed) return;
// //         if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed) return;

// //         if (accelerationInput == 0)
// //         {
// //             carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 5.0f, Time.fixedDeltaTime * 5);
// //             carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * 5);
// //         }
// //         else
// //         {
// //             carRigidbody2D.drag = 0.1f;
// //         }

// //         Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
// //         carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
// //     }

// //     private void ApplySteering()
// //     {
// //         carRigidbody2D.MoveRotation(rotationAngle);
// //     }

// //     private void KillOrthogonalVelocity()
// //     {
// //         Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
// //         Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

// //         float modifiedDriftFactor = (accelerationInput == 0) ? driftFactor * 0.5f : driftFactor;
// //         carRigidbody2D.velocity = forwardVelocity + rightVelocity * modifiedDriftFactor;
// //     }

    
// //     private void OnCollisionEnter2D(Collision2D collision)
// //     {
// //         // Handle normal collisions here
// //         // This will be called for any collider that isn't a trigger
// //     }

// //    public void Jump(float jumpHeightScale, float jumpPushScale)
// //     {
// //         if (!isJumping)
// //             StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
// //     }

// //     private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
// //     {
// //         isJumping = true;
// //         float jumpStartTime = Time.time;
// //         float jumpDuration = carRigidbody2D.velocity.magnitude * 0.1f;

// //         // Store initial velocity for maintaining momentum during jump
// //         Vector2 initialVelocity = carRigidbody2D.velocity;

// //         jumpHeightScale = Mathf.Clamp(jumpHeightScale * initialVelocity.magnitude * 0.03f, 0.0f, 1.0f);
// //         carRigidbody2D.AddForce(initialVelocity.normalized * jumpPushScale * 4.0f, ForceMode2D.Impulse);

// //         Vector3 originalScale = carSpriteRenderer.transform.localScale;
// //         Vector3 originalShadowPos = carShadowRenderer.transform.localPosition;

// //         while (isJumping)
// //         {
// //             float jumpProgress = Mathf.Clamp01((Time.time - jumpStartTime) / jumpDuration);
// //             float jumpHeight = jumpCurve.Evaluate(jumpProgress) * jumpHeightScale;
            
// //             carSpriteRenderer.transform.localScale = originalScale + Vector3.one * jumpHeight;
// //             carShadowRenderer.transform.localPosition = originalShadowPos + new Vector3(0, -jumpHeight, 0);

// //             if (jumpProgress >= 1.0f)
// //                 break;

// //             yield return null;
// //         }

// //         carSpriteRenderer.transform.localScale = originalScale;
// //         carShadowRenderer.transform.localPosition = originalShadowPos;
// //         isJumping = false;
// //     }

// //     private void OnTriggerEnter2D(Collider2D collider)
// //     {
// //         // Check for JumpData objects
// //         if (collider.CompareTag("Jump"))
// //         {
// //             // First check if it's a JumpData object
// //             JumpData jumpData = collider.GetComponent<JumpData>();
// //             if (jumpData != null)
// //             {
// //                 Jump(jumpData.jumpHeightScale, jumpData.jumpPushScale);
// //                 return;
// //             }

// //             // If it's not a JumpData object, check if it's a Tilemap
// //             TilemapCollider2D tilemapCollider = collider.GetComponent<TilemapCollider2D>();
// //             if (tilemapCollider != null)
// //             {
// //                 // Use default tilemap jump values
// //                 Jump(tilemapJumpHeight, tilemapJumpPush);
// //             }
// //         }
// //     }

// //     public bool IsTyreScreeching(out float lateralVelocity, out bool isBraking)
// //     {
// //         lateralVelocity = Vector2.Dot(transform.right, carRigidbody2D.velocity);
// //         isBraking = false;

// //         bool isScreeching = Mathf.Abs(lateralVelocity) > 2.0f;

// //         if (isScreeching)
// //         {
// //             Debug.Log("Tires screeching!");
// //         }

// //         return isScreeching;
// //     }
// // }


// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Tilemaps;

// public class CarControllerJump : MonoBehaviour
// {
//     [Header("Car settings")]
//     public float driftFactor = 0.95f;
//     public float accelerationFactor = 30.0f;
//     public float turnFactor = 3.5f;
//     public float maxSpeed = 20;
//     public float stoppingSpeed = 3f;

//     [Header("Sprites")]
//     public SpriteRenderer carSpriteRenderer;
//     public SpriteRenderer carShadowRenderer;

//     [Header("Jumping")]
//     public AnimationCurve jumpCurve;

//     [Header("Tilemap Jumping")]
//     public float tilemapJumpHeight = 0.5f;
//     public float tilemapJumpPush = 1.0f;

//     // Input
//     public FixedJoystick Joystick;
//     private Vector2 inputVector = Vector2.zero;

//     // Local variables
//     private float accelerationInput = 0;
//     private float steeringInput = 0;
//     private float rotationAngle = 0;
//     private float velocityVsUp = 0;
//     private bool isJumping = false;
//     private bool shouldStop = false;

//     // Components
//     private Rigidbody2D carRigidbody2D;
//     private Collider2D carCollider;
//     private List<Collider2D> jumpColliders = new List<Collider2D>();

//     public static bool PointerDown = false;

//     private void Awake()
//     {
//         carRigidbody2D = GetComponent<Rigidbody2D>();
//         carCollider = GetComponent<Collider2D>();
//     }

//     private void Start()
//     {
//         FindJumpColliders();
//     }

//     private void Update()
//     {
//         inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical);
//         accelerationInput = inputVector.magnitude;
//         steeringInput = inputVector.x;

//         shouldStop = PointerDown && !isJumping;

//         if (inputVector.sqrMagnitude > 0.01f)
//         {
//             float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
//             rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
//         }
//     }

//     private void FixedUpdate()
//     {
//         if (shouldStop)
//         {
//             carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * stoppingSpeed);

//             if (carRigidbody2D.velocity.magnitude < 0.1f)
//             {
//                 carRigidbody2D.velocity = Vector2.zero;
//             }
//         }
//         else
//         {
//             ApplyEngineForce();
//             ApplySteering();
//             KillOrthogonalVelocity();
//         }
//     }

//     private void ApplyEngineForce()
//     {
//         velocityVsUp = Vector2.Dot(transform.up, carRigidbody2D.velocity);

//         if (velocityVsUp > maxSpeed) return;
//         if (carRigidbody2D.velocity.sqrMagnitude > maxSpeed * maxSpeed) return;

//         if (accelerationInput == 0)
//         {
//             carRigidbody2D.drag = Mathf.Lerp(carRigidbody2D.drag, 5.0f, Time.fixedDeltaTime * 5);
//             carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * 5);
//         }
//         else
//         {
//             carRigidbody2D.drag = 0.1f;
//         }

//         Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;
//         carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
//     }

//     private void ApplySteering()
//     {
//         carRigidbody2D.MoveRotation(rotationAngle);
//     }

//     private void KillOrthogonalVelocity()
//     {
//         Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
//         Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

//         float modifiedDriftFactor = (accelerationInput == 0) ? driftFactor * 0.5f : driftFactor;
//         carRigidbody2D.velocity = forwardVelocity + rightVelocity * modifiedDriftFactor;
//     }

//     private void OnCollisionEnter2D(Collision2D collision)
//     {
//         // Handle normal collisions here
//     }

//     public void Jump(float jumpHeightScale, float jumpPushScale)
//     {
//         if (!isJumping)
//             StartCoroutine(JumpCo(jumpHeightScale, jumpPushScale));
//     }

//     private IEnumerator JumpCo(float jumpHeightScale, float jumpPushScale)
//     {
//         isJumping = true;
//         float jumpStartTime = Time.time;
//         float jumpDuration = carRigidbody2D.velocity.magnitude * 0.1f;

//         Vector2 initialVelocity = carRigidbody2D.velocity;
//         jumpHeightScale = Mathf.Clamp(jumpHeightScale * initialVelocity.magnitude * 0.03f, 0.0f, 1.0f);
//         carRigidbody2D.AddForce(initialVelocity.normalized * jumpPushScale * 4.0f, ForceMode2D.Impulse);

//         Vector3 originalScale = carSpriteRenderer.transform.localScale;
//         Vector3 originalShadowPos = carShadowRenderer.transform.localPosition;

//         IgnoreJumpColliders(true); // Disable collisions

//         while (isJumping)
//         {
//             float jumpProgress = Mathf.Clamp01((Time.time - jumpStartTime) / jumpDuration);
//             float jumpHeight = jumpCurve.Evaluate(jumpProgress) * jumpHeightScale;
            
//             carSpriteRenderer.transform.localScale = originalScale + Vector3.one * jumpHeight;
//             carShadowRenderer.transform.localPosition = originalShadowPos + new Vector3(0, -jumpHeight, 0);

//             if (jumpProgress >= 1.0f)
//                 break;

//             yield return null;
//         }

//         carSpriteRenderer.transform.localScale = originalScale;
//         carShadowRenderer.transform.localPosition = originalShadowPos;
//         isJumping = false;

//         IgnoreJumpColliders(false); // Re-enable collisions
//     }

//     private void OnTriggerEnter2D(Collider2D collider)
//     {
//         if (collider.CompareTag("Jump"))
//         {
//             JumpData jumpData = collider.GetComponent<JumpData>();
//             if (jumpData != null)
//             {
//                 Jump(jumpData.jumpHeightScale, jumpData.jumpPushScale);
//                 return;
//             }

//             TilemapCollider2D tilemapCollider = collider.GetComponent<TilemapCollider2D>();
//             if (tilemapCollider != null)
//             {
//                 Jump(tilemapJumpHeight, tilemapJumpPush);
//             }
//         }
//     }

//     private void FindJumpColliders()
//     {
//         jumpColliders.Clear();

//         GameObject[] jumpTilemaps = GameObject.FindGameObjectsWithTag("Jump");
//         foreach (GameObject tilemap in jumpTilemaps)
//         {
//             TilemapCollider2D tilemapCollider = tilemap.GetComponent<TilemapCollider2D>();
//             if (tilemapCollider != null)
//             {
//                 jumpColliders.Add(tilemapCollider);
//             }
//         }
//     }

//     private void IgnoreJumpColliders(bool ignore)
//     {
//         foreach (Collider2D tilemapCollider in jumpColliders)
//         {
//             Physics2D.IgnoreCollision(carCollider, tilemapCollider, ignore);
//         }
//     }

//     public bool IsTyreScreeching(out float lateralVelocity, out bool isBraking)
//     {
//         lateralVelocity = Vector2.Dot(transform.right, carRigidbody2D.velocity);
//         isBraking = false;

//         bool isScreeching = Mathf.Abs(lateralVelocity) > 2.0f;

//         if (isScreeching)
//         {
//             Debug.Log("Tires screeching!");
//         }

//         return isScreeching;
//     }
// }





using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class CarControllerJump : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostMultiplier = 2.0f;
    public float maxBoost = 1.0f;
    private float currentBoost = 0f;
    private bool isBoosting = false;

    public Slider boostBar;
    public Button boostButton;

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20;
    public float stoppingSpeed = 3f;

    [Header("Sprites")]
    public SpriteRenderer carSpriteRenderer;
    public SpriteRenderer carShadowRenderer;

    [Header("Jumping")]
    public AnimationCurve jumpCurve;

    [Header("Tilemap Jumping")]
    public float tilemapJumpHeight = 0.5f;
    public float tilemapJumpPush = 1.0f;

    public FixedJoystick Joystick;
    private Vector2 inputVector = Vector2.zero;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;
    private bool isJumping = false;
    private bool shouldStop = false;

    private Rigidbody2D carRigidbody2D;
    private Collider2D carCollider;
    private List<Collider2D> jumpColliders = new List<Collider2D>();

    public static bool PointerDown = false;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();
        carCollider = GetComponent<Collider2D>();

        if (boostButton != null)
        {
            boostButton.onClick.AddListener(ActivateBoost);
        }
    }

    private void Update()
{
    inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical);
    accelerationInput = inputVector.magnitude; // Get acceleration from joystick
    steeringInput = inputVector.x; // Get steering from joystick

    shouldStop = PointerDown && !isJumping;

    if (inputVector.sqrMagnitude > 0.01f)
    {
        float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
        rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
    }
}


    private void FixedUpdate()
    {
        if (shouldStop)
        {
            carRigidbody2D.velocity = Vector2.Lerp(carRigidbody2D.velocity, Vector2.zero, Time.fixedDeltaTime * stoppingSpeed);
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

    float speedLimit = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;
    if (velocityVsUp > speedLimit) return;

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


    public void CollectBoost(float amount)
    {
        currentBoost = Mathf.Clamp(currentBoost + amount, 0, maxBoost);
        UpdateBoostUI();
    }

    public void ActivateBoost()
    {
        if (currentBoost > 0 && !isBoosting)
        {
            isBoosting = true;
            StartCoroutine(BoostRoutine());
        }
    }

    private IEnumerator BoostRoutine()
    {
        float boostTime = 3.0f;
        float elapsed = 0f;

        while (elapsed < boostTime && currentBoost > 0)
        {
            elapsed += Time.deltaTime;
            currentBoost -= Time.deltaTime / boostTime * maxBoost;
            UpdateBoostUI();
            yield return null;
        }

        isBoosting = false;
    }

    private void UpdateBoostUI()
    {
        if (boostBar != null)
        {
            boostBar.value = currentBoost / maxBoost;
        }
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

        return Mathf.Abs(lateralVelocity) > 2.0f;
    }
}
