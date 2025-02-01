using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCarController : MonoBehaviour
{
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;

    //Local Variables
    float accelerationInput = 0;
    float steeringInput = 0;

    float rotationAngle = 0;

    //components  
    Rigidbody2D carRigidbody2D;

    //awake is called when the script instance is been loaded
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
        KillOrthogonalVelocity();
        ApplySteering();
    }

    void ApplyEngineForce(){
        //create a force  for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car 
        carRigidbody2D.AddForce(engineForceVector, ForceMode2D.Force);
    }


    void KillOrthogonalVelocity(){
        Vector2 forwardVelocity = transform.up * Vector2.Dot(carRigidbody2D.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(carRigidbody2D.velocity, transform.right);

        carRigidbody2D.velocity = forwardVelocity + rightVelocity * driftFactor;
    }
    void ApplySteering(){

        //Limit the cars ability to turn when moving slowly
        float minSpeedBeforeAllowTurningFactor = (carRigidbody2D.velocity.magnitude / 8);
        minSpeedBeforeAllowTurningFactor = Mathf.Clamp01(minSpeedBeforeAllowTurningFactor);

        //Update the rotation angle based on input
        rotationAngle -= steeringInput * turnFactor * minSpeedBeforeAllowTurningFactor;

        //Apply steering by rotating the car object 
        carRigidbody2D.MoveRotation(rotationAngle);
    }

    public void SetInputVector (Vector2 inputVector){
        steeringInput = inputVector.x;
        accelerationInput = inputVector.y; 
    }
}
