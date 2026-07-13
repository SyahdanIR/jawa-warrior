using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextLevel : MonoBehaviour
{
    [Tooltip("Scene tujuan setelah membersihkan DontDestroyOnLoad objects")]
    public string nextSceneName = "Level 1";  // Ganti sesuai target level kamu
    public void LevelBerikutnya()
    {
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}