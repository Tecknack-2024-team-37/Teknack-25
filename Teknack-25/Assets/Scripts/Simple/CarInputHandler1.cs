using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputHandler1 : MonoBehaviour
{
    // Components
    TopDownCarController1 topDownCarController;

    public bool isUIInput = false;

    Vector2 inputVector = Vector2.zero;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        topDownCarController = GetComponent<TopDownCarController1>(); 
        
    }

    // Update is called once per frame
    void Update()
    {
        if(isUIInput){

        }
        else{
                 inputVector = Vector2.zero;

            inputVector.x = Input.GetAxis("Horizontal");
        inputVector.y = Input.GetAxis("Vertical");
        
    }
topDownCarController.SetInputVector(inputVector);
}

public void SetInput(Vector2 newInput){
    inputVector = newInput;
}

}
