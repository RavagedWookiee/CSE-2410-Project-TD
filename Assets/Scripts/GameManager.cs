using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int maxRounds = 10;
    public static int currentRound = 0;

    public GameObject winPopup;
    public GameObject losePopup;


    void Start()
    {
        // Initially hide pop-ups
        winPopup.SetActive(false);
        losePopup.SetActive(false);
    }

     void Update()
    {
        if (PlayerStats.Lives <= 0)
        {
            Time.timeScale = 0f;
            LoseGame();
        }

        if (currentRound >= maxRounds && WaveSpawner.enemyCount == 0)
        {
            Time.timeScale = 0f;
            WinGame();
        }
    }

        void WinGame()
        {
            winPopup.SetActive(true);
        }

        void LoseGame()
        {
            losePopup.SetActive(true);
        }

}
