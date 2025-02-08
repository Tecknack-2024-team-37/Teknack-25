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
    public GameObject player;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

     void LateUpdate()
    {
        transform.position = player.transform.position+offset;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}