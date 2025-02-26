// \using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;

// public class CrossFade : MonoBehaviour
// {
//     public static CrossFade Instance { get; private set; } // Singleton

//     [SerializeField] private CanvasGroup crossFade;
//     [SerializeField] private Slider loadingSlider;

//     public Slider LoadingSlider => loadingSlider; // Public getter

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//             return;
//         }
//     }

//     private void Start()
//     {
//         FindUIElements();
//         ResetFade();
//     }

//     private void FindUIElements()
//     {
//         if (crossFade == null)
//         {
//             crossFade = FindObjectOfType<CanvasGroup>();
//             if (crossFade == null) Debug.LogError("CrossFade: No CanvasGroup found!");
//         }

//         if (loadingSlider == null)
//         {
//             loadingSlider = FindObjectOfType<Slider>();
//             if (loadingSlider == null) Debug.LogError("CrossFade: No Loading Slider found!");
//         }
//     }

//     public void PrepareForNewTransition()
//     {
//         FindUIElements();
//         ResetFade();
//         crossFade.gameObject.SetActive(true);
//         crossFade.blocksRaycasts = true;

//         if (loadingSlider != null)
//         {
//             loadingSlider.gameObject.SetActive(true);
//             loadingSlider.value = 0;
//         }
//     }

//     public IEnumerator AnimateTransitionIn()
//     {
//         if (crossFade == null) yield break;

//         PrepareForNewTransition();

//         bool isComplete = false;
//         LeanTween.alphaCanvas(crossFade, 1f, 0.8f).setOnComplete(() => isComplete = true);
//         yield return new WaitUntil(() => isComplete);
//     }

//     public IEnumerator AnimateTransitionOut()
//     {
//         if (crossFade == null) yield break;

//         bool isComplete = false;
//         LeanTween.alphaCanvas(crossFade, 0f, 0.8f).setOnComplete(() => isComplete = true);
//         yield return new WaitUntil(() => isComplete);

//         crossFade.blocksRaycasts = false;
//         if (loadingSlider != null) loadingSlider.gameObject.SetActive(false);
//         crossFade.gameObject.SetActive(false);
//     }

//     private void ResetFade()
//     {
//         if (crossFade != null)
//         {
//             crossFade.alpha = 0f;
//             crossFade.gameObject.SetActive(false);
//         }

//         if (loadingSlider != null)
//             loadingSlider.gameObject.SetActive(false);
//     }
// }
