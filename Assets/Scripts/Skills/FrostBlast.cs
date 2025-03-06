using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FrostBlast : MonoBehaviour
{
    Collider[] objects;

    List<EnemyController> enemies = new List<EnemyController>();

    Boss boss;

    public GameObject frostblastCastAudio;

    private float freezeTime;

    private float freezeRadius;

    private void Start()
    {
        freezeTime = SpellSystem.instance.MyFreezeTime;
        freezeRadius = SpellSystem.instance.MyFreezeRadius;
        ParticleSystem part = gameObject.GetComponentInChildren<ParticleSystem>();
        part.gameObject.transform.localScale = new Vector3(freezeRadius * 2, part.gameObject.transform.localScale.y, freezeRadius * 2);
        objects = Physics.OverlapSphere(gameObject.transform.position, freezeRadius);
        GameObject cast = Instantiate(frostblastCastAudio, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Destroy(cast, 2f);

        foreach (Collider obj in objects)
        {
            if (obj.CompareTag("Enemy") && obj.GetComponent<EnemyController>() != null)
            {
                enemies.Add(obj.GetComponent<EnemyController>());
            }
            else if(obj.GetComponent<Boss>())
            {
                boss = obj.GetComponent<Boss>();
                MessageFeedManager.MyInstance.WriteMessage("Przeciwnik jest zbyt silny, efekt został osłabiony");
            }
        }

        StartCoroutine(Expire(freezeTime));
    }

    private void Update()
    {
        foreach (EnemyController enemy in enemies)
        {
            enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            enemy.isFrozen = true;
        }

        if (boss)
        {
            boss.gameObject.GetComponent<NavMeshAgent>().ResetPath();
            boss.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        }
    }

    IEnumerator Expire(float delay)
    {
        yield return new WaitForSeconds(delay);

        foreach (EnemyController enemy in enemies)
        {
            if (enemy)
            {
                enemy.isFrozen = false;
                enemy.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
            }
        }

        if (boss)
        {
            boss.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        }
        Destroy(gameObject);
    }
}
