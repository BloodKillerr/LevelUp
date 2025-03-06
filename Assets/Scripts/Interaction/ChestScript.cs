using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestScript : Interactable
{
    private GameObject playerCamera;

    private ChestDisplay chestDisplay;

    private TMP_Text chestText;

    private Button yesButton;

    private Button noButton;

    private Chest chest;

    private bool isOpen = false;
    public Animator chestAnim;

    public override void Interact()
    {
        chestDisplay = GameObject.FindGameObjectWithTag("ChestDisplay").GetComponent<ChestDisplay>();
        chestText = chestDisplay.chestText;
        yesButton = chestDisplay.yesButton;
        noButton = chestDisplay.noButton;
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        chest = gameObject.GetComponent<Chest>();
        base.Interact();

        if(isOpen)
        {
            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = true;
            }

            chestAnim.SetBool("Open", false);

            inventoryUI.canBeOpened = true;
            pauseMenu.canBeOpened = true;
            chestDisplay.Close();

            yesButton.onClick.RemoveListener(chest.LootChest);
            yesButton.onClick.RemoveListener(Interact);
            noButton.onClick.RemoveListener(Interact);

            chestText.text = "";

            playerCamera.GetComponent<PlayerLook>().enabled = true;

            isOpen = false;
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            chestAnim.SetBool("Open", true);

            inventoryUI.canBeOpened = false;
            pauseMenu.canBeOpened = false;
            chestDisplay.Open();

            yesButton.onClick.AddListener(chest.LootChest);
            yesButton.onClick.AddListener(Interact);
            noButton.onClick.AddListener(Interact);

            SetChestPanel();

            playerCamera.GetComponent<PlayerLook>().enabled = false;

            isOpen = true;
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = false;
            }
            chestAnim.enabled = true;
        }
    }

    void SetChestPanel()
    {
        chest = gameObject.GetComponent<Chest>();

        chestText.text = "Zdobyłeś: \n";

        foreach (Item item in chest.MyChestLoot)
        {
            chestText.text += item.name + " x " + item.amount + "\n";
        }

        if(chest.goldReward != 0)
        {
            chestText.text += "Złoto x " + chest.goldReward;
        }

    }
}
