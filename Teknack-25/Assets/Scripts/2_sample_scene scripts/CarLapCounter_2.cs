using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using TMPro; // ✅ Import TextMeshPro

public class CarLapCounter_2 : MonoBehaviour
{
    [Header("UI Elements")]
    public TMP_Text carPositionText; // ✅ Change from Text to TMP_Text
    public TMP_Text lapCounterText;
    public Button restartButton;
    public Button mainMenuButton;
    public GameObject GameOverPanel;

    [Header("Race Variables")]
    private int passedCheckPointNumber = 0;
    private float timeAtLastPassedCheckPoint = 0;
    private int numberOfPassedCheckpoints = 0;

    private int lapsCompleted = 0;
    private const int lapsToComplete = 2;

    private bool isRaceCompleted = false;
    private int carPosition = 0;

    private bool isHideRoutineRunning = false;
    private float hideUIDelayTime;

    private float raceStartTime; // ✅ Tracks when the race starts
    private float raceEndTime;   // ✅ Tracks when the race ends
    public TMP_Text timeTakenText; // ✅ Add this to show final time
    public TMP_Text liveTimeText; // ✅ Live updating timer
    public TMP_Text bestTimeText; // ✅ Assign in Inspector
    public TMP_Text finalTimeText; // ✅ This will show the final time at a new position

    public event Action<CarLapCounter_2> onPassCheckPoint;

    void Start()
    {
        raceStartTime = Time.time; // ✅ Capture race start time
        UpdateLapUI();

         // ✅ Load best time from PlayerPrefs
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);
        if (bestTime < float.MaxValue && bestTimeText != null)
        {
        int bestMinutes = Mathf.FloorToInt(bestTime / 60);
        int bestSeconds = Mathf.FloorToInt(bestTime % 60);
        int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

        bestTimeText.text = $"Best Time: {bestMinutes:D2}:{bestSeconds:D2}.{bestMilliseconds:D3}";
        }

        if (liveTimeText == null)
        {
        Debug.LogError("LiveTimeText is NOT assigned in the Inspector!");
        }

        if (timeTakenText == null)
        {
            Debug.LogError("Time Taken Text is NOT assigned in the Inspector!");
        }

        if (lapCounterText == null)
        {
            Debug.LogError("Lap Counter Text is not assigned in the Inspector!");
        }
        UpdateLapUI();

        if (GameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is NOT assigned in the Inspector!");
        }
        else
        {
            GameOverPanel.SetActive(false);
        }

        if (carPositionText == null)
        {
            Debug.LogError("carPositionText (TMP_Text) is NOT assigned in the Inspector!");
        }

