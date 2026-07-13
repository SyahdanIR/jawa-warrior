using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1Management : MonoBehaviour
{
    // Start is called before the first frame update
    public void RestartLvl1()
    {
        SceneManager.LoadSceneAsync("Level 1");
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadSceneAsync("Main Menu");
    }
}
