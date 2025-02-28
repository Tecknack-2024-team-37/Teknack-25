// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class LapTracker : MonoBehaviour
// {
//     [Header("UI Elements")]
//     public Text carPositionText;
//     public Text lapCounterText;
//     public GameObject GameOverPanel;
//     public Text timeTakenText;
//     public Text bestTimeText;
//     public Text finalTimeText;

//     [Header("Race Variables")]
//     private int passedCheckPointNumber = 0;
//     private int numberOfPassedCheckpoints = 0;
    
//     private int lapsCompleted = 0;
//     private const int lapsToComplete = 1;

//     private bool isRaceCompleted = false;
//     private int carPosition = 0;

//     private float raceStartTime;
//     private float raceEndTime;
//     private string sceneBestTimeKey;

//     [Header("Car Controllers")]
//     public GameObject[] cars; // Assign Car GameObjects here (Car1, Car2, Car3, Car4)

//     private int carsFinished = 0;

//     public event Action<LapTracker> onPassCheckPoint;

//     void Start()
//     {
//         raceStartTime = Time.time;
//         sceneBestTimeKey = "BestTime_" + SceneManager.GetActiveScene().name;

//         UpdateLapUI();
//         ShowBestTime();

//         if (GameOverPanel != null)
//         {
//             GameOverPanel.SetActive(false);
//         }
//     }

//     void ShowBestTime()
//     {
//         float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);
//         if (bestTime < float.MaxValue && bestTimeText != null)
//         {
//             int bestMinutes = Mathf.FloorToInt(bestTime / 60);
//             int bestSeconds = Mathf.FloorToInt(bestTime % 60);
//             int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

//             bestTimeText.text = $"Best Time: {bestMinutes:D2}:{bestSeconds:D2}.{bestMilliseconds:D3}";
//         }
//     }

//     void Update()
//     {
//         if (!isRaceCompleted)
//         {
//             float elapsedTime = Time.time - raceStartTime;
//             UpdateTimeUI(elapsedTime);
//         }
//     }

//     void UpdateTimeUI(float time)
//     {
//         if (timeTakenText != null)
//         {
//             int minutes = Mathf.FloorToInt(time / 60);
//             int seconds = Mathf.FloorToInt(time % 60);
//             int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

//             timeTakenText.text = $"Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
//         }
//     }

//     // ✅ Fix: Change OnCollisionEnter2D to OnTriggerEnter2D
//     void OnTriggerEnter2D(Collider2D other)
// {
//     Debug.Log($"{gameObject.name} entered {other.gameObject.name}");

//     if (other.CompareTag("CheckPoint")) 
//     {
//         CheckPoint checkPoint = other.GetComponent<CheckPoint>();

//         if (checkPoint == null)
//         {
//             Debug.LogError("Checkpoint script is missing on: " + other.gameObject.name);
//             return;
//         }

//         Debug.Log($"Checkpoint {checkPoint.checkPointNumber} reached by {gameObject.name}");

//         if (checkPoint.checkPointNumber == ((passedCheckPointNumber % 9) + 1))
//         {
//             Debug.Log("Valid checkpoint passed!");
//             passedCheckPointNumber = checkPoint.checkPointNumber;
//             numberOfPassedCheckpoints++;

//             if (checkPoint.isFinishLine)
//             {
//                 Debug.Log("Finish Line Reached! Incrementing lap...");
//                 passedCheckPointNumber = 0;
//                 lapsCompleted++;
//                 UpdateLapUI();

//                 if (lapsCompleted >= lapsToComplete)
//                 {
//                     CompleteRace(other.gameObject);
//                 }
//             }

//             onPassCheckPoint?.Invoke(this);
//         }
//         else
//         {
//             Debug.Log($"Incorrect checkpoint order! Expected: {(passedCheckPointNumber % 9) + 1}, but got: {checkPoint.checkPointNumber}");
//         }
//     }
// }


//     void UpdateLapUI()
//     {
//         if (lapCounterText != null)
//         {
//             lapCounterText.text = $"Laps: {lapsCompleted}/{lapsToComplete}";
//             Debug.Log("Lap UI Updated: " + lapCounterText.text);
//         }
//     }

//     void CompleteRace(GameObject car)
//     {
//         void DisableCarController(GameObject car)
//     {
//     MonoBehaviour[] carScripts = car.GetComponents<MonoBehaviour>();

