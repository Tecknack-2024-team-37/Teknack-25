using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Required for EventTrigger

public class UIManager2 : MonoBehaviour
{
    public GameObject settingUI; // Only settings menu

    private EventTrigger[] eventTriggers; // Reference to all EventTriggers

    private void Start()
    {
        eventTriggers = FindObjectsOfType<EventTrigger>(); // Get all EventTriggers
    }

    private void ToggleEventTriggers(bool enable)
    {
        // Enable/Disable EventTriggers for UI buttons (Accelerate, Brake, Steering)
        foreach (EventTrigger trigger in eventTriggers)
        {
            trigger.enabled = enable;
        }
    }

    public void OnEnterSettingsPress()
    {
        settingUI.SetActive(true); // Open Settings Menu
        ToggleEventTriggers(false); // Disable UI event triggers
    }

    public void OnExitSettingsPress()
    {
        settingUI.SetActive(false); // Close Settings Menu
        ToggleEventTriggers(true); // Re-enable UI event triggers
    }

    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameExitPress()
    {
        Application.Quit();
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
