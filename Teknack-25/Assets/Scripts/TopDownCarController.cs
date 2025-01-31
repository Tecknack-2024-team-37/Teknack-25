using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    
    //local variables
    float accelerationInput = 0;
    float steeringInput = 0;
    float rotationAngle =0;

    void Awake(){
        carRigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate(){
        ApplyEngineForce();
        ApplySteering();
    }

    void ApplyEngineForce(){
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //apply force and pushes the car forward
        carRigidbody2D.AddForce(engineForceVector,ForceMode2D.Force);
    }

    void ApplySteering(){
        //update rotation angle
        rotationAngle -= steeringInput * turnFactor;

        //apply steering by rotating the car object 
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector(Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y;
    }  
}
