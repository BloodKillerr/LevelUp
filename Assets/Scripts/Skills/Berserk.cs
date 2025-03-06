using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Berserk : MonoBehaviour
{
    private float buffTime;

    private int damageBuff;

    private PlayerStats ps;

    public GameObject berserkCastAudio;

    void Start()
    {
        GameObject cast = Instantiate(berserkCastAudio, gameObject.transform.position, Quaternion.identity, gameObject.transform);

        buffTime = SpellSystem.instance.MyDamageBuffTime;

        ps = PlayerManager.instance.player.GetComponent<PlayerStats>();

        damageBuff = ps.damage.GetValue();

        ps.damage.AddModifier(damageBuff);

        StartCoroutine(Expire(buffTime));
    }

    IEnumerator Expire(float delay)
    {
        yield return new WaitForSeconds(delay);

        ps.damage.RemoveModifier(damageBuff);
        Destroy(gameObject);
    }
}
