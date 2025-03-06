using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Loot", menuName = "Inventory/Loot")]
public class Loot : Item
{
    public string Source = "Monster Name";

    public Loot()
    {
        property1 = "Źródło: ";
    }
}