        if (restartButton == null || mainMenuButton == null)
        {
            Debug.LogError("RestartButton or MainMenuButton is NOT assigned in the Inspector!");
        }
        else
        {
            restartButton.onClick.AddListener(RestartRace);
            mainMenuButton.onClick.AddListener(GoToMainMenu);
        }
    }

    public void ResetBestTime()
    {
        void ResetBestTime()
        {
        PlayerPrefs.DeleteKey("BestTime");
        PlayerPrefs.Save();
        Debug.Log("Best Time Reset!");

        // ✅ Update UI immediately
        if (bestTimeText != null)
        {
        bestTimeText.text = "Best Time: --:--.---";
        }
        }

    }

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
        if (carPositionText == null)
        {
            Debug.LogError("carPositionText (TMP_Text) is NULL! Assign it in the Inspector.");
            yield break;
        }

        hideUIDelayTime += delayUntilHidePosition;
        carPositionText.text = carPosition.ToString(); // ✅ TMP_Text uses .text like UI Text
        carPositionText.gameObject.SetActive(true);

        if (!isHideRoutineRunning)
        {
            isHideRoutineRunning = true;
            yield return new WaitForSeconds(hideUIDelayTime);
            carPositionText.gameObject.SetActive(false);
            isHideRoutineRunning = false;
        }
    }

    void Update()
    {
        if (!isRaceCompleted) // ✅ Only update time if race is still running
        {
        float elapsedTime = Time.time - raceStartTime;
        UpdateTimeUI(elapsedTime);
        }
    }

    void UpdateTimeUI(float time)
    {
        if (timeTakenText != null)
        {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);
        
        timeTakenText.text = $"Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
        }
    }


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        Debug.Log("Car collided with: " + collider2D.gameObject.name);

        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            if (checkPoint == null)
            {
                Debug.LogError("Checkpoint script is missing on: " + collider2D.gameObject.name);
                return;
            }

            // ✅ Allow first checkpoint to be any checkpoint
            if (passedCheckPointNumber == 0 && numberOfPassedCheckpoints == 0)
            {
                Debug.Log($"First checkpoint detected: {checkPoint.checkPointNumber}");
                passedCheckPointNumber = checkPoint.checkPointNumber;
                numberOfPassedCheckpoints++;
                timeAtLastPassedCheckPoint = Time.time;
                return;
            }

            // ✅ Ensure checkpoints are hit in order
            if (checkPoint.checkPointNumber == ((passedCheckPointNumber % 9) + 1))
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;
                numberOfPassedCheckpoints++;
                timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;
                    Debug.Log("Laps Completed: " + lapsCompleted);
                    UpdateLapUI(); // ✅ Update UI when lap increases

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                        raceEndTime = Time.time; // ✅ Capture race end time
                        float totalTimeTaken = raceEndTime - raceStartTime;
                        ShowFinalTime(totalTimeTaken); // ✅ Display the final time
                        Debug.Log("Race Completed!!!");
                        EndGame();
                    }
                }

                onPassCheckPoint?.Invoke(this);
                StartCoroutine(ShowPositionCO(isRaceCompleted ? 100 : 1.5f));
            }
            else
            {
                Debug.LogWarning($"Wrong checkpoint order! Expected {(passedCheckPointNumber % 9) + 1}, but got {checkPoint.checkPointNumber}");
            }
        }
    }

    void UpdateLapUI()
    {
        if (lapCounterText != null)
        {
            lapCounterText.text = $"Laps: {lapsCompleted}/{lapsToComplete}";
        }
    }

    void ShowFinalTime(float totalTime)
    {
        isRaceCompleted = true; // ✅ Stop the timer when race ends

        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        int milliseconds = Mathf.FloorToInt((totalTime * 1000) % 1000);

        timeTakenText.text = $"Final Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
        string finalTimeString = $"Final Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";

        // ✅ Compare with the stored best time and update if necessary
        float bestTime = PlayerPrefs.GetFloat("BestTime", float.MaxValue);

        // ✅ Hide live timer UI
        if (liveTimeText != null)
        {
            liveTimeText.gameObject.SetActive(false);
        }

        // ✅ Show final time in a different UI element
        if (finalTimeText != null)
        {
        finalTimeText.text = finalTimeString;
        finalTimeText.gameObject.SetActive(true);
        }
        else
        {
        Debug.LogError("finalTimeText is NOT assigned in the Inspector!");
        }
        if (totalTime < bestTime)
        {
            PlayerPrefs.SetFloat("BestTime", totalTime);
            PlayerPrefs.Save();
            Debug.Log("New Best Time: " + totalTime);

            // ✅ Update Best Time UI
            if (bestTimeText != null)
            {
                int bestMinutes = Mathf.FloorToInt(totalTime / 60);
                int bestSeconds = Mathf.FloorToInt(totalTime % 60);
                int bestMilliseconds = Mathf.FloorToInt((totalTime * 1000) % 1000);
        
                bestTimeText.text = $"Best Time: {bestMinutes:D2}:{bestSeconds:D2}.{bestMilliseconds:D3}";
            }
        }

    }


    void UpdateLiveTime(float elapsedTime)
    {
        if (liveTimeText != null)
        {
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
        
        liveTimeText.text = $"Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
        }
    }


    void EndGame()
    {
        if (GameOverPanel == null)
        {
        Debug.LogError("GameOverPanel is NULL when trying to show it!");
        return;
        }

        // ✅ Disable UI Elements
        if (timeTakenText != null) timeTakenText.gameObject.SetActive(false);
        if (lapCounterText != null) lapCounterText.gameObject.SetActive(false);
        if (liveTimeText != null) liveTimeText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
        Debug.Log("EndGame() called");
    }

    void RestartRace()
    {
        Time.timeScale = 1f;
        lapsCompleted = 0;

        if (GameOverPanel != null)
            GameOverPanel.SetActive(false);

        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void GoToMainMenu()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
    }
}
