using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager2 : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Text leaderboardText;
    
    private List<RaceResult> raceResults = new List<RaceResult>();

    public void PlayerFinishedRace(string playerName, float raceTime)
    {
        raceResults.Add(new RaceResult(playerName, raceTime));

        if (raceResults.Count >= 4) // Assuming 4 players
        {
            ShowLeaderboard();
        }
    }

    void ShowLeaderboard()
    {
        raceResults.Sort((a, b) => a.time.CompareTo(b.time)); // Sort by fastest time

        leaderboardText.text = "Race Results:\n";
        for (int i = 0; i < raceResults.Count; i++)
        {
            leaderboardText.text += (i + 1) + ". " + raceResults[i].playerName + " - " + raceResults[i].time.ToString("F2") + "s\n";
        }

        gameOverPanel.SetActive(true);
    }

    class RaceResult
    {
        public string playerName;
        public float time;

        public RaceResult(string name, float time)
        {
            playerName = name;
            this.time = time;
        }
    }
}
