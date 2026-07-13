using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if(_other.CompareTag("Player"))
        {
            StartCoroutine(RespawnPoint());
        }
    }

    IEnumerator RespawnPoint()
    {
        Playercontroller.Instance.pState.cutscene = true;
            Playercontroller.Instance.pState.invincible = true;
            Playercontroller.Instance.rb.velocity = Vector2.zero;
            Time.timeScale = 0;
            //StartCoroutine(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.In));
            Playercontroller.Instance.TakeDamage(1);
            yield return new WaitForSecondsRealtime(1f);
            Playercontroller.Instance.transform.position = GameManager.Instance.platformingRespawnPoint;
            //StartCoroutine(UIManager.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
            //yield return new WaitForSecondsRealtime(UIManager.Instance.sceneFader.fadeTime);
            Playercontroller.Instance.pState.cutscene = false;
            Playercontroller.Instance.pState.invincible = false;
            Time.timeScale = 1;
    }
}
