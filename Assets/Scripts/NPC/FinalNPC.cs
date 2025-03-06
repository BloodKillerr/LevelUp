using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalNPC : NPC
{
    public GameObject BossSpawn;
    public override void OnDestroy()
    {
        base.OnDestroy();

        Destroy(gameObject);
    }

    public void SpawnBoss()
    {
        Instantiate(BossSpawn, new Vector3(transform.position.x, transform.position.y, transform.position.z - 7f), Quaternion.identity);
    }
}
