using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class keLevel1 : MonoBehaviour
{
    [SerializeField] private string namaScene; // Bisa diisi dari Inspector

    void OnEnable()
    {
        if (!string.IsNullOrEmpty(namaScene))
        {
            SceneManager.LoadScene(namaScene);
        }
        else
        {
            Debug.LogWarning("Nama scene belum diisi di Inspector.");
        }
    }
}
