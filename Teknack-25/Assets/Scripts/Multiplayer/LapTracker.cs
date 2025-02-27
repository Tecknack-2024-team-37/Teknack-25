using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class LapTracker : MonoBehaviour
{
    public int totalLaps = 4; // Set total laps for the race
    private int currentLap = 0;
    private float raceStartTime;
    private bool raceCompleted = false;

    public string playerName; // Assign in Inspector (Car1, Car2, etc.)

    private GameManager2 gameManager;

    void Start()
    {
        raceStartTime = Time.time;
        gameManager = FindObjectOfType<GameManager2>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LapTrigger") && !raceCompleted) // LapTrigger should be placed on track
        {
            currentLap++;
            Debug.Log(playerName + " completed lap: " + currentLap);

            if (currentLap >= totalLaps)
            {
                raceCompleted = true;
                float raceTime = Time.time - raceStartTime;
                gameManager.PlayerFinishedRace(playerName, raceTime);
            }
        }
    }
}
