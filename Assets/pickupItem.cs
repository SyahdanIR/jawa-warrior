using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SkillType { Dash, Heal, Fireball }

public class pickupItem : MonoBehaviour
{
    public SkillType skillType;

    void Start()
    {
        StartCoroutine(DespawnAfterTime(10f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            pickUp_Manager.Instance.ActivateSkillButton(skillType);
            Destroy(gameObject);
        }
    }
    
    IEnumerator DespawnAfterTime(float delay) {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}

