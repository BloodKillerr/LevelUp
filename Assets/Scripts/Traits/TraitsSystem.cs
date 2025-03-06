using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitsSystem : MonoBehaviour
{
    #region Singleton

    public static TraitsSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        allPlayerText = GameObject.FindGameObjectWithTag("EQCanvas").GetComponent<AllPlayerText>();
    }

    #endregion

    private List<string> traitsList = new List<string>();

    private AllPlayerText allPlayerText;

    public int maxAmount = 10;

    public List<string> MyTraitsList { get => traitsList; set => traitsList = value; }

    void Start()
    {
        
    }

    public void AddTrait(string trait)
    {
        if(MyTraitsList.Count < maxAmount)
        {
            MyTraitsList.Add(trait);
            RefreshTraitsUi();

            switch (trait)
            {
                case "Żelazna skóra: + 10 pancerza":
                    PlayerManager.instance.player.GetComponent<PlayerStats>().armor.AddModifier(10);
                    break;
                case "Władca Cieni: + 100 obrażeń":
                    PlayerManager.instance.player.GetComponent<PlayerStats>().damage.AddModifier(100);
                    break;
                case "Duch walki: + 15 obrażeń":
                    PlayerManager.instance.player.GetComponent<PlayerStats>().damage.AddModifier(15);
                    break;
            }
        }
        else
        {
            RemoveTrait(MyTraitsList[0]);
        }
    }

    public void RemoveTrait(string trait)
    {
        MyTraitsList.Remove(trait);
        RefreshTraitsUi();

        switch(trait)
        {
            case "Żelazna skóra: + 10 pancerza":
                PlayerManager.instance.player.GetComponent<PlayerStats>().armor.RemoveModifier(10);
                break;
            case "Władca Cieni: + 100 obrażeń":
                PlayerManager.instance.player.GetComponent<PlayerStats>().damage.RemoveModifier(100);
                break;
            case "Duch walki: + 15 obrażeń":
                PlayerManager.instance.player.GetComponent<PlayerStats>().damage.RemoveModifier(15);
                break;
        }
    }

    public void RefreshTraitsUi()
    {
        allPlayerText.traitsText.text = string.Empty;

        for (int i = 0; i < MyTraitsList.Count; i++)
        {
            allPlayerText.traitsText.text += MyTraitsList[i] + "\n";
        }
    }   
}
