using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Eksperyment_SpecialAttack : StateMachineBehaviour
{
    public float lookRadius = 30f;
    float distance;

    Transform target;
    public NavMeshAgent agent;
    CharacterCombat combat;
    public GameObject fireballProjectile;
    public bool canUseSpecial = true;

    public GameObject[] positions;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = PlayerManager.instance.player.transform;
        agent = animator.GetComponent<NavMeshAgent>();
        combat = animator.GetComponent<CharacterCombat>();
        positions = GameObject.FindGameObjectsWithTag("EksperymentPosition");
        animator.SetBool("walking", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        distance = Vector3.Distance(target.position, animator.transform.position);
        Vector3 direction;
        Quaternion lookRotation;

        if (distance <= 10f)
        {
            direction = (target.position - animator.transform.position).normalized;
            lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);
            animator.SetTrigger("attack");
            agent.ResetPath();

            if (canUseSpecial)
            {
                Vector3 dir = (target.position - animator.transform.position).normalized;
                Quaternion lookRot = Quaternion.LookRotation(new Vector3(dir.x, 0, dir.z));
                animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRot, 1);
                SpecialAttack();
                canUseSpecial = false;
            }
        }
        else if (distance <= lookRadius && !agent.isStopped)
        {
            agent.SetDestination(target.position);
            animator.SetBool("walking", true);
        }

        direction = (target.position - animator.transform.position).normalized;
        lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        animator.transform.rotation = Quaternion.Slerp(animator.transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
        animator.SetBool("walking", true);
        canUseSpecial = true;
    }

    public void StopWalking()
    {
        agent.isStopped = true;
    }

    public void StartWalking()
    {
        agent.isStopped = false;
    }

    public void SpecialAttack()
    {
        foreach(GameObject pos in positions)
        {
            Instantiate(fireballProjectile, pos.transform.position, pos.transform.rotation);
        }
    }
}
