using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;  // Adjust in Inspector

    void Start()
    {
        // Automatically find the GameObject named "Car"
        GameObject carObject = GameObject.Find("Car");

        if (carObject != null)
        {
            target = carObject.transform;
        }
        else
        {
            Debug.LogError("Car GameObject not found! Make sure the name is correct.");
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
    }
}
