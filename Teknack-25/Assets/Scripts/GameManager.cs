using UnityEngine;
using UnityEngine.UI;

// public class GameManager : MonoBehaviour
// {
//     public static GameManager Instance;
//     public int totalCoins = 0;
//     public Text coinText;  // Assign UI Text element in Inspector
//     private const string COIN_KEY = "Coins";

//     private void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//         }
//     }

//     private void Start()
//     {
//         totalCoins = PlayerPrefs.GetInt(COIN_KEY, 0);
//         UpdateCoinUI();
//     }

//     public void RewardPerfectDrift()
//     {
//         totalCoins += 3;  // Give 3 coins per perfect drift
//         UpdateCoinUI();
//         PlayerPrefs.SetInt(COIN_KEY, totalCoins);
//     }

//     public void UpdateCoinUI()
//     {
//         if (coinText != null)
//         {
//             coinText.text = "Coins: " + totalCoins;
//         }
//     }
// }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int coins = 0;
    public Text coinText; // Assign UI text in Inspector

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void RewardPerfectDrift()
    {
        coins += 10; // Reward 10 coins
        Debug.Log("Coins: " + coins);

        if (coinText != null)
            coinText.text = "Coins: " + coins; // Update UI
    }
}
