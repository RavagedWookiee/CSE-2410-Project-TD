using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //add a reference for each map
    //for now just three maps with differnt enemy patterns
    public void Easy(string Game) {

        SceneManager.LoadScene(Game);
    }

    public void Medium(string Game) {

        SceneManager.LoadScene(Game);
    }

    public void Hard(string Game) {

        SceneManager.LoadScene(Game);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Player Has Quit The Game");
    }
}
