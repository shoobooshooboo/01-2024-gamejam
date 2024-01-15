using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    enum Scenes
    {
        START,
        GAME,
        ABOUT,
        LOSE,
        WIN,
        PREGAME
    }
    public void PlayGame()
    {
        SceneManager.LoadScene((int)Scenes.GAME);
    }

    public void About()
    {
        SceneManager.LoadScene((int)Scenes.ABOUT);
    }

    public void EndGame()
    {
        SceneManager.LoadScene((int)Scenes.START);
    }

    public void PREGAME()
    {
        SceneManager.LoadScene((int)Scenes.PREGAME);
    }

    public void WIN()
    {
        SceneManager.LoadScene((int)Scenes.WIN);
    }

    public void LOSE()
    {
        SceneManager.LoadScene((int)Scenes.LOSE);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
