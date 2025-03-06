using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Zapiski_Mistrza", menuName = "Inventory/Consumables/Zapiski_Mistrza")]
public class Zapiski_Mistrza : Consumables
{
    public Quest quest;

    public Zapiski_Mistrza()
    {

    }

    public override void Use()
    {
        base.Use();

        QuestLog.MyInstance.AcceptQuest(quest);

        RemoveFromInventory();
    }
}
