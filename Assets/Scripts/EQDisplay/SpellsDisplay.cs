using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SpellsDisplay : MonoBehaviour
{
    #region Singleton
    public static SpellsDisplay instance;

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

    public Image spell1Icon;

    public Image spell2Icon;

    public Image spell3Icon;

    public Image spell4Icon;

    public TMP_Text spell1Property1;

    public TMP_Text spell1Property2;

    public TMP_Text spell2Property1;

    public TMP_Text spell2Property2;

    public TMP_Text spell2Property3;

    public TMP_Text spell3Property1;

    public TMP_Text spell3Property2;

    public TMP_Text spell4Property1;

    public TMP_Text spell4Property2;

    public TMP_Text spell1ManaCost;

    public TMP_Text spell2ManaCost;

    public TMP_Text spell3ManaCost;

    public TMP_Text spell4ManaCost;

    public Image spell1QuickIcon;

    public Image spell2QuickIcon;

    public Image spell3QuickIcon;

    public Image spell4QuickIcon;

    public void UpdateSpellInfo()
    {
        SpellSystem sS = SpellSystem.instance;

        spell1Icon.sprite = sS.spell1Icon;
        spell2Icon.sprite = sS.spell2Icon;
        spell3Icon.sprite = sS.spell3Icon;
        spell4Icon.sprite = sS.spell4Icon;

        spell3Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell3Time);
        spell4Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell4Time);

        if(sS.MySpell1Unlocked)
        {
            spell1Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell1Time);
            spell1Property2.text = string.Format("Obrażenia: {0}", sS.MyFireballDamage);
            spell1ManaCost.text = string.Format("MP: {0}", sS.MySpell1Cost);

            spell1Property1.color = Color.white;
            spell1Property2.color = Color.white;
            spell1ManaCost.color = Color.white;
            spell1Icon.color = Color.white;
            spell1QuickIcon.enabled = true;
        }
        else
        {
            spell1Property1.text = "???";
            spell1Property2.text = "???";
            spell1ManaCost.text = "???";

            spell1Property1.color = Color.gray;
            spell1Property2.color = Color.gray;
            spell1ManaCost.color = Color.gray;
            spell1Icon.color = Color.gray;
            spell1QuickIcon.enabled = false;
        }

        if(sS.MySpell2Unlocked)
        {
            spell2Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell2Time);
            spell2Property2.text = string.Format("Czas działania: {0}", sS.MyFreezeTime);
            spell2Property3.text = string.Format("Zasięg działania: {0}", sS.MyFreezeRadius);
            spell2ManaCost.text = string.Format("MP: {0}", sS.MySpell2Cost);

            spell2Property1.color = Color.white;
            spell2Property2.color = Color.white;
            spell2Property3.color = Color.white;
            spell2ManaCost.color = Color.white;
            spell2Icon.color = Color.white;
            spell2QuickIcon.enabled = true;
        }
        else
        {
            spell2Property1.text = "???";
            spell2Property2.text = "???";
            spell2Property3.text = "???";
            spell2ManaCost.text = "???";

            spell2Property1.color = Color.gray;
            spell2Property2.color = Color.gray;
            spell2Property3.color = Color.gray;
            spell2ManaCost.color = Color.gray;
            spell2Icon.color = Color.gray;
            spell2QuickIcon.enabled = false;
        }

        if(sS.MySpell3Unlocked)
        {
            spell3Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell3Time);
            spell3Property2.text = string.Format("Odnowa życia: {0}", sS.MyHealAmount);
            spell3ManaCost.text = string.Format("MP: {0}", sS.MySpell3Cost);

            spell3Property1.color = Color.white;
            spell3Property2.color = Color.white;
            spell3ManaCost.color = Color.white;
            spell3Icon.color = Color.white;
            spell3QuickIcon.enabled = true;
        }
        else
        {
            spell3Property1.text = "???";
            spell3Property2.text = "???";
            spell3ManaCost.text = "???";

            spell3Property1.color = Color.gray;
            spell3Property2.color = Color.gray;
            spell3ManaCost.color = Color.gray;
            spell3Icon.color = Color.gray;
            spell3QuickIcon.enabled = false;
        }

        if(sS.MySpell4Unlocked)
        {
            spell4Property1.text = string.Format("Czas odnowienia: {0}", sS.MySpell4Time);
            spell4Property2.text = string.Format("Czas trwania: {0}", sS.MyDamageBuffTime);
            spell4ManaCost.text = string.Format("MP: {0}", sS.MySpell4Cost);

            spell4Property1.color = Color.white;
            spell4Property2.color = Color.white;
            spell4ManaCost.color = Color.white;
            spell4Icon.color = Color.white;
            spell4QuickIcon.enabled = true;
        }
        else
        {
            spell4Property1.text = "???";
            spell4Property2.text = "???";
            spell4ManaCost.text = "???";

            spell4Property1.color = Color.gray;
            spell4Property2.color = Color.gray;
            spell4ManaCost.color = Color.gray;
            spell4Icon.color = Color.gray;
            spell4QuickIcon.enabled = false;
        }
    }
}
