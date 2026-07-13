using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition2 : MonoBehaviour
{
    [SerializeField] private string transitionTo;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector2 exitDirection;
    [SerializeField] private float exitTime;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("GameManager: " + (GameManager.Instance != null));
        //Debug.Log("Playercontroller: " + (Playercontroller.Instance != null));
        //Debug.Log("Start Point: " + (startPoint != null));
        //Debug.Log("UIManager: " + (UIManager.Instance != null));
        //Debug.Log("Scene Fader: " + (UIManager.Instance != null && UIManager.Instance.sceneFader != null));

        if(transitionTo == GameManager.Instance.transitionedFromScene)
        {   
            Playercontroller.Instance.transform.position = startPoint.position;

            StartCoroutine(Playercontroller.Instance.WalkIntoNewScene(exitDirection, exitTime));
        }
        //StartCoroutine(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
    }

    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player"))
        {
            GameManager.Instance.transitionedFromScene = SceneManager.GetActiveScene().name;

            //Playercontroller.Instance.pState.cutscene = true;
            
            SceneManager.LoadScene("Boss Level 2");
        }
    }
}
