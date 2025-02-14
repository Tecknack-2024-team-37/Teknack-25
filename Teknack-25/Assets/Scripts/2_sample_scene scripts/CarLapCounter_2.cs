using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CarLapCounter_2 : MonoBehaviour
{
    public Text carPositionText;
    int passedCheckPointNumber = 0;
    float timeAtLastPassedCheckPoint = 0;
    int numberOfPassedCheckpoints = 0;

    int lapsCompleted = 0;
    const int lapsToComplete = 2;

    bool isRaceCompleted = false;

    int carPosition = 0;

    bool isHideRoutineRunning = false;
    float hideUIDElayTime;

    //Events
    public event Action<CarLapCounter_2>onPassCheckPoint;

    public void SetCarPosition(int position)
    {
        carPosition = position;
    }

    public int GetNumberOfCheckpointsPassed()
    {
        return numberOfPassedCheckpoints;
    }

    public float GetTimeAtLastCheckPoint()
    {
        return timeAtLastPassedCheckPoint;
    }

    IEnumerator ShowPositionCO(float delayUntilHidePosition)
    {
        hideUIDElayTime += delayUntilHidePosition;

        carPositionText.text = carPosition.ToString();

        carPositionText.gameObject.SetActive(true);

        if(!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;

            yield return new WaitForSeconds(hideUIDElayTime);
            carPositionText.gameObject.SetActive(false);

            isHideRoutineRunning = false;
        }
    }

    void onTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.CompareTag("CheckPoint"))
        {
            if(isRaceCompleted)
                return;

            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            //make sure car is following correct path
            if(passedCheckPointNumber + 1 == checkPoint.checkPointNumber)
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;

                numberOfPassedCheckpoints++;

                //store time at checkpoint
                timeAtLastPassedCheckPoint = Time.time;

                if(checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;

                    if(lapsCompleted >= lapsToComplete)
                        isRaceCompleted = true;
                }

                //Invoke the PassedCheckPoint Event
                onPassCheckPoint?.Invoke(this);

                //Now show the car position
                if(isRaceCompleted)
                    StartCoroutine(ShowPositionCO(100));
                else StartCoroutine(ShowPositionCO(1.5f));


            }

        }
    }
}
