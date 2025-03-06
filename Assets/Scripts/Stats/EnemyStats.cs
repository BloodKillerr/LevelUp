using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyStats : CharacterStats
{
    [SerializeField]
    private string enemyName = null;

    [SerializeField]
    private int enemyID;

    private LootTable lootTable;

    [HideInInspector]
    public Slider healthBar;
    
    public TMP_Text healthText;
    public TMP_Text enemyNameText;

    public string MyEnemyName { get => enemyName;}
    public int MyEnemyID { get => enemyID; set => enemyID = value; }

    private void Start()
    {
        enemyNameText.text = MyEnemyName;
        healthBar = gameObject.GetComponentInChildren<Slider>();
        lootTable = gameObject.GetComponent<LootTable>();
    }

    private void Update()
    {
        healthBar.maxValue = maxHealth;
        healthBar.value = MyCurrentHealth;
        healthText.text = MyCurrentHealth + "/" + maxHealth;
    }

    public override void Die()
    {
        base.Die();

        MessageFeedManager.MyInstance.WriteMessage(string.Format("Zabito: {0}", enemyName));
        CombatEvents.EnemyDied(this);
        lootTable.GainLoot();
        Destroy(gameObject);
    }
}
