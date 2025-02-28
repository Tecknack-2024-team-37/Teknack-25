using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingUI;
    public GameObject Finish;
    public ToggleSwitch toggleSwitch; // Reference to Toggle Switch

    public RectTransform joystickTransform; // Joystick UI
    public RectTransform boostButtonTransform; // Boost Button UI

    private Vector2 bottomLeftJoystick = new Vector2(400, 400);  // Joystick (Bottom Left)
    private Vector2 bottomRightJoystick; // Joystick (Bottom Right) - Calculated dynamically

    private Vector2 bottomLeftBoost; // Boost (Bottom Left) - Same as Joystick Bottom Left
    private Vector2 bottomRightBoost = new Vector2(2177f, 405f); // Boost (Bottom Right)

    private bool isPaused = false;
    private int lapsCompleted = 0;

    private CarUIInputHandler[] inputHandlers;
    private Button[] uiButtons;
    private EventTrigger[] eventTriggers;

    private void Start()
    {
        inputHandlers = FindObjectsOfType<CarUIInputHandler>();
        uiButtons = FindObjectsOfType<Button>();
        eventTriggers = FindObjectsOfType<EventTrigger>();

        bottomRightJoystick = new Vector2(Screen.width - 400, 400);
        bottomLeftBoost = bottomLeftJoystick;

        joystickTransform.anchoredPosition = bottomLeftJoystick;
        boostButtonTransform.anchoredPosition = bottomRightBoost;

        if (settingUI == null)
            {
                Debug.LogError("⚠️ settingUI is NOT assigned in the Inspector!");
            }
        else
            {
                Debug.Log("✅ settingUI is assigned: " + settingUI.name);
        }

    }

    private void ToggleInputHandlers(bool enable)
    {
        foreach (CarUIInputHandler handler in inputHandlers)
        {
            handler.enabled = enable;
        }

        foreach (EventTrigger trigger in eventTriggers)
        {
            trigger.enabled = enable;
        }

        foreach (Button button in uiButtons)
        {
            button.interactable = enable;
        }
    }

    public void OnRestartPress()
    {
        Time.timeScale = 1f;
        lapsCompleted = 0;

        if (Finish != null)
            Finish.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameResumePress()
    {
        pauseUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        ToggleInputHandlers(true);
    }

    public void OnGameExitPress()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    public void OnEnterPausePress()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pauseUI.SetActive(true);
        settingUI.SetActive(false);
        isPaused = true;
        Time.timeScale = 0f;
        ToggleInputHandlers(false);
    }

    private void ResumeGame()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(false);
        isPaused = false;
        Time.timeScale = 1f;
        ToggleInputHandlers(true);
    }

    public void OnEnterSettingsPress()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
        StartCoroutine(RefreshUI(settingUI));
        ToggleInputHandlers(false);
        // UpdateToggleSwitch(); // ✅ Update toggle state when entering settings
    }

    private IEnumerator RefreshUI(GameObject panel)
    {
        panel.SetActive(false);
        yield return null;
        panel.SetActive(true);
    }

    public void OnExitSettingsPress()
    {
        settingUI.SetActive(false);
        pauseUI.SetActive(true);
        ToggleInputHandlers(false);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(0);
    }

    public void AllLevels()
    {
        Time.timeScale = 1f;
        SceneManager.LoadSceneAsync(1);
    }

    public void MoveJoystickRight_BoostLeft()
    {
        if (joystickTransform == null || boostButtonTransform == null)
        {
            Debug.LogError("⚠️ Joystick or Boost Button Transform is not assigned!");
            return;
        }

        joystickTransform.anchoredPosition = bottomRightJoystick;
        boostButtonTransform.anchoredPosition = bottomLeftBoost;

        joystickTransform.gameObject.SetActive(true);
        boostButtonTransform.gameObject.SetActive(true);

        Debug.Log("📌 Joystick → Bottom Right | Boost → Bottom Left");
    }

    public void MoveJoystickLeft_BoostRight()
    {
        if (joystickTransform == null || boostButtonTransform == null)
        {
            Debug.LogError("⚠️ Joystick or Boost Button Transform is not assigned!");
            return;
        }

        joystickTransform.anchoredPosition = bottomLeftJoystick;
        boostButtonTransform.anchoredPosition = bottomRightBoost;

        joystickTransform.gameObject.SetActive(true);
        boostButtonTransform.gameObject.SetActive(true);

        Debug.Log("📌 Joystick → Bottom Left | Boost → Bottom Right");
    }

    // public void UpdateToggleSwitch()
    // {
    //     if (toggleSwitch != null)
    //     {
    //         toggleSwitch.ToggleByGroupManager(!toggleSwitch.CurrentValue);
    //     }
    // }
}








