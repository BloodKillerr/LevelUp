using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;

    [SerializeField]
    private int currentHealth;

    public Stat damage;
    public Stat armor;

    public float attackSpeed = 1f;

    public int MyCurrentHealth { get => currentHealth; set => currentHealth = value; }

    private void Awake()
    {
        MyCurrentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        damage -= armor.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue);

        MyCurrentHealth -= damage;

        if (MyCurrentHealth <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log(transform.name + " died!");
    }
}
