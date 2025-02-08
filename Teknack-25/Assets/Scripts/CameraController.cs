// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     public Transform player; // Changed to Transform for better efficiency
//     private Vector3 offset;

//     void Start()
//     {
//         offset = transform.position - player.position;
//     }

//     void LateUpdate()
//     {
//         // Follow the car's position
//         transform.position = player.position + offset;

//         // Rotate the camera to match the car's rotation
//         transform.rotation = Quaternion.Euler(0, 0, player.eulerAngles.z);
//     }
// }




using System.Collections;
using System.Collections.Generic;
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