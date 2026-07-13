using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin_Chase : StateMachineBehaviour
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

        if (goblin.Instance.attackCountdown <= 0)
        {
            goblin.Instance.AttackHandler();
            goblin.Instance.attackCountdown = goblin.Instance.attackTimer;
        }
    }
    void TargetPlayerPosition(Animator animator)
    {
        goblin.Instance.Flip();

        float distance = Vector2.Distance(Playercontroller.Instance.transform.position, rb.position);

        if (distance > goblin.Instance.chaseRange)
        {
            animator.SetBool("Chase", false); // stop mengejar, balik ke idle
            return;
        }

        Vector2 _target = new Vector2(Playercontroller.Instance.transform.position.x, rb.position.y);
        Vector2 _newPos = Vector2.MoveTowards(rb.position, _target, goblin.Instance.runSpeed * Time.fixedDeltaTime);
        goblin.Instance.runSpeed = goblin.Instance.speed;
        rb.MovePosition(_newPos);

        if (distance <= goblin.Instance.attackRange)
        {
            animator.SetBool("Chase", false);
        }
    }


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Chase", false);
    }
}
