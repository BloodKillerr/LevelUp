using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blessing : MonoBehaviour
{
    private int healAmount;

    public GameObject blessingCastAudio;

    void Start()
    {
        GameObject cast = Instantiate(blessingCastAudio, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        healAmount = SpellSystem.instance.MyHealAmount;

        PlayerStats ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

        ps.MyCurrentHealth += healAmount;

        if(ps.MyCurrentHealth > ps.maxHealth)
        {
            ps.MyCurrentHealth = ps.maxHealth;
        }

        Destroy(gameObject, 4.5f);
    }
}
