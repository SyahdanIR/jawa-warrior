using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class GolemEvent : MonoBehaviour
{
    void SlashDamagePlayer()
    {
        if (Playercontroller.Instance.transform.position.x > transform.position.x ||
        Playercontroller.Instance.transform.position.x < transform.position.x)
        {
            Hit(Golem.Instance.SideAttackTransform, Golem.Instance.SideAttackArea);
        }
    }
    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] _ObjectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0);
        for (int i = 0; i < _ObjectsToHit.Length; i++)
        {
            if (_ObjectsToHit[i].GetComponent<Playercontroller>() != null)
            {
                _ObjectsToHit[i].GetComponent<Playercontroller>().TakeDamage(2);
            }
        }
    }
    void BendDownCheck()
    {
        if (Golem.Instance.barrageAttack)
        {
            StartCoroutine(BarrageAttackTransition());
        }
    }
    void BarrageOrOutbreak()
    {
        if (Golem.Instance.barrageAttack)
        {
            Golem.Instance.StartCoroutine(Golem.Instance.Barrage());
        }
    }
    IEnumerator BarrageAttackTransition()
    {
        yield return new WaitForSeconds(1f);
        Golem.Instance.anim.SetBool("Throw", true);
    }
    void DestroyAfterDeath()
    {
        Golem.Instance.DestroyAfterDeath();
    }
}
