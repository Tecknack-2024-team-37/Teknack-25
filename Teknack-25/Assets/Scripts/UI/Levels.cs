using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Levels : MonoBehaviour
{
    public void BackToMain(){
    SceneManager.LoadSceneAsync(0);
   }
   public void PlayLevel0(){
    SceneManager.LoadSceneAsync(2);
   }
   public void PlayLevel1(){
    SceneManager.LoadSceneAsync(3);
   }

   public void PlayLevel2(){
    SceneManager.LoadSceneAsync(4);
   }

   public void PlayLevel3(){
    SceneManager.LoadSceneAsync(6);
   }
}


