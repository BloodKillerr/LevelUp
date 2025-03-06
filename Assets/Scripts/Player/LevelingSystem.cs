using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingSystem : MonoBehaviour
{
    #region Singleton

    public static LevelingSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField]
    public int currentExp=0;

    [SerializeField]
    public int requiredExp=100;

    [SerializeField]
    public int level = 1;

    [SerializeField]
    public int skillPoints = 0;

    [SerializeField]
    public int hpUpgrade = 50;

    [SerializeField]
    public int mpUpgrade = 15;

    public void GainExp(int x)
    {
        if(x > 0)
        {
            currentExp += x;
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto: {0} XP", x));

            while (currentExp >= requiredExp)
            {
                currentExp = currentExp - requiredExp;
                LevelUp();
            }
        }    
    }

    public void LevelUp()
    {
        level++;
        requiredExp += 25 * level;
        PlayerManager.instance.player.GetComponent<PlayerStats>().maxHealth += hpUpgrade;
        PlayerManager.instance.player.GetComponent<PlayerStats>().GainHeal(hpUpgrade);
        PlayerManager.instance.player.GetComponent<PlayerStats>().maxMana += mpUpgrade;
        PlayerManager.instance.player.GetComponent<PlayerStats>().GainMana(mpUpgrade);
        hpUpgrade += 15;
        skillPoints++;
        mpUpgrade += 15;

        MessageFeedManager.MyInstance.WriteMessage(string.Format("Osiągnięto {0} poziom", level));

        //Adding traits and other rewards on some levels;

        if(level == 5)
        {
            SpellSystem.instance.MySpell1Unlocked = true;
            SpellsDisplay.instance.UpdateSpellInfo();
            MessageFeedManager.MyInstance.WriteMessage("Odblokowano umiejętność: Kula ognia");
        }

        if(level == 10)
        {
            TraitsSystem.instance.GetComponent<TraitsSystem>().AddTrait("Żelazna skóra: + 10 pancerza");
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto cechę: {0}", "Żelazna skóra"));
            SpellSystem.instance.MySpell2Unlocked = true;
            SpellsDisplay.instance.UpdateSpellInfo();
            MessageFeedManager.MyInstance.WriteMessage("Odblokowano umiejętność: Podmuch mrozu");
        }

        if(level == 13)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().MyTitle = "Rosnący w sile";
            MessageFeedManager.MyInstance.WriteMessage("Zdobyto tytuł: Rosnący w sile");
            SpellSystem.instance.MySpell3Unlocked = true;
            SpellsDisplay.instance.UpdateSpellInfo();
            MessageFeedManager.MyInstance.WriteMessage("Odblokowano umiejętność: Błogosławieństwo");
        }

        if(level == 16)
        {
            SpellSystem.instance.MySpell4Unlocked = true;
            SpellsDisplay.instance.UpdateSpellInfo();
            MessageFeedManager.MyInstance.WriteMessage("Odblokowano umiejętność: Berserk");
        }
    }
}
