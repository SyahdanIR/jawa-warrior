using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;

public class ButoIjoEvent : MonoBehaviour
{
    void SlashDamagePlayer()
    {
        if (Playercontroller.Instance.transform.position.x > transform.position.x ||
        Playercontroller.Instance.transform.position.x < transform.position.x)
        {
            Hit(ButoIjo.Instance.SideAttackTransform, ButoIjo.Instance.SideAttackArea);
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
        if (ButoIjo.Instance.barrageAttack)
        {
            StartCoroutine(BarrageAttackTransition());
        }
    }
    void BarrageOrOutbreak()
    {
        if (ButoIjo.Instance.barrageAttack)
        {
            ButoIjo.Instance.StartCoroutine(ButoIjo.Instance.Barrage());
        }
    }
    IEnumerator BarrageAttackTransition()
    {
        yield return new WaitForSeconds(1f);
        ButoIjo.Instance.anim.SetBool("Throw", true);
    }
    void DestroyAfterDeath()
    {
        ButoIjo.Instance.DestroyAfterDeath();
    }
}
