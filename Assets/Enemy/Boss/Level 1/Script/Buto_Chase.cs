using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buto_Chase : StateMachineBehaviour
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
        TargetPlayerPosition(animator);

        if (ButoIjo.Instance.attackCountdown <= 0)
        {
            ButoIjo.Instance.AttackHandler();
            ButoIjo.Instance.attackCountdown = ButoIjo.Instance.attackTimer;
        }
    }
    void TargetPlayerPosition(Animator animator)
    {
        ButoIjo.Instance.Flip();
        Vector2 _target = new Vector2(Playercontroller.Instance.transform.position.x, rb.position.y);
        Vector2 _newPos = Vector2.MoveTowards(rb.position, _target, ButoIjo.Instance.runSpeed * Time.fixedDeltaTime);
        ButoIjo.Instance.runSpeed = ButoIjo.Instance.speed;
        rb.MovePosition(_newPos);
        if (Vector2.Distance(Playercontroller.Instance.transform.position, rb.position) <= ButoIjo.Instance.attackRange)
        {
            animator.SetBool("Chase", false);
        }
        else
        {
            return;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Chase", false);
    }
}
