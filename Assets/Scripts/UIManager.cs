using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Text Round;
    public Text Livesleft;
    public Text CurrencyAmount;
    public static int curr_Rounds;
    public GameObject settingsPopup;

    void Start()
    {
        // Initially hide pop-ups
        settingsPopup.SetActive(false);
    }

    void Update()
    {
        if (GameManager.currentRound <= 1)
        {
            curr_Rounds = 1;
        }
        else {
            curr_Rounds = GameManager.currentRound;
        }

        Round.text = "Round: " + curr_Rounds + "/" + GameManager.maxRounds;
        Livesleft.text = "❤️ " + PlayerStats.Lives.ToString();
        CurrencyAmount.text = "$" + PlayerStats.Currency.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Settings();
        }
    }

    public void TogglePause()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }
    
    public void FastForward()
    {
        if (Time.timeScale == 1f)
        {
            Time.timeScale = 2f; // Speed up the game
        }
        else
        {
            Time.timeScale = 1f; // Resume the game
        }
    }

    public void Settings()
    {
        if (Time.timeScale != 0f)
        {
            Time.timeScale = 0f;
            settingsPopup.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            settingsPopup.SetActive(false);
        }        
    }
    public void Test ()
    {
        Debug.Log("Pressed");
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        settingsPopup.SetActive(false);
    }
}
