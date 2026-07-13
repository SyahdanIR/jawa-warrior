using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Idle : StateMachineBehaviour
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
        rb.velocity = Vector2.zero;

    float distance = Vector2.Distance(Playercontroller.Instance.transform.position, rb.position);

    if (distance > goblin.Instance.chaseRange)
    {
        animator.SetBool("Chase", false);
        return;
    }

    if (distance > goblin.Instance.attackRange && distance <= goblin.Instance.chaseRange)
    {
        animator.SetBool("Chase", true);
        return;
    }

    // Jarak dekat, waktunya serang
    if (distance <= goblin.Instance.attackRange && goblin.Instance.attackCountdown <= 0)
    {
        goblin.Instance.AttackHandler();
        goblin.Instance.attackCountdown = goblin.Instance.attackTimer;
    }
    }
    void RunToPlayer(Animator animator)
    {
        float distance = Vector2.Distance(Playercontroller.Instance.transform.position, rb.position);
        if (distance >= goblin.Instance.attackRange && distance <= goblin.Instance.chaseRange)
        {
                animator.SetBool("Chase", true);
        }
        else if (distance > goblin.Instance.chaseRange)
        {
            animator.SetBool("Chase", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
