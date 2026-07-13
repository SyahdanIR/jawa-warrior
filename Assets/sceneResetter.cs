using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneResetter : MonoBehaviour
{
    [Tooltip("Scene tujuan setelah membersihkan DontDestroyOnLoad objects")]
    public string nextSceneName = "Level 1";  // Ganti sesuai target level kamu

    void Start()
    {
        StartCoroutine(ResetAndLoad());
    }

    IEnumerator ResetAndLoad()
    {
        // Tunggu satu frame untuk memastikan semua objek DontDestroyOnLoad sudah termuat
        yield return null;

        GameObject[] allObjects = FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Objek DontDestroyOnLoad biasanya tidak punya scene atau scene name kosong
            if (obj.scene.name == null || obj.scene.name == "")
            {
                if (obj != this.gameObject) // Jangan hancurkan self
                    Destroy(obj);
            }
        }

        // Tunggu 1 frame lagi agar semua objek terhapus
        yield return null;

        // Hancurkan juga SceneResetter ini sendiri

        // Load scene target
        SceneManager.LoadScene(nextSceneName, LoadSceneMode.Single);
    }
}
