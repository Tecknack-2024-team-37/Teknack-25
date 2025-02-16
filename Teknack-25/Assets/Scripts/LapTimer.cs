using UnityEngine;
using UnityEngine.UI;

public class LapTimer : MonoBehaviour
{
    public Text lapTimeText;
    public Text bestTimeText;

    private float startTime;
    private float bestTime = Mathf.Infinity;
    private int lapCount = 0;
    private int maxLaps = 2;
    private bool raceFinished = false;

    void Start()
    {
        ResetTimer();
    }

    void Update()
    {
        if (!raceFinished)
        {
            float currentLapTime = Time.time - startTime;
            lapTimeText.text = "Lap Time: " + currentLapTime.ToString("F2") + "s";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FinishLine") && !raceFinished)
        {
            float lapTime = Time.time - startTime; // Get time for this lap
            lapCount++;

            Debug.Log("Lap " + lapCount + " Completed! Time: " + lapTime.ToString("F2") + "s");

            // Check if this lap is the best time
            if (lapTime < bestTime)
            {
                bestTime = lapTime;
                bestTimeText.text = "Best Time: " + bestTime.ToString("F2") + "s";
            }

            // Check if race is finished
            if (lapCount >= maxLaps)
            {
                raceFinished = true;
                lapTimeText.text = "Final Time: " + lapTime.ToString("F2") + "s";
                Debug.Log("Race Finished! Final Time: " + lapTime.ToString("F2") + "s");
            }
            else
            {
                ResetTimer(); // Reset timer for next lap
            }
        }
    }

    private void ResetTimer()
    {
        startTime = Time.time; // Set new start time
        lapTimeText.text = "Lap Time: 0.00s";
    }
}
