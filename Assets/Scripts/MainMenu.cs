using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public void startGame()
    {
        SceneManager.LoadScene("Central Hub");
    }
    public void exitGame()
    {
        Application.Quit();
    }
    public void mainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void resume()
    {
        PlayerManager.Instance.pause = false;
    }
    public void central()
    {
        SceneManager.LoadScene("Central Hub");
    }
}
