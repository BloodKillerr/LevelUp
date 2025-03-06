using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    [SerializeField]
    private string title;

    [SerializeField]
    private string description;

    [SerializeField]
    private CollectObjective[] collectObjectives=null;

    [SerializeField]
    private KillObjective[] killObjectives = null;

    [SerializeField]
    private RiddleObjective[] riddleObjectives = null;

    [SerializeField]
    public int xp;

    [SerializeField]
    public int gold;

    [SerializeField]
    private string titleReward;

    [SerializeField]
    private string traitReward;

    [SerializeField]
    private List<QuestLoot> items = new List<QuestLoot>();

    [SerializeField]
    private string questGiverName = null;

    public QuestScript MyQuestScript { get; set; }

    public string MyQuestGiverName { get => questGiverName; set => questGiverName = value; }

    public string MyTitle { get => title; set => title = value; }

    public string MyDescription { get => description; set => description = value; }

    public CollectObjective[] MyCollectObjectives { get => collectObjectives; set => collectObjectives = value; }

    public KillObjective[] MyKillObjectives { get => killObjectives; set => killObjectives = value; }

    public RiddleObjective[] MyRiddleObjectives { get => riddleObjectives; set => riddleObjectives = value; }

    public List<QuestLoot> MyItems { get => items; set => items = value; }

    public string MyTitleReward { get => titleReward; set => titleReward = value; }

    public string MyTraitReward { get => traitReward; set => traitReward = value; }

    public bool IsComplete
    {
        get
        {
            foreach (Objective o in MyCollectObjectives)
            {
                if(!o.isComplete)
                {
                    return false;
                }
            }

            foreach (Objective o in MyKillObjectives)
            {
                if (!o.isComplete)
                {
                    return false;
                }
            }

            foreach (Objective o in MyRiddleObjectives)
            {
                if(!o.isComplete)
                {
                    return false;
                }
            }

            return true;
        }
    }

    public void GainRewards()
    {
        foreach (QuestLoot item in MyItems)
        {
            bool added = false;
            if(item.MyItem.isStackable)
            {
                item.MyItem.amount = item.amount;
                added = Inventory.instance.Add(item.MyItem);
                if (!added)
                {
                    Inventory.instance.DropOnTheGround(item.MyItem);
                }
                item.MyItem.amount = 1;
            }
            else
            {
                for(int i=0; i<item.amount; i++)
                {
                    added = Inventory.instance.Add(item.MyItem);
                    if (!added)
                    {
                        Inventory.instance.DropOnTheGround(item.MyItem);
                    }
                }
            }
        }

        if(MyTitleReward != "")
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().MyTitle = MyTitleReward;
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto tytuł: {0}", MyTitleReward));
        }

        if(MyTraitReward != "")
        {
            TraitsSystem.instance.AddTrait(MyTraitReward);
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto cechę: {0}", MyTraitReward));
        }

        PlayerManager.instance.player.GetComponent<PlayerStats>().money += gold;
        MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto {0} złota", gold));

        LevelingSystem.instance.GainExp(xp);
    }
}

[System.Serializable]
public abstract class Objective
{
    [SerializeField]
    private int amount=1;

    private int currentAmount = 0;

    [SerializeField]
    private string type="";

    public int MyAmount { get => amount;}

    public int MyCurrentAmount { get => currentAmount; set => currentAmount = value; }

    public string MyType { get => type;}

    public bool isComplete { get => MyCurrentAmount >= MyAmount; }
}

[System.Serializable]
public class CollectObjective : Objective
{
    public void UpdateItemCount()
    {
        int amount = 0;

        foreach (Item item in Inventory.instance.items)
        {
            if(MyType.ToLower() == item.name.ToLower())
            {
                amount += item.amount;
            }
        }
        MyCurrentAmount = amount;

        QuestLog.MyInstance.UpdateSelected();
        QuestLog.MyInstance.CheckCompletion();
    }
}

[System.Serializable]
public class KillObjective : Objective
{
    public void UpdateKillCount(EnemyStats enemyStats)
    {
        if(MyType.ToLower() == enemyStats.MyEnemyName.ToLower())
        {
            MyCurrentAmount++;
        }

        QuestLog.MyInstance.UpdateSelected();
        QuestLog.MyInstance.CheckCompletion();
    }
}

[System.Serializable]
public class RiddleObjective : Objective
{
    public void UpdateRiddleCount(RiddleInteraction riddle)
    {
        if(MyType.ToLower() == riddle.riddleName.ToLower())
        {
            MyCurrentAmount++;
        }

        QuestLog.MyInstance.UpdateSelected();
        QuestLog.MyInstance.CheckCompletion();
    }
}
