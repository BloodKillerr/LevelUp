using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestLog : MonoBehaviour
{
    private List<QuestScript> questScripts = new List<QuestScript>();

    [SerializeField]
    private List<Quest> quests = new List<Quest>();

    [SerializeField]
    private GameObject questPrefab=null;

    [SerializeField]
    private Transform questList=null;

    private Quest selected;

    [SerializeField]
    private TMP_Text questDescription = null;

    [SerializeField]
    private TMP_Text questCountText = null;

    public GameObject dialogue;

    public TMP_Text dialogueText;

    private string action;

    private int questCount;

    private static QuestLog instance;

    public static QuestLog MyInstance
    {
        get
        {
            if(instance==null)
            {
                instance = FindObjectOfType<QuestLog>();
            }
            return instance;
        }
    }

    public List<Quest> MyQuests { get => quests; set => quests = value; }

    private void Start()
    {
        questCountText.text = questCount.ToString();
    }

    public void AcceptQuest(Quest quest)
    {
        Quest[] questsToCheck = MyQuests.ToArray();

        foreach(Quest q in questsToCheck)
        {
            if(quest.MyTitle == q.MyTitle)
            {
                return;
            }
        }

        questCount++;
        questCountText.text = questCount.ToString();
        foreach (CollectObjective o in quest.MyCollectObjectives)
        {
            Inventory.instance.onItemChangedCallback += new Inventory.OnItemChanged(o.UpdateItemCount);
            o.UpdateItemCount();
        }

        foreach (KillObjective o in quest.MyKillObjectives)
        {
            CombatEvents.OnEnemyDeath += new CombatEvents.EnemyEventHandler(o.UpdateKillCount);
        }

        foreach (RiddleObjective o in quest.MyRiddleObjectives)
        {
            RiddleBehaviour.OnRiddleTriggered += new RiddleBehaviour.RiddleEventHandler(o.UpdateRiddleCount);
        }

        GameObject go = Instantiate(questPrefab, questList);
        go.name = quest.MyTitle;

        QuestScript qs = go.GetComponent<QuestScript>();
        quest.MyQuestScript = qs;
        qs.MyQuest = quest;

        questScripts.Add(qs);
        MyQuests.Add(quest);

        go.GetComponent<TMP_Text>().text = quest.MyTitle;

        if (MessageFeedManager.MyInstance != null)
        {
            MessageFeedManager.MyInstance.WriteMessage(string.Format("\"{0}\" rozpoczęty", quest.MyTitle));
        }

        CheckCompletion();
    }

    public void UpdateSelected()
    {
        ShowDescription(selected);
    }

    public void ShowDescription(Quest quest)
    {
        if (quest != null)
        {
            selected = quest;

            string title = quest.MyTitle;
            string description = quest.MyDescription;
            string objectives = string.Empty;
            string rewards = string.Empty;

            if(quest.MyCollectObjectives.Length > 0) { objectives += string.Format("Zdobądź:\n"); }
            foreach (CollectObjective obj in quest.MyCollectObjectives)
            {
                objectives += string.Format("{0}: {1}/{2}\n", obj.MyType, obj.MyCurrentAmount, obj.MyAmount);
            }

            if (quest.MyKillObjectives.Length > 0) { objectives += string.Format("Pokonaj:\n"); }
            foreach (KillObjective obj in quest.MyKillObjectives)
            {
                objectives += string.Format("{0}: {1}/{2}\n", obj.MyType, obj.MyCurrentAmount, obj.MyAmount);
            }

            if (quest.MyRiddleObjectives.Length > 0) { objectives += string.Format("Użyj:\n"); }
            foreach (RiddleObjective obj in quest.MyRiddleObjectives)
            {     
                objectives += string.Format("{0}: {1}/{2}\n", obj.MyType, obj.MyCurrentAmount, obj.MyAmount);
            }

            rewards += string.Format("XP: {0}\n", quest.xp);
            rewards += string.Format("Złoto: {0}\n", quest.gold);

            foreach (QuestLoot reward in quest.MyItems)
            {
                rewards += string.Format("{0} x {1}\n", reward.MyItem.name, reward.amount);
            }

            if(quest.MyTitleReward != "")
            {
                rewards += string.Format("Tytuł: {0}\n", quest.MyTitleReward);
            }
            
            if(quest.MyTraitReward != "")
            {
                rewards += string.Format("Cecha: {0}\n", quest.MyTraitReward);
            }          

            questDescription.text = string.Format("\n<size=60><b>{0}</b></size>\n\n\n{1}\n\nCele:\n\n{2}\nNagrody:\n{3}", title, description, objectives, rewards);
        }
        else
        {
            questDescription.text = string.Empty;
        }
    }

    public void CheckCompletion()
    {
        foreach (QuestScript qs in questScripts)
        {
            qs.IsComplete();
        }
    }

    public void ShowDialogue(GameObject clickButton)
    {
        CloseDialogue();
        if(selected != null)
        {
            if (selected.IsComplete == true)
            {
                action = clickButton.name;

                switch (action)
                {
                    case "CompleteButton":
                        dialogueText.text = "Ukończyć zadanie?";
                        break;
                    case "AbandonButton":
                        dialogueText.text = "Porzucić zadanie?";
                        break;
                }

                dialogue.SetActive(true);
            }
            else
            {
                action = clickButton.name;

                switch (action)
                {
                    case "AbandonButton":
                        dialogueText.text = "Porzucić zadanie?";
                        dialogue.SetActive(true);
                        break;
                }           
            }
        }
    }

    public void ExecuteAction()
    {
        switch (action)
        {
            case "CompleteButton":
                CompleteQuest(selected);
                break;
            case "AbandonButton":
                AbandonQuest(selected);
                break;
        }
        CloseDialogue();
    }

    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    public void CompleteQuest(Quest quest)
    {
        if (quest != null && quest.IsComplete)
        {
            foreach (CollectObjective o in quest.MyCollectObjectives)
            {
                Inventory.instance.onItemChangedCallback -= new Inventory.OnItemChanged(o.UpdateItemCount);
            }

            foreach (KillObjective o in quest.MyKillObjectives)
            {
                CombatEvents.OnEnemyDeath -= new CombatEvents.EnemyEventHandler(o.UpdateKillCount);
            }

            foreach (RiddleObjective o in quest.MyRiddleObjectives)
            {
                RiddleBehaviour.OnRiddleTriggered -= new RiddleBehaviour.RiddleEventHandler(o.UpdateRiddleCount);
            }

            quest.GainRewards();

            RemoveQuest(quest.MyQuestScript);
        }
    }

    public void AbandonQuest(Quest quest)
    {
        //Removes the quest from quest log
        //Remember to remove quest from quest list
        if(quest != null)
        {
            foreach (CollectObjective o in quest.MyCollectObjectives)
            {
                Inventory.instance.onItemChangedCallback -= new Inventory.OnItemChanged(o.UpdateItemCount);
            }

            foreach (KillObjective o in quest.MyKillObjectives)
            {
                CombatEvents.OnEnemyDeath -= new CombatEvents.EnemyEventHandler(o.UpdateKillCount);
            }

            foreach (RiddleObjective o in quest.MyRiddleObjectives)
            {
                RiddleBehaviour.OnRiddleTriggered -= new RiddleBehaviour.RiddleEventHandler(o.UpdateRiddleCount);
            }

            MessageFeedManager.MyInstance.WriteMessage(string.Format("Porzucono zadanie: {0}", quest.MyTitle));

            RemoveQuest(quest.MyQuestScript);
        }
    }

    public void RemoveQuest(QuestScript qs)
    {
        if(qs != null)
        {
            MyQuests.Remove(qs.MyQuest);
            questScripts.Remove(qs);
            Destroy(qs.gameObject);
            selected = null;
            ShowDescription(selected);
            questCount--;
            questCountText.text = questCount.ToString();
        }
    }
}
