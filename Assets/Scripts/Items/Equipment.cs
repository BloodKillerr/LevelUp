using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damageModifier;

    public Equipment()
    {
        property1 = "Pancerz: " + armorModifier.ToString();
        property2 = "Obrażenia: " + damageModifier.ToString();
    }

    public override void Use()
    {
        base.Use();
        int index = (int)equipSlot;

        if ((EquipmentManager.instance.currentEquipment[index] != null && name != EquipmentManager.instance.currentEquipment[index].name) || EquipmentManager.instance.currentEquipment[index] == null)
        {
            EquipmentManager.instance.Equip(this);
            RemoveFromInventory();
        }         
    }
}

public enum EquipmentSlot { Head, Chest, Shoulders, Arms, Legs, Weapon, Boots, Neck, Finger}
