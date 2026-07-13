using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnBoss1 : MonoBehaviour
{
    [SerializeField] GameObject boss;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            boss.SetActive(true);
            Destroy(gameObject);
        }
    }
}
