using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AllPlayerText : MonoBehaviour
{
    public TMP_Text moneyText;
    public TMP_Text titleText;
    public TMP_Text hpText;
    public TMP_Text mpText;
    public TMP_Text expText;
    public TMP_Text levelText;
    public TMP_Text damageText;
    public TMP_Text armorText;
    public TMP_Text traitsText;
    public Slider potion1Slider;
    public Slider potion2Slider;


    private PlayerStats playerStats;

    private void Start()
    {
        playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        potion1Slider.maxValue = playerStats.potionsTime;
        potion2Slider.maxValue = playerStats.potionsTime;
    }

    private void Update()
    {
        moneyText.text = playerStats.money.ToString();
        titleText.text = playerStats.MyTitle;
        hpText.text = string.Format("HP: {0}/{1}", playerStats.MyCurrentHealth, playerStats.maxHealth);
        mpText.text = string.Format("MP: {0}/{1}", playerStats.currentMana, playerStats.maxMana);
        levelText.text = string.Format("Lvl: {0}", playerStats.level);
        expText.text = string.Format("Exp: {0}/{1}",playerStats.currentExp, playerStats.maxExp);
        damageText.text = string.Format("Obrażenia: {0}",playerStats.damage.GetValue());
        armorText.text = string.Format("Pancerz: {0}", playerStats.armor.GetValue());

        potion1Slider.value = playerStats.MyPotion1Cooldown;
        potion2Slider.value = playerStats.MyPotion2Cooldown;

        if (playerStats.MyPotion1Cooldown <= 0f)
        {
            potion1Slider.gameObject.SetActive(false);
        }
        else
        {
            potion1Slider.gameObject.SetActive(true);
        }

        if (playerStats.MyPotion2Cooldown <= 0f)
        {
            potion2Slider.gameObject.SetActive(false);
        }
        else
        {
            potion2Slider.gameObject.SetActive(true);
        }
    }
}
