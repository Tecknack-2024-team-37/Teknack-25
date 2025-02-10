using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
  public GameObject pauseUI;
    // Start is called before the first frame update
  public void OnRestartPress(){
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void OnGameResumePress(){
        pauseUI.SetActive(false);
  }

  public void OnGameExitPress(){
        Application.Quit();
  }

  public void OnEnterPausePress(){
        pauseUI.SetActive(true);
  }

  public void MainMenu(){
    SceneManager.LoadSceneAsync(0);
   }

   public void AllLevels(){
    SceneManager.LoadSceneAsync(1);
   }
}
