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

    public event Action<CarLapCounter_2> onPassCheckPoint;

    void Start()
    {
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

                    if (lapsCompleted >= lapsToComplete)
                    {
                        isRaceCompleted = true;
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

    void EndGame()
    {
        if (GameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is NULL when trying to show it!");
            return;
        }

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
