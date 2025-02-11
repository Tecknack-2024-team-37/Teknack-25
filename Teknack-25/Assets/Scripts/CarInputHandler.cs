using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler : MonoBehaviour
{
    // Components
    TopDownCarController topDownCarController;
    public float jumpScale;
    public bool isUIInput = false;

    Vector2 inputVector = Vector2.zero;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUIInput){

        }
        else{
                Vector2 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");

        if (topDownCarController != null)
        {
            topDownCarController.SetInputVector(inputVector);
        }

        if(Input.GetButtonDown("Jump"))
            topDownCarController.Jump(jumpScale, 0.0f);

        }

        
    }
    public void SetInput(Vector2 newInput){
        inputVector = newInput;
    }

}
