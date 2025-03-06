using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDisplay : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    [HideInInspector]
    public Conversation conversation;

    [HideInInspector]
    public NPC NPC_Script;

    public GameObject NPC;
    public GameObject MainCharacter;

    private SpeakerUI NPC_UI;
    private SpeakerUI MainCharacter_UI;

    [HideInInspector]
    public int activeLineIndex = 0;
    
    void Start()
    {
        NPC_UI = NPC.GetComponent<SpeakerUI>();
        MainCharacter_UI = MainCharacter.GetComponent<SpeakerUI>();
    }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && conversation)
        {
            AdvanceConversation();  
        }
    }

    public void AdvanceConversation()
    {
        if(conversation)
        {
            NPC_UI.Speaker = conversation.NPC;
            MainCharacter_UI.Speaker = conversation.MainCharacter;
        }

        if (activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex++;
        }
        else
        {
            NPC_UI.Hide();
            MainCharacter_UI.Hide();

            conversation = null;

            activeLineIndex = 0;

            Close();

            if(NPC_Script.qg)
            {
                NPC_Script.qg.GiveQuests();
            }

            if(NPC_Script.gameObject.GetComponent<FinalNPC>())
            {
                NPC_Script.gameObject.GetComponent<FinalNPC>().SpawnBoss();
            }
            Destroy(NPC_Script);
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if(NPC_UI.SpeakerIs(character))
        {
            SetDialogue(NPC_UI, MainCharacter_UI, line.text);
        }
        else
        {
            SetDialogue(MainCharacter_UI, NPC_UI, line.text);
        }
    }

    void SetDialogue(SpeakerUI activeSpeakerUI, SpeakerUI inactiveSpeakerUi, string text)
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUi.Hide();
    }

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