// using System.Collections;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class UIManager : MonoBehaviour
// {
//     public GameObject pauseUI;
//     public GameObject settingUI;
//     public GameObject Finish;

//     public RectTransform joystickTransform; // Joystick UI
//     public RectTransform boostButtonTransform; // Boost Button UI

//     private Vector2 bottomLeftJoystick = new Vector2(400, 400);  // Joystick (Bottom Left)
//     private Vector2 bottomRightJoystick; // Joystick (Bottom Right) - Calculated dynamically

//     private Vector2 bottomLeftBoost; // Boost (Bottom Left) - Same as Joystick Bottom Left
//     private Vector2 bottomRightBoost = new Vector2(1970f, 333f); // Boost (Bottom Right)

//     private bool isPaused = false;
//     private int lapsCompleted = 0;

//     private CarUIInputHandler[] inputHandlers;
//     private Button[] uiButtons;
//     private EventTrigger[] eventTriggers;

//     private void Start()
//     {
//         inputHandlers = FindObjectsOfType<CarUIInputHandler>();
//         uiButtons = FindObjectsOfType<Button>();
//         eventTriggers = FindObjectsOfType<EventTrigger>();

//         // ✅ Set dynamic positions based on screen width
//         bottomRightJoystick = new Vector2(Screen.width - 400, 400);
//         bottomLeftBoost = bottomLeftJoystick; // Boost moves where Joystick was

//         // ✅ Ensure default positions are correct
//         joystickTransform.anchoredPosition = bottomLeftJoystick;
//         boostButtonTransform.anchoredPosition = bottomRightBoost;
//     }

//     private void ToggleInputHandlers(bool enable)
//     {
//         foreach (CarUIInputHandler handler in inputHandlers)
//         {
//             handler.enabled = enable;
//         }

//         foreach (EventTrigger trigger in eventTriggers)
//         {
//             trigger.enabled = enable;
//         }

//         foreach (Button button in uiButtons)
//         {
//             button.interactable = enable;
//         }
//     }

//     public void OnRestartPress()
//     {
//         Time.timeScale = 1f;
//         lapsCompleted = 0;

//         if (Finish != null)
//             Finish.SetActive(false);

//         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//     }

//     public void OnGameResumePress()
//     {
//         pauseUI.SetActive(false);
//         isPaused = false;
//         Time.timeScale = 1f;
//         ToggleInputHandlers(true);
//     }

//     public void OnGameExitPress()
//     {
//         Time.timeScale = 1f;
//         Application.Quit();
//     }

//     public void OnEnterPausePress()
//     {
//         if (isPaused)
//         {
//             ResumeGame();
//         }
//         else
//         {
//             PauseGame();
//         }
//     }

//     private void PauseGame()
//     {
//         pauseUI.SetActive(true);
//         settingUI.SetActive(false);
//         isPaused = true;
//         Time.timeScale = 0f;
//         ToggleInputHandlers(false);
//     }

//     private void ResumeGame()
//     {
//         pauseUI.SetActive(false);
//         settingUI.SetActive(false);
//         isPaused = false;
//         Time.timeScale = 1f;
//         ToggleInputHandlers(true);
//     }

//     public void OnEnterSettingsPress()
//     {
//         pauseUI.SetActive(false);
//         settingUI.SetActive(true);
//         StartCoroutine(RefreshUI(settingUI)); // ✅ Refresh UI to fix toggle handle issue
//         ToggleInputHandlers(false);
//     }

//     private IEnumerator RefreshUI(GameObject panel)
//     {
//         panel.SetActive(false);
//         yield return null; // Wait for a frame
//         panel.SetActive(true);
//     }

//     public void OnExitSettingsPress()
//     {
//         settingUI.SetActive(false);
//         pauseUI.SetActive(true);
//         ToggleInputHandlers(false);
//     }

//     public void MainMenu()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadSceneAsync(0);
//     }

//     public void AllLevels()
//     {
//         Time.timeScale = 1f;
//         SceneManager.LoadSceneAsync(1);
//     }

