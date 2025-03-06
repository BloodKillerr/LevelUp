using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestScript : MonoBehaviour
{
    public Quest MyQuest { get; set; }

    private bool markedComplete = false;

    public void Select()
    {
        QuestLog.MyInstance.ShowDescription(MyQuest);
        QuestLog.MyInstance.CheckCompletion();
    }

    public void IsComplete()
    {
        if(MyQuest.IsComplete && !markedComplete)
        {
            markedComplete = true;
            GetComponent<TMP_Text>().text += " (Ukończony)";
            MessageFeedManager.MyInstance.WriteMessage(string.Format("{0} (Ukończony)", MyQuest.MyTitle));
        }
        else if(!MyQuest.IsComplete)
        {
            markedComplete = false;
            GetComponent<TMP_Text>().text = MyQuest.MyTitle;
        }
    }
}
