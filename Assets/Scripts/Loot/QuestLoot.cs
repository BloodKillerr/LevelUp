using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestLoot
{
    [SerializeField]
    private Item item = null;

    public int amount;

    public Item MyItem { get => item; set => item = value; }
}
