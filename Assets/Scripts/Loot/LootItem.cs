using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LootItem
{
    [SerializeField]
    private Item item = null;

    [SerializeField]
    private float dropChance = 100;

    public int minAmount;
    public int maxAmount;

    public Item MyItem { get => item; }

    public float MyDropChance { get => dropChance; }
}
