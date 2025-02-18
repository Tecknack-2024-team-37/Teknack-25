using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for EventTrigger

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingUI;
    private bool isPaused = false; 
    public GameObject Finish;
    private int lapsCompleted = 0;

    private CarUIInputHandler[] inputHandlers; // Reference to all input handlers
    private Button[] uiButtons; // Reference to UI buttons (Pause & Settings)
    private EventTrigger[] eventTriggers; // Reference to all EventTriggers

    private void Start()
    {
        inputHandlers = FindObjectsOfType<CarUIInputHandler>(); // Get all input handlers
        uiButtons = FindObjectsOfType<Button>(); // Get all UI buttons
        eventTriggers = FindObjectsOfType<EventTrigger>(); // Get all EventTriggers
    }

    private void ToggleInputHandlers(bool enable)
    {
        // Enable/Disable Car Input Scripts
        foreach (CarUIInputHandler handler in inputHandlers)
        {
            handler.enabled = enable;
        }

        // Enable/Disable EventTriggers for UI buttons (Accelerate, Brake, Steering)
        foreach (EventTrigger trigger in eventTriggers)
        {
            trigger.enabled = enable;
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
        ToggleInputHandlers(true); // Re-enable inputs
    }

    public void OnGameExitPress()
    {
        Application.Quit();
    }

    public void OnEnterPausePress()
    {
        if (isPaused)
        {
            pauseUI.SetActive(false);
            settingUI.SetActive(false);
            isPaused = false;
            ToggleInputHandlers(true); // Enable input when unpaused
        }
        else
        {
            pauseUI.SetActive(true);
            settingUI.SetActive(false);
            isPaused = true;
            ToggleInputHandlers(false); // Disable input when paused
        }
    }

    public void OnEnterSettingsPress()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
        ToggleInputHandlers(false); // Disable input while settings are open
    }

    public void OnExitSettingsPress()
    {
        settingUI.SetActive(false);
        pauseUI.SetActive(true);
        ToggleInputHandlers(false); // Keep input disabled while in pause menu
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void AllLevels()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
