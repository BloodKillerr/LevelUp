using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    float distance;
    
    Transform target;
    public NavMeshAgent agent;
    CharacterCombat combat;

    public bool isFrozen = false;

    public GameObject healthBar;
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        combat = GetComponent<CharacterCombat>();
    }

    void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if(distance <= lookRadius && !agent.isStopped)
        {
            healthBar.SetActive(true);
            agent.SetDestination(target.position);
            gameObject.GetComponent<Animator>().SetBool("walking", true);

            if(Mathf.Round(distance) <= agent.stoppingDistance & !isFrozen)
            {
                CharacterStats targetStats = target.GetComponent<CharacterStats>();
                if(targetStats != null)
                {
                    combat.Attack(targetStats);
                }

                FaceTarget();
            }

        }
        else if(!(distance <= lookRadius) && !agent.isStopped)
        {
            healthBar.SetActive(false);
        }

        if(agent.velocity.magnitude < 0.15f)
        {
            gameObject.GetComponent<Animator>().SetBool("walking", false);
        }
    }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void SnapToTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 1);
    }

    public void StopWalking()
    {
        agent.isStopped = true;
    }

    public void StartWalking()
    {
        agent.isStopped = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
