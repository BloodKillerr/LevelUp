using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCombat : MonoBehaviour
{
    public GameObject specialAttackAudio;

    CharacterStats enemyStats;
    CharacterStats myStats;
    CharacterCombat CC;

    NavMeshAgent enemyAgent;

    float distance;
    bool isInDistance = false;
    public bool normalAttack = true;
    public bool specialAttack = true;

    private float specialAttackCooldown = 0f;
    public float specialAttackDelay = 0.4f;

    private void Start()
    {
        CC = gameObject.GetComponent<CharacterCombat>();
        myStats = GetComponent<CharacterStats>();
    }

    private void FixedUpdate()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit) && hit.collider.gameObject.CompareTag("Enemy"))
        {
            enemyStats = hit.collider.gameObject.GetComponent<CharacterStats>();
            enemyAgent = hit.collider.gameObject.GetComponent<NavMeshAgent>();
            distance = Vector3.Distance(gameObject.transform.position, hit.collider.gameObject.transform.position);

            if (Mathf.Round(distance) <= enemyAgent.stoppingDistance)
            {
                isInDistance = true;
            }
            else
            {
                isInDistance = false;
            }
        }
        else
        {
            isInDistance = false;
            enemyStats = null;
            enemyAgent = null;
            distance = 0f;
        }
    }

    private void Update()
    {
        specialAttackCooldown -= Time.deltaTime;

        if (Input.GetMouseButtonDown(0) && normalAttack)
        {
            if (isInDistance && Time.timeScale != 0f && enemyStats)
            {
                Interact();
            }
        }
        else if (Input.GetMouseButtonDown(1) && specialAttack)
        {
            if (isInDistance && Time.timeScale != 0f && enemyStats)
            {
                SpecialAttack(enemyStats);
            }
        }
    }

    public void Interact()
    {
        if (CC != null && EquipmentManager.instance.currentEquipment[5] != null)
        {
            CC.Attack(enemyStats);
        }
    }

    public void AllowAttack()
    {
        normalAttack = true;
        specialAttack = true;
    }

    public void ForbidAttack()
    {
        normalAttack = false;
        specialAttack = false;
    }

    public void SpecialAttack(CharacterStats targetStats)
    {
        if (specialAttackCooldown <= 0f && EquipmentManager.instance.currentEquipment[5] != null)
        {
            if (gameObject.GetComponent<Animator>())
            {
                gameObject.GetComponent<Animator>().SetTrigger("specialAttack");
            }

            StartCoroutine(DoDamage(targetStats, specialAttackDelay));

            specialAttackCooldown = 4f;

            GameObject go = Instantiate(specialAttackAudio, gameObject.transform.position, Quaternion.identity);
            Destroy(go, .5f);
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue() + Mathf.RoundToInt(myStats.damage.GetValue()/5));
    }
}
