using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiverNPC : Interactable
{
    public QuestGiver qg;

    public override void Interact()
    {
        base.Interact();
        qg.GiveQuests();
        Destroy(this);
    }
}
