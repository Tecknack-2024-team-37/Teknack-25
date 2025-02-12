using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarUIInputHandler : MonoBehaviour
{
    CarInputHandler1 playerCarInputHandler;

    Vector2 inputVector = Vector2.zero;

   private void Awake(){
         CarInputHandler1[] carinputHandlers = FindObjectsOfType<CarInputHandler1>();

         foreach(CarInputHandler1 carInputHandler in carinputHandlers){
            if(carInputHandler.isUIInput){
                playerCarInputHandler = carInputHandler;
                break;
            }
         }


    }
    // Start is called before the first frame update
    void Start()
    {
        
    }


    public void OnAcceleratePress(){
            inputVector.y = 1.0f;
            playerCarInputHandler.SetInput(inputVector);
    }

    public void OnBreakPress(){
        inputVector.y = -1.0f;
         playerCarInputHandler.SetInput(inputVector);
    }

    public void OnAccelerateBreakRelease(){
        inputVector.y =  0.0f;
         playerCarInputHandler.SetInput(inputVector);
    }

    public void OnSteerLeftPress(){
        inputVector.x = -1.0f;
         playerCarInputHandler.SetInput(inputVector);
    }

   public void OnSteerRightPress(){
    inputVector.x = 1.0f; // Correcting input for right turn
    playerCarInputHandler.SetInput(inputVector);
}


    public void OnSteerRelease(){
        inputVector.y = 0.0f;
         playerCarInputHandler.SetInput(inputVector);
    }
   
}