//     foreach (var script in carScripts)
//     {
//         if (script.GetType().Name.StartsWith("CarController"))
//         {
//             script.enabled = false;
//             Debug.Log($"Disabled {script.GetType().Name} for {car.name}");
//         }
//     }
//     }

//     }

//     void DisableCarController(GameObject car)
//     {
//         MonoBehaviour[] carScripts = car.GetComponents<MonoBehaviour>();

//         foreach (var script in carScripts)
//         {
//             if (script.GetType().Name.StartsWith("CarController"))
//             {
//                 script.enabled = false;
//                 Debug.Log($"Disabled {script.GetType().Name} for {car.name}");
//             }
//         }
//     }

//     void ShowFinalTime(float totalTime)
//     {
//         isRaceCompleted = true;

//         int minutes = Mathf.FloorToInt(totalTime / 60);
//         int seconds = Mathf.FloorToInt(totalTime % 60);
//         int milliseconds = Mathf.FloorToInt((totalTime * 1000) % 1000);

//         timeTakenText.text = $"Final Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";

//         float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);

//         if (finalTimeText != null)
//         {
//             finalTimeText.text = timeTakenText.text;
//             finalTimeText.gameObject.SetActive(true);
//         }

//         if (totalTime < bestTime)
//         {
//             PlayerPrefs.SetFloat(sceneBestTimeKey, totalTime);
//             PlayerPrefs.Save();

//             if (bestTimeText != null)
//             {
//                 bestTimeText.text = $"Best Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
//             }
//         }
//     }

//     void EndGame()
//     {
//         if (GameOverPanel == null)
//         {
//             Debug.LogError("GameOverPanel is NULL when trying to show it!");
//             return;
//         }

//         Time.timeScale = 0f;
//         GameOverPanel.SetActive(true);
//     }
// }




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class LapTracker : MonoBehaviour
// {
//     [Header("UI Elements")]
//     public Text carPositionText;
//     public Text lapCounterText;
//     public GameObject GameOverPanel;
//     public Text timeTakenText;
//     public Text bestTimeText;
//     public Text finalTimeText;

//     [Header("Race Variables")]
//     private int passedCheckPointNumber = 0;
//     private int numberOfPassedCheckpoints = 0;

//     private int lapsCompleted = 0;
//     private const int lapsToComplete = 1;
//     private static int totalPlayersFinished = 0;
//     private static bool raceEnded = false;

//     private bool isRaceCompleted = false;
//     private float raceStartTime;
//     private float raceEndTime;
//     private string sceneBestTimeKey;

//     private MonoBehaviour carController; // Generic controller reference

//     public event Action<LapTracker> onPassCheckPoint;

//     void Start()
//     {
//         raceStartTime = Time.time;
//         sceneBestTimeKey = "BestTime_" + SceneManager.GetActiveScene().name;

//         // Find and store the specific car controller script dynamically
//         carController = GetCarController();

//         UpdateLapUI();

//         float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);
//         if (bestTime < float.MaxValue && bestTimeText != null)
//         {
//             int bestMinutes = Mathf.FloorToInt(bestTime / 60);
//             int bestSeconds = Mathf.FloorToInt(bestTime % 60);
//             int bestMilliseconds = Mathf.FloorToInt((bestTime * 1000) % 1000);

//             bestTimeText.text = $"Best Time: {bestMinutes:D2}:{bestSeconds:D2}.{bestMilliseconds:D3}";
//         }

//         if (GameOverPanel != null)
//         {
//             GameOverPanel.SetActive(false);
//         }
//     }

//     void Update()
//     {
//         if (!isRaceCompleted)
//         {
//             float elapsedTime = Time.time - raceStartTime;
//             UpdateTimeUI(elapsedTime);
//         }
//     }

//     void UpdateTimeUI(float time)
//     {
//         if (timeTakenText != null)
//         {
//             int minutes = Mathf.FloorToInt(time / 60);
//             int seconds = Mathf.FloorToInt(time % 60);
//             int milliseconds = Mathf.FloorToInt((time * 1000) % 1000);

//             timeTakenText.text = $"Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
//         }
//     }

//     void OnTriggerEnter2D(Collider2D collider2D)
//     {
//         if (collider2D.CompareTag("CheckPoint"))
//         {
//             CheckPoint checkPoint = collider2D.GetComponent<CheckPoint>();
//             if (checkPoint == null)
//             {
//                 Debug.LogError("Checkpoint script is missing on: " + collider2D.gameObject.name);
//                 return;
//             }

