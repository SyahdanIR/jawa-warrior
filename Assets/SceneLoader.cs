using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;
    [SerializeField] GameObject satu;
    [SerializeField] GameObject dua;
    [SerializeField] GameObject tiga;
    [SerializeField] GameObject empat;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadScene(string sceneName)
    {
        // Hapus semua object yang diberi PersistentFlag
        if (sceneName == "Main Menu") //|| sceneName.StartsWith("Level"))
        {
            StartCoroutine(UIManager.Instance.DeactivateCanvas());
            Destroy(satu);
            Destroy(dua);
            Destroy(tiga);
            Destroy(empat);
        }
        if (sceneName == "CleanUpScene2" || sceneName == "CleanUpScene3" || sceneName == "CleanUpEndingScene" || sceneName == "CleanUpScene3Hidden" || sceneName == "CleanUpEndingHidden") //|| sceneName.StartsWith("Level"))
        {
            StartCoroutine(UIManager.Instance.DeactivateVictoryScreen());
            Destroy(satu);
            Destroy(dua);
            Destroy(tiga);
            Destroy(empat);
            if (sceneName == "CleanUpScene2")
            {
                PlayerPrefs.SetInt("Score_Level_1", ScoreManager.Instance.score); //set skor level 1
                PlayerPrefs.Save();
            }
            if (sceneName == "CleanUpScene3" || sceneName == "CleanUpScene3Hidden")
            {
                PlayerPrefs.SetInt("Score_Level_2", ScoreManager.Instance.score); //set skor level 2
                PlayerPrefs.Save();
            }
            if (sceneName == "CleanUpEndingScene")
            {
                PlayerPrefs.SetInt("Score_Level_3", ScoreManager.Instance.score); //set skor level 2
                PlayerPrefs.Save();
            }
            if (sceneName == "CleanUpEndingHidden")
            {
                PlayerPrefs.SetInt("Score_Level_3", ScoreManager.Instance.score+500); //set skor level 2
                PlayerPrefs.Save();
            }
        }
        StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
        SceneManager.LoadScene(sceneName);
    }
    public void LoadLevel2(string sceneName)
    {
        // Hapus semua object yang diberi PersistentFlag
        StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
        SceneManager.LoadScene(sceneName);
    }

    void CleanupPersistentObjects()
    {
        PersistentFlag[] persistents = FindObjectsOfType<PersistentFlag>();
        //Debug.Log("Cleaning up " + persistents.Length + " persistent objects.");

        foreach (var obj in persistents)
        {
            Destroy(obj.gameObject);
        }
    }
}
