using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_menu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Continue");
        PlayerPrefs.SetInt("Back", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void newgame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void quit_game()
    {
        Debug.Log("Close Game");
        Application.Quit();
    }
}
