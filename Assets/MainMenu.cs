using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    void Start()
    {
    
        PlayerPrefs.DeleteKey("score_level_1");
        PlayerPrefs.DeleteKey("score_level_2");
        PlayerPrefs.DeleteKey("score_level_3");

        PlayerPrefs.Save();
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("Prolog");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
