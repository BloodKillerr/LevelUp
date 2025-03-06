using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCombat : MonoBehaviour
{
    private float attackCooldown = 0f;

    public GameObject attackAudio;

    public float attackDelay = 0.6f;

    public event System.Action OnAttack;

    CharacterStats myStats;

    private void Start()
    {
        myStats = GetComponent<CharacterStats>();    
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            if (gameObject.GetComponent<Animator>())
            {
                gameObject.GetComponent<Animator>().SetTrigger("attack");
            }

            StartCoroutine(DoDamage(targetStats, attackDelay));

            OnAttack?.Invoke();

            attackCooldown = 1f / myStats.attackSpeed;

            GameObject go = Instantiate(attackAudio, gameObject.transform.position, Quaternion.identity);
            Destroy(go, .5f);
        }
    }

    public void BossAttack(CharacterStats targetStats, int boost)
    {
        if (attackCooldown <= 0f)
        {
            if (gameObject.GetComponent<Animator>())
            {
                gameObject.GetComponent<Animator>().SetTrigger("attack");
            }

            StartCoroutine(DoBossDamage(targetStats, attackDelay, boost));

            OnAttack?.Invoke();

            attackCooldown = 1f / myStats.attackSpeed;

            GameObject go = Instantiate(attackAudio, gameObject.transform.position, Quaternion.identity);
            Destroy(go, .5f);
        }
    }

    IEnumerator DoDamage(CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue());
    }

    IEnumerator DoBossDamage(CharacterStats stats, float delay, int boost)
    {
        yield return new WaitForSeconds(delay);
        stats.TakeDamage(myStats.damage.GetValue() + boost);
    }
}
