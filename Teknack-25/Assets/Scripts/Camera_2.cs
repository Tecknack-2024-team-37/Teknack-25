using System.Collections.Generic;
using UnityEngine;

public class Camera_2 : MonoBehaviour
{
    public List<GameObject> targets; // List of cars to follow
    public float smoothSpeed = 5f; // Speed for smoothing camera movement
    public float minZoom = 10f; // Minimum zoom level
    public float maxZoom = 15f; // Maximum zoom level
    public float zoomLimiter = 50f; // Factor to adjust zoom based on distance
    public Vector3 offset; // Offset to keep the cars in the frame

    private Camera cam;

    void Start()
    {
        if (targets == null || targets.Count == 0)
        {
            Debug.LogError("Camera_2: No targets assigned!");
            return;
        }
        
        cam = GetComponent<Camera>();
        if (cam == null)
        {
            Debug.LogError("Camera_2: No Camera component found!");
            return;
        }
    }

    void LateUpdate()
    {
        if (targets.Count == 0)
            return;

        MoveCamera();
        AdjustZoom();
    }

    void MoveCamera()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;
        transform.position = Vector3.Lerp(transform.position, newPosition, smoothSpeed * Time.deltaTime);
    }

    void AdjustZoom()
    {
        float greatestDistance = GetGreatestDistance();
        float newZoom = Mathf.Lerp(maxZoom, minZoom, greatestDistance / zoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].transform.position;
        }
        
        Bounds bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.center;
    }

    float GetGreatestDistance()
    {
        Bounds bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 1; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.size.x > bounds.size.z ? bounds.size.x : bounds.size.z;
    }
}
