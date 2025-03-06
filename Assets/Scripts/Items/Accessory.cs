using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Accessory", menuName = "Inventory/Equipment/Accessory")]
public class Accessory : Equipment
{
    public int type;

    //0 - fireDamage
    //1 - frostDuration
    //2 - frostRadius
    //3 - healAmount
    //4 - damageModifier
    //5 - Armor modifier

    public int fireDamage;
    public float frostDuration;
    public float frostRadius;
    public int healAmount;

    public Accessory()
    {
        property1 = "Działanie1";
        property2 = "Działanie2";
    }
}
