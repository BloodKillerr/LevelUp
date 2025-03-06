using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FinalBoss_SpecialAttack : StateMachineBehaviour
{
    public float lookRadius = 50f;
    float distance;

    Transform target;
    public NavMeshAgent agent;
    CharacterCombat combat;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = PlayerManager.instance.player.transform;
        agent = animator.GetComponent<NavMeshAgent>();
        combat = animator.GetComponent<CharacterCombat>();
        animator.SetBool("walking", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(target.position, animator.transform.position);

        if (Mathf.Round(distance) <= agent.stoppingDistance)
        {
            agent.ResetPath();
            CharacterStats targetStats = target.GetComponent<CharacterStats>();
            if (targetStats != null)
            {
                Vector3 dir = (target.position - animator.transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRot, 1);
                combat.BossAttack(targetStats, 300);
            }

            Vector3 direction = (target.position - animator.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        else if (distance <= lookRadius && !agent.isStopped)
        {
            agent.SetDestination(target.position);
            animator.SetBool("walking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.SetBool("walking", false);
    }

    public void StopWalking()
    {
        agent.isStopped = true;
    }

    public void StartWalking()
    {
        agent.isStopped = false;
    }
}
