using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class wraithEvent : MonoBehaviour
{
    void SlashDamagePlayer()
    {
        if (Playercontroller.Instance.transform.position.x > transform.position.x ||
        Playercontroller.Instance.transform.position.x < transform.position.x)
        {
            Hit(wraith.Instance.SideAttackTransform, wraith.Instance.SideAttackArea);
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
        if (wraith.Instance.barrageAttack)
        {
            StartCoroutine(BarrageAttackTransition());
        }
    }
    void BarrageOrOutbreak()
    {
        if (wraith.Instance.barrageAttack)
        {
            wraith.Instance.StartCoroutine(wraith.Instance.Barrage());
        }
    }
    IEnumerator BarrageAttackTransition()
    {
        yield return new WaitForSeconds(1f);
        wraith.Instance.anim.SetBool("Throw", true);
    }
    void DestroyAfterDeath()
    {
        wraith.Instance.DestroyAfterDeath();
    }
}
