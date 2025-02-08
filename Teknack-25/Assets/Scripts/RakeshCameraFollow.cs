using UnityEngine;

public class RakeshCameraFollow : MonoBehaviour
{ 
    public Transform target;  // Assign this in Inspector (Car)
    public float smoothSpeed = 5f;
    public Vector3 offset;

    void LateUpdate()
    {
        if (target == null)
        {
            Debug.LogError("Camera target not set! Assign your car in the Inspector.");
            return;
        }

        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
    }
}

