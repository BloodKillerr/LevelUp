using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vendor : Interactable
{
    [SerializeField]
    private string vendorName;

    private VendorWindow vendorWindow;
    
    [HideInInspector]
    public GameObject playerCamera;

    private bool isOn = false;

    [SerializeField]
    private VendorItem[] items = null;

    [SerializeField]
    private List<Item> itemsToBuyBack = new List<Item>();

    [HideInInspector]
    private VendorButton[] vendorButtons = null;

    public VendorItem[] MyItems { get => items; set => items = value; }
    public string MyVendorName { get => vendorName; set => vendorName = value; }
    public List<Item> MyItemsToBuyBack { get => itemsToBuyBack; set => itemsToBuyBack = value; }

    public override void Interact()
    {
        vendorWindow = VendorWindow.instance;
        vendorWindow.vendorNameText.text = MyVendorName;
        vendorWindow.vendor = this;
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");

        vendorButtons = vendorWindow.vendorButtons;
        base.Interact();

        if(!isOn)
        {
            #region Interakcja
            isOn = true;
            vendorWindow.Open();

            inventoryUI.canBeOpened = false;
            pauseMenu.canBeOpened = false;

            playerCamera.GetComponent<PlayerLook>().enabled = false;

            Time.timeScale = 0f;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            #endregion

            CreatePages(MyItems);

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = false;
            }
        }
        else
        {
            #region Interakcja
            isOn = false;
            vendorWindow.Close();

            inventoryUI.canBeOpened = true;
            pauseMenu.canBeOpened = true;

            playerCamera.GetComponent<PlayerLook>().enabled = true;

            Time.timeScale = 1f;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            #endregion

            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            vendorButtons = null;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach (Animator anim in animators)
            {
                anim.enabled = true;
            }
        }

    }

    public void CreatePages(VendorItem[] items)
    {
        for(int i=0; i<items.Length; i++)
        {
            vendorButtons[i].AddItem(items[i]);
        }
    }

    public void ClearButtons()
    {
        foreach (VendorButton btn in vendorButtons)
        {
            btn.gameObject.SetActive(false);
        }
    }
}
