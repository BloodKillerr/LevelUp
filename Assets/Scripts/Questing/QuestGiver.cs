using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private string questGiverName = null;

    [SerializeField]
    private Quest[] quests = null;

    [SerializeField]
    private GameObject questCanvas = null;

    public Quest[] MyQuests { get => quests; }

    public string MyQuestGiverName { get => questGiverName; }

    public void GiveQuests()
    {
        foreach (Quest quest in MyQuests)
        {
            if(QuestLog.MyInstance)
            {
               QuestLog.MyInstance.AcceptQuest(quest);
            }   
        }
        Destroy(this);
    }

    private void OnDestroy()
    {
        if (questCanvas != null)
        {
            Destroy(questCanvas);
        }
        if (FindObjectOfType<QuestGiverNPC>() != null)
        {
            Destroy(FindObjectOfType<QuestGiverNPC>());
        }
        if(FindObjectOfType<NPC>() != null)
        {
            Destroy(FindObjectOfType<NPC>());
        }
    }
}
