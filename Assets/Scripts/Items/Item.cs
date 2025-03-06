using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName ="Inventory/Item")]
public abstract class Item : ScriptableObject
{
    [SerializeField]
    new public string name = "New Item";

    public string rarity = "Common";
    public string property1 = "Property1";
    public string property2 = "Property2";
    public string description = "Przedmiot";
    public int price = 10;
    public Sprite icon = null;

    [SerializeField]
    public int amount=1;

    public bool isStackable=false;

    public GameObject mesh;

    public virtual void Use()
    {
        //Use Item
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.Remove(this);
    }

    public void CompletelyRemoveFromInventory()
    {
        Inventory.instance.RemoveCompletely(this);
    }
}
