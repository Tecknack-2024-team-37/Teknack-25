// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CarLapCounter : MonoBehaviour
// {
//     int passedCheckPointNumber =  0;
//     float timeAtLastPassedCheckPoint = 0;
     
//     int numberOfPassedCheckpoints = 0;


//     //Events
//     public event Action<CarLapCounter> OnPassCheckpoint;
//      void OnTriggerEnter2D(Collider2D collider2D){
//          if(collider2D.CompareTag("ChekPoint")){
//             CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

//                 if(passedCheckPointNumber + 1 == checkPoint.checkPointNumber){
//                     passCheckPointNumber = checkPoint.checkPointNumber;
//                     numberOfPassedcheckpoints++;

//                     //Store the time at the checkpoint
//                     timeAtLastPassedCheckPoint = Time.time;

//                     OnPassCheckpoint?.Invoke(this);
//                 }
//          }
//      }
// }


