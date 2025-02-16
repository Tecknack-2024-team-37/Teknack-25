

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; // The object to follow
    public float smoothSpeed = 5f; // Adjust speed for smoother follow

    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraController: Target is not assigned in the Inspector!");
            return;
        }

        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            Vector3 desiredPosition = target.transform.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        }
    }
}