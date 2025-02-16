using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CrossFade : MonoBehaviour
{
    public static CrossFade Instance { get; private set; } // Singleton
    public CanvasGroup crossFade;
    public Slider loadingSlider;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Keep across scenes
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (crossFade == null)
        {
            Debug.LogError("CrossFade: CanvasGroup not assigned!");
        }

        if (loadingSlider == null)
        {
            Debug.LogError("CrossFade: Loading Slider not assigned!");
        }

        ResetFade(); // Ensure it's hidden initially
    }

    public IEnumerator AnimateTransitionIn()
    {
        if (crossFade == null) yield break;

        ResetFade(); // Ensure it's fully hidden before starting
        crossFade.gameObject.SetActive(true);
        crossFade.blocksRaycasts = true;
        loadingSlider.gameObject.SetActive(true);

        bool isComplete = false;
        LeanTween.alphaCanvas(crossFade, 1f, 0.8f).setOnComplete(() => isComplete = true);
        yield return new WaitUntil(() => isComplete);
    }

    public IEnumerator AnimateTransitionOut()
    {
        if (crossFade == null) yield break;

        bool isComplete = false;
        LeanTween.alphaCanvas(crossFade, 0f, 0.8f).setOnComplete(() => isComplete = true);
        yield return new WaitUntil(() => isComplete);

        crossFade.blocksRaycasts = false;
        loadingSlider.gameObject.SetActive(false);
        crossFade.gameObject.SetActive(false);
    }

    private void ResetFade()
    {
        crossFade.alpha = 0f;
        crossFade.gameObject.SetActive(false);
        loadingSlider.gameObject.SetActive(false);
    }
}

