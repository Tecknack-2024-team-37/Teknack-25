using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PositionHandler_2 : MonoBehaviour
{
    public List<CarLapCounter_2>carLapCounters = new List<CarLapCounter_2>();
    // Start is called before the first frame update
    void Start()
    {
        //Get all car lap counters in the scene
        CarLapCounter_2[] carLapCounterArray = FindObjectsOfType<CarLapCounter_2>();

        //Store lap counters in a list
        carLapCounters = carLapCounterArray.ToList<CarLapCounter_2>();

        //Hookup on the passedcheckpoint
        foreach(CarLapCounter_2 lapCounters in carLapCounters)
            lapCounters.onPassCheckPoint += onPassCheckPoint;
    }

    void onPassCheckPoint(CarLapCounter_2 carLapCounter)
    {
        Debug.Log($"Event: Car {carLapCounter.gameObject.name} passed a checkpoint");

        //sort the cars first based on how many checkpoints they passed
        carLapCounters = carLapCounters.OrderByDescending(s => s.GetNumberOfCheckpointsPassed()).ThenBy(s => s.GetTimeAtLastCheckPoint()).ToList();
        
        //Get car position
        int carPosition = carLapCounters.IndexOf(carLapCounter) + 1;

        carLapCounter.SetCarPosition(carPosition);
    }
}
