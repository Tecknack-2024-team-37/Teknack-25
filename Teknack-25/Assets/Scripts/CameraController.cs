using System.Collections;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; // The car to follow
    public float smoothSpeed = 5f; // Adjust speed for smoother follow
    public float zoomSpeed = 2f; // Speed of zoom effect
    public float zoomAmount = 3f; // How much to zoom in
    public float startDelay = 3f; // Time before camera starts moving

    private Vector3 offset;
    private Camera cam; // Reference to the camera component

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraController: Target is not assigned in the Inspector!");
            return;
        }

        offset = transform.position - target.transform.position;
        cam = GetComponent<Camera>();

        // Start the cinematic camera sequence
        StartCoroutine(CinematicStart());
    }

    IEnumerator CinematicStart()
    {
        // Wait for the start delay (mimicking countdown lights)
        yield return new WaitForSeconds(startDelay);

        // Smoothly zoom in
        float initialSize = cam.orthographicSize;
        float targetSize = initialSize - zoomAmount;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * zoomSpeed;
            cam.orthographicSize = Mathf.Lerp(initialSize, targetSize, t);
            yield return null;
        }

        // Start following the target
        enabled = true;
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
