using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class goblinEvent : MonoBehaviour
{
    void SlashDamagePlayer()
    {
        if (Playercontroller.Instance.transform.position.x > transform.position.x ||
        Playercontroller.Instance.transform.position.x < transform.position.x)
        {
            Hit(goblin.Instance.SideAttackTransform, goblin.Instance.SideAttackArea);
        }
    }
    void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider2D[] _ObjectsToHit = Physics2D.OverlapBoxAll(_attackTransform.position, _attackArea, 0);
        //if (_ObjectsToHit.GetComponent<Playercontroller>() != null && !Playercontroller.Instance.pState.invincible)
        //    {
        //        _ObjectsToHit.GetComponent<Playercontroller>().TakeDamage(2);
        //    }
        for (int i = 0; i < _ObjectsToHit.Length; i++)
        {
           if (_ObjectsToHit[i].GetComponent<Playercontroller>() != null)
           {
                _ObjectsToHit[i].GetComponent<Playercontroller>().TakeDamage(2);
           }
        }

    }
}
