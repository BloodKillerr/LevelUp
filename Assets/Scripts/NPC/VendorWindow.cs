using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VendorWindow : MonoBehaviour
{
    #region Singleton

    public static VendorWindow instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    public TMP_Text vendorNameText;

    public VendorButton[] vendorButtons;

    public Vendor vendor;

    public TMP_Text moneyText;

    public TMP_Text buyBackText;

    public GameObject quickInv;

    public GameObject itemsArea;

    public GameObject itemToSell;

    private bool isQInvOpen = false;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = PlayerManager.instance.player.GetComponent<PlayerStats>().money.ToString();
        if(vendor)
        {
            buyBackText.text = string.Format("{0}/5", vendor.MyItemsToBuyBack.Count);
        }       
    }

    public void OCQInv()
    {
        isQInvOpen = !isQInvOpen;
        quickInv.SetActive(isQInvOpen);

        foreach (Transform child in itemsArea.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (Item item in Inventory.instance.items)
        {
            itemToSell.GetComponent<SoldItemButton>().AddItem(item);
            Instantiate(itemToSell, itemsArea.transform);
        }
    }

    public void BuyBack()
    {
        if (vendor.MyItemsToBuyBack.Count > 0 && PlayerManager.instance.player.GetComponent<PlayerStats>().money >= vendor.MyItemsToBuyBack[0].price * vendor.MyItemsToBuyBack[0].amount)
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().money -= vendor.MyItemsToBuyBack[0].amount * vendor.MyItemsToBuyBack[0].price;

            Inventory.instance.Add(vendor.MyItemsToBuyBack[0]);

            vendor.MyItemsToBuyBack.RemoveAt(0);

            foreach (Transform child in itemsArea.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Item item in Inventory.instance.items)
            {
                itemToSell.GetComponent<SoldItemButton>().AddItem(item);
                Instantiate(itemToSell, itemsArea.transform);
            }
        }
        else if(vendor.MyItemsToBuyBack.Count > 0 && PlayerManager.instance.player.GetComponent<PlayerStats>().money < vendor.MyItemsToBuyBack[0].price * vendor.MyItemsToBuyBack[0].amount)
        {
            MessageFeedManager.MyInstance.WriteMessage("Za mało złota, by odkupić przedmiot");
        }
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

        quickInv.SetActive(false);
        isQInvOpen = false;
    }
}
