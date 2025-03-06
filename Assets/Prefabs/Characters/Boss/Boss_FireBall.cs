using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_FireBall : MonoBehaviour
{
    public float projectileSpeed;

    private Rigidbody fireballRigidbody;

    public GameObject fireballCastAudio;

    public GameObject fireballExplosionAudio;

    public int damage = 30;

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
        if (other.CompareTag("Player"))
        {
            GameObject explosion = Instantiate(fireballExplosionAudio, other.transform.position, Quaternion.identity);
            Destroy(explosion, 0.5f);
            PlayerStats playerStats = other.gameObject.GetComponent<PlayerStats>();
            playerStats.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
