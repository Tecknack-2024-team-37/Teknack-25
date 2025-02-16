using UnityEngine;
using UnityEngine.SceneManagement;  // Required for scene management

public class FinishPanelManager : MonoBehaviour
{
    public void ReplayLevel()
    {
        Debug.Log("Replay button clicked! Reloading the scene...");  // Debug to check if function is called
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);  // Reloads the current scene
    }
    

}

