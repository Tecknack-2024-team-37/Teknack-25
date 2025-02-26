using UnityEngine;

public class GameManager_2 : MonoBehaviour
{
    public static GameManager_2 Instance;

    [Header("Game Settings")]
    public bool isMultiplayer = false;  // True if multiplayer is selected
    public int playerCount = 1;  // Number of players (1, 2, 3, or 4)

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures GameManager_2 persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetGameMode(int numPlayers)
    {
        playerCount = numPlayers;
        isMultiplayer = (numPlayers > 1);
    }
}
