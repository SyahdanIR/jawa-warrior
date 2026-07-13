using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wraith_Skill : StateMachineBehaviour
{
    Rigidbody2D rb;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponentInParent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.gravityScale = 0;
        int _dir = wraith.Instance.facingRight ? 1 : -1;
        rb.velocity = new Vector2(_dir * (wraith.Instance.speed * 6), 0f);

        if (Vector2.Distance(Playercontroller.Instance.transform.position, rb.position) <= wraith.Instance.attackRange &&
        !wraith.Instance.damagedPlayer)
        {
            Playercontroller.Instance.TakeDamage(3);
            wraith.Instance.damagedPlayer = true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
