using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public Button pauseButton;
    public Button resumeButton;
    public Button allModesButton;
    public Button settingsButton;
    public Button closeSettingsButton;

    public GameObject pauseMenu;
    public GameObject settingsPanel;
    public GameObject allModesPanel;
    void Start()
    {
        if (pauseButton != null)
            pauseButton.onClick.AddListener(PauseGame);
        else
            Debug.LogError("Pause Button is NOT assigned!");

        if (resumeButton != null)
            resumeButton.onClick.AddListener(ResumeGame);
        else
            Debug.LogError("Resume Button is NOT assigned!");

        if (allModesButton != null)
            allModesButton.onClick.AddListener(OpenAllModes);
        else
            Debug.LogError("All Modes Button is NOT assigned!");

        if (settingsButton != null)
            settingsButton.onClick.AddListener(OpenSettings);
        else
            Debug.LogError("Settings Button is NOT assigned!");

        if (closeSettingsButton != null)
            closeSettingsButton.onClick.AddListener(CloseSettings);
        else
            Debug.LogError("Close Settings Button is NOT assigned!");

        if (pauseMenu != null) pauseMenu.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (allModesPanel != null) allModesPanel.SetActive(false);
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        if (pauseMenu != null) pauseMenu.SetActive(true);
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        if (pauseMenu != null) pauseMenu.SetActive(false);
    }

    void OpenAllModes()
    {
        Debug.Log("Opening All Modes...");
        if (allModesPanel != null) 
        {
            allModesPanel.SetActive(true);
            settingsPanel.SetActive(false);  // Ensure settings panel is hidden
        }
    }

    void OpenSettings()
    {
        Debug.Log("Opening Settings...");
        if (settingsPanel != null) 
        {
            settingsPanel.SetActive(true);
            allModesPanel.SetActive(false);  // Ensure all modes panel is hidden
        }
    }

    void CloseSettings()
    {
        Debug.Log("Closing Settings...");
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }
}