//             if (checkPoint.checkPointNumber == ((passedCheckPointNumber % 9) + 1))
//             {
//                 passedCheckPointNumber = checkPoint.checkPointNumber;
//                 numberOfPassedCheckpoints++;

//                 if (checkPoint.isFinishLine)
//                 {
//                     passedCheckPointNumber = 0;
//                     lapsCompleted++;
//                     UpdateLapUI();

//                     if (lapsCompleted >= lapsToComplete && !isRaceCompleted)
//                     {
//                         isRaceCompleted = true;
//                         raceEndTime = Time.time;
//                         float totalTimeTaken = raceEndTime - raceStartTime;
//                         ShowFinalTime(totalTimeTaken);

//                         totalPlayersFinished++;

//                         // Disable the car's control script
//                         if (carController != null)
//                         {
//                             carController.enabled = false; // Disables movement
//                         }

//                         // When all 4 cars finish, show the Game Over Panel
//                         if (totalPlayersFinished >= 4 && !raceEnded)
//                         {
//                             raceEnded = true;
//                             EndGame();
//                         }
//                     }
//                 }

//                 onPassCheckPoint?.Invoke(this);
//             }
//         }
//     }

//     void UpdateLapUI()
//     {
//         if (lapCounterText != null)
//         {
//             lapCounterText.text = $"Laps: {lapsCompleted}/{lapsToComplete}";
//         }
//     }

//     void ShowFinalTime(float totalTime)
//     {
//         isRaceCompleted = true;
//         int minutes = Mathf.FloorToInt(totalTime / 60);
//         int seconds = Mathf.FloorToInt(totalTime % 60);
//         int milliseconds = Mathf.FloorToInt((totalTime * 1000) % 1000);

//         timeTakenText.text = $"Final Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";

//         float bestTime = PlayerPrefs.GetFloat(sceneBestTimeKey, float.MaxValue);
//         if (finalTimeText != null)
//         {
//             finalTimeText.text = timeTakenText.text;
//             finalTimeText.gameObject.SetActive(true);
//         }

//         if (totalTime < bestTime)
//         {
//             PlayerPrefs.SetFloat(sceneBestTimeKey, totalTime);
//             PlayerPrefs.Save();

//             if (bestTimeText != null)
//             {
//                 bestTimeText.text = $"Best Time: {minutes:D2}:{seconds:D2}.{milliseconds:D3}";
//             }
//         }
//     }

//     void EndGame()
//     {
//         if (GameOverPanel == null)
//         {
//             Debug.LogError("GameOverPanel is NULL when trying to show it!");
//             return;
//         }

//         Time.timeScale = 0f;
//         GameOverPanel.SetActive(true);
//     }

//     /// <summary>
//     /// Dynamically finds the correct car controller script attached to this object.
//     /// </summary>
//     MonoBehaviour GetCarController()
//     {
//         // Try finding different possible car controller scripts
//         MonoBehaviour controller = GetComponent<CarController101>(); 
//         if (controller == null) controller = GetComponent<CarController102>(); 
//         if (controller == null) controller = GetComponent<CarController103>(); 
//         if (controller == null) controller = GetComponent<CarController104>(); 

//         if (controller == null)
//         {
//             Debug.LogError($"No car controller script found on {gameObject.name}!");
//         }
//         return controller;
//     }
// }









using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LapTracker : MonoBehaviour
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
    private const int lapsToComplete = 1;

    private bool isRaceCompleted = false;
    private int carPosition = 0;

    private bool isHideRoutineRunning = false;
    private float hideUIDelayTime;

    private float raceStartTime;
    private float raceEndTime;
    private string sceneBestTimeKey;

    public event Action<LapTracker> onPassCheckPoint;

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
                        StopGame();
                    }
                }

                onPassCheckPoint?.Invoke(this);
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

    void StopGame()
    {
        if (GameOverPanel == null)
        {
            Debug.LogError("GameOverPanel is NULL when trying to show it!");
            return;
        }

        if(lapsCompleted==1)
        {   Debug.LogError("Laps Completed =1 ");
            DisableCarControls();
            Time.timeScale = 0f; // Stop the game
            GameOverPanel.SetActive(true);
        }
        
    }

    void DisableCarControls()
    {
        MonoBehaviour[] carControllers = GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour controller in carControllers)
        {
            if (controller is CarController101 || 
                controller is CarController102 || 
                controller is CarController103 || 
                controller is CarController104)
            {
                controller.enabled = false; // Disable the controller script
            }
        }

        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = Vector2.zero; // Stop movement
            rb.angularVelocity = 0f;
        }
    }
}
