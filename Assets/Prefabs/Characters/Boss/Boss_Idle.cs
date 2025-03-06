using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Idle : StateMachineBehaviour
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
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(target.position, animator.transform.position);

        if (distance <= lookRadius && !agent.isStopped)
        {
            agent.SetDestination(target.position);
            animator.SetBool("walking", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
