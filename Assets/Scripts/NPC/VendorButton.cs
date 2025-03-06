using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class VendorButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private VendorWindow vendorWindow;

    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private TMP_Text itemNameText = null;

    [SerializeField]
    private TMP_Text itemPriceText = null;

    [SerializeField]
    private TMP_Text quantityText = null;

    private VendorItem vendorItem;

    public void Start()
    {
        vendorWindow = VendorWindow.instance;
    }

    public void AddItem(VendorItem vendorItem)
    {
        this.vendorItem = vendorItem;

        if(vendorItem.MyQuantity > 0 || (vendorItem.MyQuantity == 0 && vendorItem.MyUnlimited))
        {
            icon.sprite = vendorItem.MyItem.icon;
            itemNameText.text = vendorItem.MyItem.name;

            if (!vendorItem.MyUnlimited)
            {
                quantityText.text = vendorItem.MyQuantity.ToString();
            }
            else
            {
                quantityText.text = string.Empty;
            }

            if(vendorItem.MyItem.price > 0)
            {
                itemPriceText.text = "Cena: " + vendorItem.MyItem.price;
            }
            else
            {
                itemPriceText.text = string.Empty;
            }

            gameObject.SetActive(true);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RealToolTip.instance.gameObject.transform.position = this.transform.position;
        RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        RealToolTip.instance.SetTooltip(vendorItem.MyItem);
        gameObject.GetComponent<Image>().color = new Color(
            gameObject.GetComponent<Image>().color.r, 
            gameObject.GetComponent<Image>().color.g,
            gameObject.GetComponent<Image>().color.b, .5f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<Image>().color = new Color(
            gameObject.GetComponent<Image>().color.r,
            gameObject.GetComponent<Image>().color.g,
            gameObject.GetComponent<Image>().color.b, .15f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (PlayerManager.instance.player.GetComponent<PlayerStats>().money >= vendorItem.MyItem.price &&
            Inventory.instance.Add(vendorItem.MyItem))
        {
            SellItem();
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Kupiono: {0} za {1} złota", vendorItem.MyItem.name, vendorItem.MyItem.price));

            foreach (Transform child in vendorWindow.itemsArea.transform)
            {
                Destroy(child.gameObject);
            }

            foreach (Item item in Inventory.instance.items)
            {
                vendorWindow.itemToSell.GetComponent<SoldItemButton>().AddItem(item);
                Instantiate(vendorWindow.itemToSell, vendorWindow.itemsArea.transform);
            }
        }
        else if (PlayerManager.instance.player.GetComponent<PlayerStats>().money < vendorItem.MyItem.price)
        {
            MessageFeedManager.MyInstance.WriteMessage("Za mało złota, by dokonać zakupu");
        }
    }

    private void SellItem()
    {
        PlayerManager.instance.player.GetComponent<PlayerStats>().money -= vendorItem.MyItem.price;

        if(!vendorItem.MyUnlimited)
        {
            vendorItem.MyQuantity--;
            quantityText.text = vendorItem.MyQuantity.ToString();

            if(vendorItem.MyQuantity == 0)
            {
                gameObject.SetActive(false);
                RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
        }
    }

    private void OnDisable()
    {
        gameObject.GetComponent<Image>().color = new Color(
            gameObject.GetComponent<Image>().color.r,
            gameObject.GetComponent<Image>().color.g,
            gameObject.GetComponent<Image>().color.b, .15f);
    }
}
