using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potions")]
public class Potions : Item
{
    public PotionType potionType;

    public int healAmount;
    public int manaAmount;

    public Potions()
    {
        property1 = "Działanie: ";
        property2 = "";
    }

    public override void Use()
    {
        base.Use();
        int index = (int)potionType;
        
        if((PotionsUsage.instance.currentPotions[index] != null && name != PotionsUsage.instance.currentPotions[index].name) || PotionsUsage.instance.currentPotions[index] == null)
        {
            PotionsUsage.instance.EquipPotion(this);
            Inventory.instance.RemovePotion(this);
        }  
    }
}

public enum PotionType { Potion1, Potion2}
