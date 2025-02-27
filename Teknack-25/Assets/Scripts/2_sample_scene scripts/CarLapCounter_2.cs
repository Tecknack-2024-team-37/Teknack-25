
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarLapCounter_2 : MonoBehaviour
{
    [Header("UI Elements")]
    public Text carPositionText;
    public Text lapCounterText;
    public GameObject GameOverPanel;
    public Text timeTakenText;
    public Text bestTimeText;
    public Text finalTimeText;

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

    private float raceStartTime;
    private float raceEndTime;
    private string sceneBestTimeKey;

    public event Action<CarLapCounter_2> onPassCheckPoint;

    void Start()
    {
        raceStartTime = Time.time;
        sceneBestTimeKey = "BestTime_" + SceneManager.GetActiveScene().name;

        UpdateLapUI();

        float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);
        if (bestTime < float.MaxValue && bestTimeText != null)
        {
            int bestMinutes = Mathf.FloorToInt(bestTime / 60);
            int bestSeconds = Mathf.FloorToInt(bestTime % 60);
            int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

            bestTimeText.text = $"Best Time: {bestMinutes:D2}:{bestSeconds:D2}.{bestMilliseconds:D3}";
        }

        ValidateUIElements();

        UpdateLapUI();

        if (GameOverPanel != null)
        {
            GameOverPanel.SetActive(false);
        }
    }

    void ValidateUIElements()
    {
        if (timeTakenText == null) Debug.LogError("Time Taken Text is NOT assigned in the Inspector!");
        if (lapCounterText == null) Debug.LogError("Lap Counter Text is not assigned in the Inspector!");
        if (GameOverPanel == null) Debug.LogError("GameOverPanel is NOT assigned in the Inspector!");
        // if (carPositionText == null) Debug.LogError("carPositionText (Text) is NOT assigned in the Inspector!");
    }

    public void ResetBestTime()
    {
        PlayerPrefs.DeleteKey(sceneBestTimeKey);
        PlayerPrefs.Save();
        Debug.Log("Best Time Reset for " + SceneManager.GetActiveScene().name);

        if (bestTimeText != null)
        {
            bestTimeText.text = "Best Time: --:--.---";
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
            Debug.LogError("carPositionText (Text) is NULL! Assign it in the Inspector.");
            yield break;
        }

        hideUIDelayTime += delayUntilHidePosition;
        carPositionText.text = carPosition.ToString();
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
        if (!isRaceCompleted)
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
        if (collider2D.CompareTag("CheckPoint"))
        {
            CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();

            if (checkPoint == null)
            {
                Debug.LogError("Checkpoint script is missing on: " + collider2D.gameObject.name);
                return;
            }

            if (checkPoint.checkPointNumber == ((passedCheckPointNumber % 9) + 1))
            {
                passedCheckPointNumber = checkPoint.checkPointNumber;
                numberOfPassedCheckpoints++;
                timeAtLastPassedCheckPoint = Time.time;

                if (checkPoint.isFinishLine)
                {
                    passedCheckPointNumber = 0;
                    lapsCompleted++;
                    UpdateLapUI();

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
                        raceEndTime = Time.time;
                        float totalTimeTaken = raceEndTime - raceStartTime;
                        ShowFinalTime(totalTimeTaken);
                        EndGame();
                    }
                }

                onPassCheckPoint?.Invoke(this);
                StartCoroutine(ShowPositionCO(isRaceCompleted ? 100 : 1.5f));
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
        isRaceCompleted = true;

        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);
        int milliseconds = Mathf.FloorToInt((totalTime * 1000) % 1000);

        timeTakenText.text = $"Final Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";

        float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);

        if (finalTimeText != null)
        {
            finalTimeText.text = timeTakenText.text;
            finalTimeText.gameObject.SetActive(true);
        }

        if (totalTime < bestTime)
        {
            PlayerPrefs.SetFloat(sceneBestTimeKey, totalTime);
            PlayerPrefs.Save();

            if (bestTimeText != null)
            {
                bestTimeText.text = $"Best Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
            }
        }
    }

    void EndGame()
    {
        if (GameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is NULL when trying to show it!");
            return;
        }

        // if (timeTakenText != null) timeTakenText.gameObject.SetActive(false);
        // if (lapCounterText != null) lapCounterText.gameObject.SetActive(false);

        Time.timeScale = 0f;
        GameOverPanel.SetActive(true);
    }
}
