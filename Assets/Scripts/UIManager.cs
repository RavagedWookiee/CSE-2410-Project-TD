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

    void Update()
    {
        if (WaveSpawner.wave <= 1)
        {
            curr_Rounds = 1;
        }
        else {
            curr_Rounds = WaveSpawner.wave;
        }

        Round.text = "Round: " + curr_Rounds + "/100";
        Livesleft.text = "❤️ " + PlayerStats.Lives.ToString();
        CurrencyAmount.text = "$" + PlayerStats.Currency.ToString();
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
}
