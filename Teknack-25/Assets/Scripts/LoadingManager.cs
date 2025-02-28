// using System.Collections;
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.SceneManagement;

// public class LoadingManager : MonoBehaviour
// {
//     public Slider progressBar;   // Assign in Inspector
//     public Image carImage;       // Assign in Inspector
//     public Text loadingText;     // Assign in Inspector (for random tips)

//     private string[] loadingTips = {
//         "Handle the joystick carefully!",
//         "Take proper turns to avoid crashing!",
//         "Perfect turns will reward you with extra coins!",
//         "Drifting at the right moment gives you a speed boost!",
//         "Avoid obstacles to maintain maximum speed!",
//         "Upgrade your car for better handling and speed!",
//         "Use Nitro Boost wisely to win the race!",
//         "Master the tracks to become a racing champion!",
//         "Stay on the racing line for the best performance!"
//     };

//     void Start()
//     {
//         // Display a random quote from the list
//         loadingText.text = loadingTips[Random.Range(0, loadingTips.Length)];

//         // Start loading the next level
//         StartCoroutine(LoadLevelAsync(PlayerPrefs.GetInt("NextLevel")));  
//     }

//     IEnumerator LoadLevelAsync(int levelIndex)
//     {
//         AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
//         operation.allowSceneActivation = false; // Prevent immediate scene switch

//         while (operation.progress < 0.9f) // Unity loads until ~90%
//         {
//             float progress = Mathf.Clamp01(operation.progress / 0.9f);
//             progressBar.value = progress; 

//             // Move car image from left to right along progress bar
//             carImage.transform.position = new Vector3(
//                 Mathf.Lerp(-500, 500, progress),
//                 carImage.transform.position.y, 
//                 0
//             );

//             yield return null;
//         }

//         progressBar.value = 1;
//         yield return new WaitForSeconds(0.5f);

//         operation.allowSceneActivation = true; // Load the selected level
//     }
//     public Image fadeImage; // Assign in Inspector

// IEnumerator FadeOut()
// {
//     float alpha = 0;
//     while (alpha < 1)
//     {
//         alpha += Time.deltaTime;
//         fadeImage.color = new Color(0, 0, 0, alpha);
//         yield return null;
//     }
// }

// }
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingManager : MonoBehaviour
{
    public Slider progressBar;   // Assign in Inspector
    public Image carImage;       // Assign in Inspector
    public Text loadingText;     // Assign in Inspector (for random tips)
    public Image fadeImage;      // Assign in Inspector for fade effect

    public float loadingSpeed = 0.3f;  // Adjust this to slow down loading

    private string[] loadingTips = {
        "Handle the joystick carefully!",
        "Take proper turns to avoid crashing!",
        // "Perfect turns will reward you with extra coins!",
        "Drifting at the right moment gives you a speed boost!",
        "Avoid obstacles to maintain maximum speed!",
        // "Upgrade your car for better handling and speed!",
        "Use Nitro Boost wisely to win the race!",
        "Master the tracks to become a racing champion!",
        "Come on Lewis it's Hammer Time",
        "Complete the race Simply Lovely!",
        "Stay on the racing line for the best performance!"
    };

    void Start()
    {
        
        loadingText.text = loadingTips[Random.Range(0, loadingTips.Length)];
        StartCoroutine(LoadLevelAsync(PlayerPrefs.GetInt("NextLevel")));  
    }

// IEnumerator LoadLevelAsync(int levelIndex)
// {
//     AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
//     operation.allowSceneActivation = false; // Prevent immediate scene switch

//     float progress = 0; // Manual progress control

//     RectTransform progressBarRect = progressBar.GetComponent<RectTransform>();
//     float minX = progressBarRect.position.x - (progressBarRect.rect.width / 2);
//     float maxX = progressBarRect.position.x + (progressBarRect.rect.width / 2);

//     while (progress < 1f) // Simulate slow loading
//     {
//         progress += Random.Range(0.02f, 0.05f) * loadingSpeed; // Adjust speed
//         progress = Mathf.Clamp01(progress);
//         progressBar.value = progress;

//         // Move car image with progress bar
//         if (carImage != null)
//         {
//             Vector3 newPos = carImage.transform.position;
//             newPos.x = Mathf.Lerp(minX, maxX, progressBar.value);
//             carImage.transform.position = newPos;
//         }

//         yield return new WaitForSeconds(0.1f); // Adjust to make loading slower
//     }

//     progressBar.value = 1;
//     yield return new WaitForSeconds(0.5f); // Wait before fading

//     StartCoroutine(FadeOut()); // Fade effect before transition
//     yield return new WaitForSeconds(0.5f); // Adjust fade duration

//     operation.allowSceneActivation = true; // Load the selected level
// }

IEnumerator LoadLevelAsync(int levelIndex)
{
    AsyncOperation operation = SceneManager.LoadSceneAsync(levelIndex);
    operation.allowSceneActivation = false; // Prevent immediate scene switch

    float progress = 0; // Manual progress control

    while (progress < 1f) // Simulate slow loading
    {
        progress += Random.Range(0.02f, 0.05f) * loadingSpeed; // Adjust speed
        progress = Mathf.Clamp01(progress);
        progressBar.value = progress;

        // Get the start and end positions of the progress bar
        RectTransform fillArea = progressBar.fillRect;
        float minX = progressBar.transform.position.x - (progressBar.GetComponent<RectTransform>().rect.width / 2);
        float maxX = progressBar.transform.position.x + (progressBar.GetComponent<RectTransform>().rect.width / 2);

        // Move car image with progress bar
        if (carImage != null)
        {
            Vector3 newPos = carImage.transform.position;
            newPos.x = Mathf.Lerp(minX, maxX, progress); // Sync car movement with progress
            carImage.transform.position = newPos;
        }

        yield return new WaitForSeconds(0.1f); // Adjust to make loading slower
    }

    progressBar.value = 1;
    yield return new WaitForSeconds(0.2f); // Wait before fading

    StartCoroutine(FadeOut()); // Fade effect before transition
    yield return new WaitForSeconds(0.5f); // Adjust fade duration

    operation.allowSceneActivation = true; // Load the selected level
}


    IEnumerator FadeOut()
    {
        float alpha = 0;
        while (alpha < 1)
        {
            alpha += Time.deltaTime / 2.0f; // Adjust fade speed
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }
    }
}
