using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField]
    private int chestID = 0;

    public ChestLoot[] loot;

    [SerializeField]
    private List<Item> chestLoot = new List<Item>();

    [HideInInspector]
    public int goldReward;

    public int minGold = 1;
    public int maxGold = 1;

    public List<Item> MyChestLoot { get => chestLoot; set => chestLoot = value; }
    public int MyChestID { get => chestID; set => chestID = value; }

    private void Awake()
    {
        goldReward = Random.Range(minGold, maxGold + 1);

        foreach (ChestLoot item in loot)
        {
            item.MyItem.amount = Random.Range(item.minAmount, item.maxAmount + 1);
            MyChestLoot.Add(Instantiate(item.MyItem));
            item.MyItem.amount = 1;
        }
    }

    public void LootChest()
    {
        foreach(Item item in MyChestLoot)
        {
            bool added = Inventory.instance.Add(item);

            if(!added)
            {
                Inventory.instance.DropOnTheGround(item);
            }
        }

        if(goldReward != 0)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().money += goldReward;

            if(goldReward == 1)
            {
                MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto: {0} sztukę złota", goldReward));
            }
            else if(goldReward > 1 && goldReward <= 4)
            {
                MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto: {0} sztuki złota", goldReward));
            }
            else if(goldReward >= 5)
            {
                MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto: {0} sztuk złota", goldReward));
            }
            
        }

        //Instantiate effect
        Destroy(gameObject);
    }
}
