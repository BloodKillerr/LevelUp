using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    bool secPhase = false;
    EnemyStats mystats;

    private void Start()
    {
        mystats = gameObject.GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(mystats.MyCurrentHealth <= mystats.maxHealth/2 && !secPhase)
        {
            secPhase = true;           
            gameObject.GetComponent<Animator>().SetBool("2phase", true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 16);
    }
}