//     // 📌 Move Joystick to Bottom Right & Boost to Bottom Left
//     public void MoveJoystickRight_BoostLeft()
//     {
//         if (joystickTransform == null || boostButtonTransform == null)
//         {
//             Debug.LogError("⚠️ Joystick or Boost Button Transform is not assigned!");
//             return;
//         }

//         joystickTransform.anchoredPosition = bottomRightJoystick;
//         boostButtonTransform.anchoredPosition = bottomLeftBoost;

//         joystickTransform.gameObject.SetActive(true);
//         boostButtonTransform.gameObject.SetActive(true);

//         Debug.Log("📌 Joystick → Bottom Right | Boost → Bottom Left");
//     }

//     // 📌 Move Joystick to Bottom Left & Boost to Bottom Right
//     public void MoveJoystickLeft_BoostRight()
//     {
//         if (joystickTransform == null || boostButtonTransform == null)
//         {
//             Debug.LogError("⚠️ Joystick or Boost Button Transform is not assigned!");
//             return;
//         }

//         joystickTransform.anchoredPosition = bottomLeftJoystick;
//         boostButtonTransform.anchoredPosition = bottomRightBoost;

//         joystickTransform.gameObject.SetActive(true);
//         boostButtonTransform.gameObject.SetActive(true);

//         Debug.Log("📌 Joystick → Bottom Left | Boost → Bottom Right");
//     }
// }













// // // using System.Collections;
// // // using System.Collections.Generic;
// // // using UnityEngine;
// // // using UnityEngine.SceneManagement;
// // // using UnityEngine.UI;
// // // using UnityEngine.EventSystems; // Required for EventTrigger

// // // public class UIManager : MonoBehaviour
// // // {
// // //     public GameObject pauseUI;
// // //     public GameObject settingUI;
// // //     private bool isPaused = false; 
// // //     public GameObject Finish;
// // //     private int lapsCompleted = 0;

// // //     private CarUIInputHandler[] inputHandlers; // Reference to all input handlers
// // //     private Button[] uiButtons; // Reference to UI buttons (Pause & Settings)
// // //     private EventTrigger[] eventTriggers; // Reference to all EventTriggers

// // //     private void Start()
// // //     {
// // //         inputHandlers = FindObjectsOfType<CarUIInputHandler>(); // Get all input handlers
// // //         uiButtons = FindObjectsOfType<Button>(); // Get all UI buttons
// // //         eventTriggers = FindObjectsOfType<EventTrigger>(); // Get all EventTriggers
// // //     }

// // //     private void ToggleInputHandlers(bool enable)
// // //     {
// // //         // Enable/Disable Car Input  ipts
// // //         foreach (CarUIInputHandler handler in inputHandlers)
// // //         {
// // //             handler.enabled = enable;
// // //         }

// // //         // Enable/Disable EventTriggers for UI buttons (Accelerate, Brake, Steering)
// // //         foreach (EventTrigger trigger in eventTriggers)
// // //         {
// // //             trigger.enabled = enable;
// // //         }
// // //     }

// // //     public void OnRestartPress()
// // //     {
// // //         Time.timeScale = 1f;
// // //         lapsCompleted = 0;

// // //         if (Finish != null)
// // //             Finish.SetActive(false);

// // //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
// // //     }

// // //     public void OnGameResumePress()
// // //     {
// // //         pauseUI.SetActive(false);
// // //         isPaused = false;
// // //         ToggleInputHandlers(true); // Re-enable inputs
// // //     }

// // //     public void OnGameExitPress()
// // //     {
// // //         Application.Quit();
// // //     }

// // //     public void OnEnterPausePress()
// // //     {
// // //         if (isPaused)
// // //         {
// // //             pauseUI.SetActive(false);
// // //             settingUI.SetActive(false);
// // //             isPaused = false;
// // //             ToggleInputHandlers(true); // Enable input when unpaused
// // //         }
// // //         else
// // //         {
// // //             pauseUI.SetActive(true);
// // //             settingUI.SetActive(false);
// // //             isPaused = true;
// // //             ToggleInputHandlers(false); // Disable input when paused
// // //         }
// // //     }

// // //     public void OnEnterSettingsPress()
// // //     {
// // //         pauseUI.SetActive(false);
// // //         settingUI.SetActive(true);
// // //         ToggleInputHandlers(false); // Disable input while settings are open
// // //     }

// // //     public void OnExitSettingsPress()
// // //     {
// // //         settingUI.SetActive(false);
// // //         pauseUI.SetActive(true);
// // //         ToggleInputHandlers(false); // Keep input disabled while in pause menu
// // //     }

