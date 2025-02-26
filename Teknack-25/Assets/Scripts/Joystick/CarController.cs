
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class CarController : MonoBehaviour
// {
//     [Header("Boost Settings")]
//     public float boostMultiplier = 2.0f;
//     public float maxBoost = 1.0f;
//     private float currentBoost = 0f;
//     private bool isBoosting = false;

//     public Slider boostBar;
//     public Button boostButton;

//     public Text driftText; // Assign in Inspector

//     [Header("Drift Settings")]
//     public GameObject driftTextPrefab; // Assign in Unity Inspector
//     public Canvas gameCanvas; // Assign UI Canvas in Inspector
//     private float perfectDriftTime = 0f;
//     private float requiredDriftTime = 1.2f; // Reduced drift time requirement
//     private float driftTextCooldown = 1.5f; // Cooldown to prevent spam
//     private bool canShowDriftText = true;

//     [Header("Car settings")]
//     public float driftFactor = 0.95f;
//     public float accelerationFactor = 30.0f;
//     public float turnFactor = 3.5f;
//     public float maxSpeed = 20;

//     public FixedJoystick Joystick;
//     private Vector2 inputVector = Vector2.zero;

//     private float accelerationInput = 0;
//     private float steeringInput = 0;
//     private float rotationAngle = 0;
//     private float velocityVsUp = 0;

//     private Rigidbody2D carRigidbody2D;
//     public static bool PointerDown = false;

//     private void Awake()
//     {
//         carRigidbody2D = GetComponent<Rigidbody2D>();

//         if (boostButton != null)
//         {
//             boostButton.onClick.AddListener(ActivateBoost);
//         }
//     }

//     private void Update()
//     {
//         inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical);

//         accelerationInput = inputVector.magnitude;
//         steeringInput = inputVector.x;

//         if (inputVector.sqrMagnitude > 0.01f)
//         {
//             float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
//             rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
//         }

//         // Check if drifting
//         if (IsDrifting())
//         {
//             ShowDriftText();
//         }
//         else
//         {
//             HideDriftText();
//         }
//     }

//     private void FixedUpdate()
//     {
//         if (PointerDown)
//         {
//             carRigidbody2D.velocity = Vector2.zero;
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

//         float speedLimit = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;

//         if (velocityVsUp > speedLimit) return;
//         if (carRigidbody2D.velocity.sqrMagnitude > speedLimit * speedLimit) return;

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

//     public void CollectBoost(float amount)
//     {
//         currentBoost = Mathf.Clamp(currentBoost + amount, 0, maxBoost);
//         UpdateBoostUI();
//     }

//     public void ActivateBoost()
//     {
//         if (currentBoost > 0 && !isBoosting)
//         {
//             isBoosting = true;
//             StartCoroutine(BoostRoutine());
//         }
//     }

//     private IEnumerator BoostRoutine()
//     {
//         float boostTime = 3.0f;
//         float elapsed = 0f;

//         while (elapsed < boostTime && currentBoost > 0)
//         {
//             elapsed += Time.deltaTime;
//             currentBoost -= Time.deltaTime / boostTime * maxBoost;
//             UpdateBoostUI();
//             yield return null;
//         }

//         isBoosting = false;
//     }

//     private void UpdateBoostUI()
//     {
//         if (boostBar != null)
//         {
//             boostBar.value = currentBoost / maxBoost;
//         }
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

//     private bool IsDrifting()
//     {
//         float lateralVelocity = Vector2.Dot(transform.right, carRigidbody2D.velocity);
//         return Mathf.Abs(lateralVelocity) > 2.0f && Mathf.Abs(steeringInput) > 0.3f;
//     }

//     public float GetVelocityMagnitude()
//     {
//         return carRigidbody2D.velocity.magnitude;
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

//     IEnumerator FadeDriftText()
//     {
//         driftText.enabled = true;
//         Color originalColor = driftText.color;

//         yield return new WaitForSeconds(0.5f); // Show for 0.5 sec

