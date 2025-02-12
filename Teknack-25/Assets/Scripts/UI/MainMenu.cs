using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
     public GameObject pauseUI;
   public void PlayGame(){
    SceneManager.LoadSceneAsync(1);
   }
   public void OnEnterCreditPress(){
        pauseUI.SetActive(true);
  }

  public void OnEnterBackPress(){
    pauseUI.SetActive(false);
  }

  public void OnEnterCreditsPress(){
    SceneManager.LoadSceneAsync(5);
  }
}
