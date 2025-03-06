using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    [SerializeField]
    private LootItem[] items = null;

    private List<Item> droppedItems = new List<Item>();

    private int goldReward;
    public int minGold = 1;
    public int maxGold = 1;

    public int xpReward = 10;

    private void RollLoot()
    {
        foreach (LootItem item in items)
        {
            int roll = Random.Range(0, 100);

            if(roll <= item.MyDropChance)
            {
                int amount = Random.Range(item.minAmount, item.maxAmount + 1);
                if (item.MyItem.isStackable)
                {
                    item.MyItem.amount = amount;
                    droppedItems.Add(item.MyItem);
                }
                else
                {
                    for(int i=0; i<amount; i++)
                    {
                        droppedItems.Add(item.MyItem);
                    }
                }
            }
        }
    }

    public void GainLoot()
    {
        RollLoot();

        foreach (Item item in droppedItems)
        {
            bool added = Inventory.instance.Add(item);
            if (!added)
            {
                Inventory.instance.DropOnTheGround(item);
            }
            item.amount = 1;
        }

        goldReward = Random.Range(minGold, maxGold+1);
        PlayerManager.instance.player.GetComponent<PlayerStats>().money += goldReward;
        LevelingSystem.instance.GetComponent<LevelingSystem>().GainExp(xpReward);
    }
}
