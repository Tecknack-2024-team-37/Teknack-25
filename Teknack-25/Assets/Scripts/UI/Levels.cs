// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.SceneManagement;
// public class Levels : MonoBehaviour
// {
//     public void BackToMain(){
//     SceneManager.LoadSceneAsync(0);
//    }
//    public void PlayLevel0(){
//     SceneManager.LoadSceneAsync(2);
//    }
//    public void PlayLevel1(){
//     SceneManager.LoadSceneAsync(3);
//    }

//    public void PlayLevel2(){
//     SceneManager.LoadSceneAsync(4);
//    }

//    public void PlayLevel3(){
//     SceneManager.LoadSceneAsync(6);
//    }
// }


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public void BackToMain()
    {
        SceneManager.LoadSceneAsync(0); // Load Main Menu Scene
    }

    public void PlayLevel(int levelIndex)
    {
        // Store the selected level index before loading the loading screen
        PlayerPrefs.SetInt("NextLevel", levelIndex);

        // Load the loading screen scene first
        SceneManager.LoadScene("LoadingScreen");
    }
}

