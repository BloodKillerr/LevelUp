using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerStats : CharacterStats
{
    PotionsUsage potionUsage;

    LevelingSystem levelingSystem;

    [SerializeField]
    private string title = "Początkujący";

    public int maxMana = 100;
    public int currentMana;

    public int maxExp;
    public int currentExp;
    public int level;

    public int money = 0;

    public string MyTitle { get => title; set => title = value; }
    public float MyPotion1Cooldown { get => potion1Cooldown; set => potion1Cooldown = value; }
    public float MyPotion2Cooldown { get => potion2Cooldown; set => potion2Cooldown = value; }

    public GameObject healParticle;
    public GameObject manaParticle;
    public GameObject potionUseAudioObj;

    [SerializeField]
    private float potion1Cooldown = 0f;

    [SerializeField]
    private float potion2Cooldown = 0f;

    public float potionsTime = 2.5f;

    private void Awake()
    {
        currentMana = maxMana;
        MyCurrentHealth = maxHealth;      
    }

    void Start()
    {
        levelingSystem = LevelingSystem.instance.GetComponent<LevelingSystem>();
        //EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
        potionUsage = PotionsUsage.instance;
    }

    public void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            if ((int)newItem.equipSlot == 7 || (int)newItem.equipSlot == 8)
            {
                Accessory ac = newItem as Accessory;
                switch (ac.type)
                {
                    case 0:
                        SpellSystem.instance.MyFireballDamage += ac.fireDamage;
                        break;
                    case 1:
                        SpellSystem.instance.MyFreezeTime += ac.frostDuration;
                        break;
                    case 2:
                        SpellSystem.instance.MyFreezeRadius += ac.frostRadius;
                        break;
                    case 3:
                        SpellSystem.instance.MyHealAmount += ac.healAmount;
                        break;
                    case 4:
                        damage.AddModifier(ac.damageModifier);
                        break;
                    case 5:
                        armor.AddModifier(ac.armorModifier);
                        break;
                    default:
                        Debug.Log("Nie działa");
                        break;
                }
            }
            else
            {
                armor.AddModifier(newItem.armorModifier);
                damage.AddModifier(newItem.damageModifier);
            }
        }

        if (oldItem != null)
        {
            if ((int)oldItem.equipSlot == 7 || (int)oldItem.equipSlot == 8)
            {
                Accessory ac = oldItem as Accessory;
                switch (ac.type)
                {
                    case 0:
                        SpellSystem.instance.MyFireballDamage -= ac.fireDamage;
                        break;
                    case 1:
                        SpellSystem.instance.MyFreezeTime -= ac.frostDuration;
                        break;
                    case 2:
                        SpellSystem.instance.MyFreezeRadius -= ac.frostRadius;
                        break;
                    case 3:
                        SpellSystem.instance.MyHealAmount -= ac.healAmount;
                        break;
                    case 4:
                        damage.RemoveModifier(ac.damageModifier);
                        break;
                    case 5:
                        armor.RemoveModifier(ac.armorModifier);
                        break;
                    default:
                        Debug.Log("Nie działa");
                        break;
                }
            }
            else
            {
                armor.RemoveModifier(oldItem.armorModifier);
                damage.RemoveModifier(oldItem.damageModifier);
            }
        }
    }

    private void Update()
    {
        MyPotion1Cooldown -= Time.deltaTime;
        MyPotion2Cooldown -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Heal();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            RestoreMana();
        }

        currentExp = levelingSystem.currentExp;
        maxExp = levelingSystem.requiredExp;
        level = levelingSystem.level;
    }

    public void Heal()
    {
        if (potionUsage.currentPotions[0] != null && Time.timeScale != 0 && MyPotion1Cooldown <= 0f)
        {
            if (MyCurrentHealth < maxHealth)
            {
                MyCurrentHealth += potionUsage.currentPotions[0].healAmount;
                potionUsage.UsePotionAlt(0);
                GameObject hGO = Instantiate(healParticle, gameObject.transform.position, healParticle.transform.rotation, gameObject.transform);
                GameObject potionAudio = Instantiate(potionUseAudioObj, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                Destroy(hGO, 2.5f);
                Destroy(potionAudio, 1.5f);

                if (MyCurrentHealth > maxHealth)
                {
                    MyCurrentHealth = maxHealth;
                }

                MyPotion1Cooldown = potionsTime;
            }
        }
    }

    public void GainHeal(int x)
    {
        if (MyCurrentHealth < maxHealth)
        {
            MyCurrentHealth += x;

            if (MyCurrentHealth > maxHealth)
            {
                MyCurrentHealth = maxHealth;
            }
        }
    }

    public void GainMana(int x)
    {
        if (currentMana < maxMana)
        {
            currentMana += x;

            if (currentMana > maxMana)
            {
                currentMana = maxMana;
            }
        }
    }

    public void RestoreMana()
    {
        if(potionUsage.currentPotions[1] != null && Time.timeScale != 0 && MyPotion2Cooldown <= 0f)
        {
            if (currentMana < maxMana)
            {
                currentMana += potionUsage.currentPotions[1].manaAmount;
                potionUsage.UsePotionAlt(1);
                GameObject mGO = Instantiate(manaParticle, gameObject.transform.position, manaParticle.transform.rotation, gameObject.transform);
                GameObject potionAudio = Instantiate(potionUseAudioObj, gameObject.transform.position, Quaternion.identity, gameObject.transform);
                Destroy(mGO, 2.5f);
                Destroy(potionAudio, 1.5f);

                if (currentMana > maxMana)
                {
                    currentMana = maxMana;
                }

                MyPotion2Cooldown = potionsTime;
            }
        }      
    }

    public override void Die()
    {
        base.Die();
        PlayerManager.instance.KillPlayer();
    }
}