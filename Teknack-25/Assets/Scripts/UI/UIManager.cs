// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;

// public class UIManager : MonoBehaviour
// {
//   public GameObject pauseUI;
//   public GameObject settingUI;
//     // Start is called before the first frame update
//   public void OnRestartPress(){
//     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
//   }

//   public void OnGameResumePress(){
//         pauseUI.SetActive(false);
//   }

//   public void OnGameExitPress(){
//         Application.Quit();
//   }

//   public void OnEnterPausePress(){
//         pauseUI.SetActive(true);
//         // pauseUI.SetActive(false);
//   }

//   public void OnEnterSettingsPress(){
//     pauseUI.SetActive(false);
//     settingUI.SetActive(true);
//     pauseUI.SetActive(false);
//   }

//   public void MainMenu(){
//     SceneManager.LoadSceneAsync(0);
//    }

//    public void AllLevels(){
//     SceneManager.LoadSceneAsync(1);
//    }

//   //  public void OnEnterSettingsPress(){
//   //   pauseUI.SetActive(true);
//   //  }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject pauseUI;
    public GameObject settingUI;
    private bool isPaused = false;  // Track if game is paused

    public void OnRestartPress()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnGameResumePress()
    {
        pauseUI.SetActive(false);
        isPaused = false;
    }

    public void OnGameExitPress()
    {
        Application.Quit();
    }

    public void OnEnterPausePress()
    {
        if (isPaused)
        {
            // If already paused, close everything
            pauseUI.SetActive(false);
            settingUI.SetActive(false);
            isPaused = false;
        }
        else
        {
            // Open Pause menu
            pauseUI.SetActive(true);
            settingUI.SetActive(false);
            isPaused = true;
        }
    }

    public void OnEnterSettingsPress()
    {
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }

    public void OnExitSettingsPress()
    {
        // Go back to pause menu from settings
        settingUI.SetActive(false);
        pauseUI.SetActive(true);
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
