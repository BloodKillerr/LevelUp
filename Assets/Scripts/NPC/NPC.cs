using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable
{
    public QuestGiver qg;

    public DialogueDisplay dialogueDisplay;

    [HideInInspector]
    public GameObject playerCamera;

    public Conversation conversation;

    private bool isOn = false;

    public override void Interact()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        dialogueDisplay = GameObject.FindGameObjectWithTag("DialogueBox").GetComponent<DialogueDisplay>();

        if (!isOn)
        {
            dialogueDisplay.Open();
            dialogueDisplay.conversation = conversation;
            dialogueDisplay.NPC_Script = this;
            dialogueDisplay.AdvanceConversation();
            isOn = true;

            inventoryUI.canBeOpened = false;
            pauseMenu.canBeOpened = false;

            playerCamera.GetComponent<PlayerLook>().enabled = false;

            Time.timeScale = 0f;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = false;
            }
        }
        else
        {
            dialogueDisplay.Close();
            dialogueDisplay.conversation = null;
            dialogueDisplay.NPC_Script = null;
            dialogueDisplay.activeLineIndex = 0;
            isOn = false;

            inventoryUI.canBeOpened = true;
            pauseMenu.canBeOpened = true;

            playerCamera.GetComponent<PlayerLook>().enabled = true;

            Time.timeScale = 1f;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = true;
            }
        }
    }

    public override void OnDestroy()
    {
        base.OnDestroy();

        if (playerCamera)
        {
            playerCamera.GetComponent<PlayerLook>().enabled = true;
        }

        inventoryUI.canBeOpened = true;
        pauseMenu.canBeOpened = true;

        Time.timeScale = 1f;

        Animator[] animators = FindObjectsOfType<Animator>();

        foreach (Animator anim in animators)
        {
            anim.enabled = true;
        }
    }
}
