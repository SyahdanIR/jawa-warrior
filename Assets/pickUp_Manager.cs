using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp_Manager : MonoBehaviour
{
    public static pickUp_Manager Instance;
    public GameObject dashButton, healButton, fireballButton;

    void Awake() {
        Instance = this;
    }

    public void ActivateSkillButton(SkillType skill) {
        GameObject button = null;
        switch (skill) {
            case SkillType.Dash: button = dashButton; break;
            case SkillType.Heal: button = healButton; break;
            case SkillType.Fireball: button = fireballButton; break;
        }

        if (button != null) {
            button.SetActive(true);
            StartCoroutine(DeactivateAfterTime(button, 15f));
        }
    }

    IEnumerator DeactivateAfterTime(GameObject button, float delay) {
        yield return new WaitForSeconds(delay);
        button.SetActive(false);
    }
}

