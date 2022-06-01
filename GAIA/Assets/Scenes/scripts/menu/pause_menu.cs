using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pause_menu : MonoBehaviour
{ 

    private GameObject[] otter;
    private GameObject[] snake;
    private GameObject[] wolf;
    private GameObject[] sheep;
    public void PlayGame()
    {
        Debug.Log("Continue");
        PlayerPrefs.SetInt("Back", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        
    }
    public void quitToMain()
    {
        Debug.Log("Quit to main menu");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }

    public void saveGame()
    {
        GameObject.Find("Save").GetComponentInChildren<TextMeshProUGUI>().text = "Animals Saved";
    }
    

}
