using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballProjectile : MonoBehaviour
{
    public float projectileSpeed;

    private Rigidbody fireballRigidbody;

    public GameObject fireballCastAudio;

    public GameObject fireballExplosionAudio;

    private int damage;

    void Start()
    {
        fireballRigidbody = GetComponent<Rigidbody>();
        fireballRigidbody.velocity = transform.forward * projectileSpeed;
        GameObject cast = Instantiate(fireballCastAudio, gameObject.transform.position, Quaternion.identity, gameObject.transform);
        Destroy(cast, .6f);

        Destroy(gameObject, 1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        damage = SpellSystem.instance.MyFireballDamage;
        if (other.CompareTag("Enemy"))
        {
            GameObject explosion = Instantiate(fireballExplosionAudio, other.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            EnemyStats enemyStats = other.gameObject.GetComponent<EnemyStats>();
            enemyStats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
