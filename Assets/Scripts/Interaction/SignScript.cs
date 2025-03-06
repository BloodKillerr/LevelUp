using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignScript : Interactable
{
    public GameObject SignPanel;

    [HideInInspector]
    public GameObject playerCamera;

    private  bool isDisplayed = false;

    public override void Interact()
    {
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        base.Interact();
        if (isDisplayed == false)
        {
            inventoryUI.canBeOpened = false;
            pauseMenu.canBeOpened = false;

            isDisplayed = true;
            SignPanel.SetActive(true);

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
            inventoryUI.canBeOpened = true;
            pauseMenu.canBeOpened = true;

            isDisplayed = false;
            SignPanel.SetActive(false);

            playerCamera.GetComponent<PlayerLook>().enabled = true;

            Time.timeScale = 1f;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = true;
            }
        }
    }
}