// // //     public void MainMenu()
// // //     {
// // //         SceneManager.LoadSceneAsync(0);
// // //     }

// // //     public void AllLevels()
// // //     {
// // //         SceneManager.LoadSceneAsync(1);
// // //     }
// // // }

// // using System.Collections;
// // using System.Collections.Generic;
// // using UnityEngine;
// // using UnityEngine.SceneManagement;
// // using UnityEngine.UI;
// // using UnityEngine.EventSystems; // Required for EventTrigger

// // public class UIManager : MonoBehaviour
// // {
// //     public GameObject pauseUI;
// //     public GameObject settingUI;
// //     public GameObject Finish;

// //     private bool isPaused = false;
// //     private int lapsCompleted = 0;

// //     private CarUIInputHandler[] inputHandlers; // Reference to all input handlers
// //     private Button[] uiButtons; // Reference to UI buttons (Pause & Settings)
// //     private EventTrigger[] eventTriggers; // Reference to all EventTriggers

// //     private void Start()
// //     {
// //         inputHandlers = FindObjectsOfType<CarUIInputHandler>(); // Get all input handlers
// //         uiButtons = FindObjectsOfType<Button>(); // Get all UI buttons
// //         eventTriggers = FindObjectsOfType<EventTrigger>(); // Get all EventTriggers
// //     }

// //     private void ToggleInputHandlers(bool enable)
// //     {
// //         // Enable/Disable Car Input Scripts
// //         foreach (CarUIInputHandler handler in inputHandlers)
// //         {
// //             handler.enabled = enable;
// //         }

// //         // Enable/Disable EventTriggers for UI buttons (Accelerate, Brake, Steering)
// //         foreach (EventTrigger trigger in eventTriggers)
// //         {
// //             trigger.enabled = enable;
// //         }

// //         // Disable UI buttons to prevent accidental presses while paused
// //         foreach (Button button in uiButtons)
// //         {
// //             button.interactable = enable;
// //         }
// //     }

// //     public void OnRestartPress()
// //     {
// //         Time.timeScale = 1f; // Ensure normal time before restarting
// //         lapsCompleted = 0;

// //         if (Finish != null)
// //             Finish.SetActive(false);

// //         SceneManager.LoadScene(SceneManager.GetActiveScene().name);
// //     }

// //     public void OnGameResumePress()
// //     {
// //         pauseUI.SetActive(false);
// //         isPaused = false;
// //         Time.timeScale = 1f;  // Resume game speed
// //         ToggleInputHandlers(true); // Re-enable inputs
// //     }

// //     public void OnGameExitPress()
// //     {
// //         Time.timeScale = 1f; // Ensure normal time before quitting
// //         Application.Quit();
// //     }

// //     public void OnEnterPausePress()
// //     {
// //         if (isPaused)
// //         {
// //             ResumeGame();
// //         }
// //         else
// //         {
// //             PauseGame();
// //         }
// //     }

// //     private void PauseGame()
// //     {
// //         pauseUI.SetActive(true);
// //         settingUI.SetActive(false);
// //         isPaused = true;
// //         Time.timeScale = 0f; // Stop game time
// //         ToggleInputHandlers(false); // Disable input when paused
// //     }

// //     private void ResumeGame()
// //     {
// //         pauseUI.SetActive(false);
// //         settingUI.SetActive(false);
// //         isPaused = false;
// //         Time.timeScale = 1f; // Resume game time
// //         ToggleInputHandlers(true); // Enable input when resuming
// //     }

// //     public void OnEnterSettingsPress()
// //     {
// //         pauseUI.SetActive(false);
// //         settingUI.SetActive(true);
// //         ToggleInputHandlers(false); // Disable input while settings are open
// //     }

// //     public void OnExitSettingsPress()
// //     {
// //         settingUI.SetActive(false);
// //         pauseUI.SetActive(true);
// //         ToggleInputHandlers(false); // Keep input disabled while in pause menu
// //     }

// //     public void MainMenu()
// //     {
// //         Time.timeScale = 1f; // Ensure normal time before switching scenes
// //         SceneManager.LoadSceneAsync(0);
// //     }

// //     public void AllLevels()
// //     {
// //         Time.timeScale = 1f; // Ensure normal time before switching scenes
// //         SceneManager.LoadSceneAsync(1);
// //     }
// // }