//         for (float t = 1; t > 0; t -= Time.deltaTime)
//         {
//             driftText.color = new Color(originalColor.r, originalColor.g, originalColor.b, t);
//             yield return null;
//         }

//         driftText.enabled = false;
//         driftText.color = originalColor; // Reset color
//     }

//     void ShowDriftText()
//     {
//         StopAllCoroutines();
//         StartCoroutine(FadeDriftText());
//     }

//     void HideDriftText()
//     {
//         driftText.enabled = false;
//     }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarController : MonoBehaviour
{
    [Header("Boost Settings")]
    public float boostMultiplier = 2.0f;
    public float maxBoost = 1.0f;
    private float currentBoost = 0f;
    private bool isBoosting = false;

    public Slider boostBar;
    public Button boostButton;

    public Text driftText; // Assign in Inspector

    [Header("Drift Settings")]
    public GameObject driftTextPrefab; // Assign in Unity Inspector
    public Canvas gameCanvas; // Assign UI Canvas in Inspector
    private float perfectDriftTime = 0f;
    private float requiredDriftTime = 1.2f; // Time needed for perfect drift
    private float driftCooldown = 1.5f; // Cooldown to prevent spam
    private bool canShowDriftText = true;

    [Header("Car settings")]
    public float driftFactor = 0.9f; // Adjusted for moderate drifting
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 20f; // Adjusted for a more natural drift initiation

    public FixedJoystick Joystick;
    private Vector2 inputVector = Vector2.zero;

    private float accelerationInput = 0;
    private float steeringInput = 0;
    private float rotationAngle = 0;
    private float velocityVsUp = 0;

    private Rigidbody2D carRigidbody2D;
    public static bool PointerDown = false;

    private void Awake()
    {
        carRigidbody2D = GetComponent<Rigidbody2D>();

        if (boostButton != null)
        {
            boostButton.onClick.AddListener(ActivateBoost);
        }
    }

    private void Update()
    {
        inputVector = new Vector2(-Joystick.Horizontal, Joystick.Vertical);
        accelerationInput = inputVector.magnitude;
        steeringInput = inputVector.x;

        if (inputVector.sqrMagnitude > 0.01f)
        {
            float targetAngle = Mathf.Atan2(inputVector.x, inputVector.y) * Mathf.Rad2Deg;
            rotationAngle = Mathf.LerpAngle(rotationAngle, targetAngle, Time.deltaTime * 5f);
        }

        // Check drift conditions
        if (IsDrifting())
        {
            perfectDriftTime += Time.deltaTime;
            if (perfectDriftTime >= requiredDriftTime && canShowDriftText)
            {
                StartCoroutine(ShowDriftText());
                canShowDriftText = false; // Prevent spam
                Invoke(nameof(ResetDriftCooldown), driftCooldown);
            }
        }
        else
        {
            perfectDriftTime = 0f;
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

        float speedLimit = isBoosting ? maxSpeed * boostMultiplier : maxSpeed;
        if (velocityVsUp > speedLimit) return;
        if (carRigidbody2D.velocity.sqrMagnitude > speedLimit * speedLimit) return;

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

    private bool IsDrifting()
    {
        float lateralVelocity = Vector2.Dot(transform.right, carRigidbody2D.velocity);
        float speedThreshold = 5.0f; // Moderate speed required for drift
        return Mathf.Abs(lateralVelocity) > 1.5f && Mathf.Abs(steeringInput) > 0.3f && GetVelocityMagnitude() > speedThreshold;
    }

    public float GetVelocityMagnitude()
    {
        return carRigidbody2D.velocity.magnitude;
    }

    private IEnumerator ShowDriftText()
    {
        driftText.enabled = true;
        Color originalColor = driftText.color;

        yield return new WaitForSeconds(0.5f); // Show for 0.5 sec

        for (float t = 1; t > 0; t -= Time.deltaTime)
        {
            driftText.color = new Color(originalColor.r, originalColor.g, originalColor.b, t);
            yield return null;
        }

        driftText.enabled = false;
        driftText.color = originalColor; // Reset color
    }

    private void ResetDriftCooldown()
    {
        canShowDriftText = true;
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
}
