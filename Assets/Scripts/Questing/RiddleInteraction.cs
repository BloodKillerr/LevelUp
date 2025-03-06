using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleInteraction : Interactable
{
    public string riddleName;
    public string questName;

    public int id = 0;

    public override void Interact()
    {
        Quest[] questsToCheck = QuestLog.MyInstance.MyQuests.ToArray();
        foreach (Quest q in questsToCheck)
        {
            if(questName == q.MyTitle)
            {
                base.Interact();
                RiddleBehaviour.RiddleTriggered(this);
                Destroy(this);
                return;
            }
        }
        MessageFeedManager.MyInstance.WriteMessage(string.Format("Może da się tego jakoś użyć?"));
        return; 
    }

    public override void OnTriggerEnter(Collider other)
    {
        /*
        Quest[] questsToCheck = QuestLog.MyInstance.MyQuests.ToArray();
        foreach (Quest q in questsToCheck)
        {
            if (questName == q.MyTitle)
            {
                base.OnTriggerEnter(other);
                return;
            }
        }
        interactIcon.Close();
        triggered = false;
        return;
        */
        base.OnTriggerEnter(other);
    }
    
    public override void OnTriggerStay(Collider other)
    {
        /*
        Quest[] questsToCheck = QuestLog.MyInstance.MyQuests.ToArray();
        foreach (Quest q in questsToCheck)
        {
            if (questName == q.MyTitle)
            {
                base.OnTriggerStay(other);
                return;
            }
        }
        interactIcon.Close();
        triggered = false;
        return;
        */
        base.OnTriggerStay(other);
    }
}
