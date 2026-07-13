using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Diagnostics;

public class GameManager : MonoBehaviour
{
    public string transitionedFromScene;
    public Vector2 platformingRespawnPoint;
    public Vector2 RespawnPoint;

    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public void RespawnPlayer()
    {
        RespawnPoint = platformingRespawnPoint;
        Playercontroller.Instance.transform.position = RespawnPoint;
        StartCoroutine(UIManager.Instance.DeactivateDeathScreen());
        Playercontroller.Instance.Respawned();
    }
}
