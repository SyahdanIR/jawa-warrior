using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] public SceneFader sceneFader;
    [SerializeField] GameObject deathScreen;
    [SerializeField] GameObject victoryScreen;
    [SerializeField] GameObject Canvas;
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

    public IEnumerator ActivateDeathScreen()
    {
        yield return new WaitForSeconds(0.8f);
        deathScreen.SetActive(true);
    }

    public IEnumerator DeactivateDeathScreen()
    {
        yield return new WaitForSeconds(0.1f);
        deathScreen.SetActive(false);
    }
    public IEnumerator ActivateVictoryScreen()
    {
        yield return new WaitForSeconds(1f);
        victoryScreen.SetActive(true);
    }

    public IEnumerator DeactivateVictoryScreen()
    {
        yield return new WaitForSeconds(0.1f);
        victoryScreen.SetActive(false);
    }
    public IEnumerator ActivateCanvas()
    {
        yield return new WaitForSeconds(0.1f);
        Canvas.SetActive(true);
    }

    public IEnumerator DeactivateCanvas()
    {
        yield return new WaitForSeconds(0.1f);
        Canvas.SetActive(false);
    }
}
