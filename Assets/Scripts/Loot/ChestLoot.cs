using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChestLoot
{
    [SerializeField]
    private Item item = null;

    public int minAmount;
    public int maxAmount;

    public Item MyItem { get => item; }
}
