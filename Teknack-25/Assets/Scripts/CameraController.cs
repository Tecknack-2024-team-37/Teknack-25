using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject target; // The object to follow
    private Vector3 offset;

    void Start()
    {
        if (target == null)
        {
            Debug.LogError("CameraController: Target is not assigned in the Inspector!");
            return; // Prevent further execution
        }

        offset = transform.position - target.transform.position;
    }

    void LateUpdate()
    {
        if (target != null) // Check to prevent null reference errors
        {
            transform.position = target.transform.position + offset;
        }
    }
}
