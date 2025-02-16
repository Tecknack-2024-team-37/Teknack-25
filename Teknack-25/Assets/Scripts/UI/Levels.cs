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
using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public float fadeOutDelay = 1.5f; // Delay before fading out after loading

    public void LoadLevel(int sceneIndex)
    {
        if (CrossFade.Instance == null)
        {
            Debug.LogError("CrossFade Instance is missing! Cannot perform transition.");
            SceneManager.LoadScene(sceneIndex);
            return;
        }

        StartCoroutine(LoadSceneWithCrossFade(sceneIndex));
    }

    private IEnumerator LoadSceneWithCrossFade(int sceneIndex)
    {
        if (CrossFade.Instance == null) yield break;

        yield return StartCoroutine(CrossFade.Instance.AnimateTransitionIn());

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        asyncLoad.allowSceneActivation = false;

        // 🔥 Keep updating the slider until the scene is activated
        while (!asyncLoad.isDone)
        {
            if (CrossFade.Instance.loadingSlider != null)
            {
                // If progress is 0.9, keep it at 1.0 for full load effect
                float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
                CrossFade.Instance.loadingSlider.value = progress;
            }

            if (asyncLoad.progress >= 0.9f)
            {
                yield return new WaitForSeconds(fadeOutDelay);
                yield return StartCoroutine(CrossFade.Instance.AnimateTransitionOut());

                asyncLoad.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
