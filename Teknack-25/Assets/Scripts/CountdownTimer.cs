using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CountdownTimer : MonoBehaviour
{
    public int countdownTime = 3;  
    public Text countdownDisplay;  

    private void Start()
    {
        Time.timeScale = 0;  // Pause game at the start
        DisablePlayerControls();
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            countdownDisplay.text = countdownTime.ToString();
            yield return new WaitForSecondsRealtime(1f); // Use WaitForSecondsRealtime to ignore timeScale
            countdownTime--;
        }

        countdownDisplay.text = "GO!!!";
        
        EnablePlayerControls();
        
        Time.timeScale = 1;  // Resume the game (unpause everything)

        yield return new WaitForSeconds(1f);
        countdownDisplay.gameObject.SetActive(false);
    }

    void DisablePlayerControls()
    {
        CarController player = FindObjectOfType<CarController>();
        if (player != null)
        {
            player.enabled = false;
        }
    }

    void EnablePlayerControls()
    {
        CarController player = FindObjectOfType<CarController>();
        if (player != null)
        {
            player.enabled = true;
        }
    }
}
