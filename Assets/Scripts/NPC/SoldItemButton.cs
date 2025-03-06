using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class SoldItemButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private Image icon = null;

    [SerializeField]
    private TMP_Text quantityText = null;

    private VendorWindow vendorWindow;

    public Item item;

    private void Start()
    {
        vendorWindow = VendorWindow.instance;
    }

    public void AddItem(Item item)
    {
        this.item = item;

        quantityText.text = item.amount.ToString();
        icon.sprite = item.icon;
    }

    public void SellItem()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Sprzedano: {0} x {1} za {2} złota", item.name, item.amount, item.amount * item.price));
            PlayerManager.instance.player.GetComponent<PlayerStats>().money += item.amount * item.price;
            Inventory.instance.Remove(item);
            Destroy(gameObject);
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;

            if (vendorWindow.vendor.MyItemsToBuyBack.Count < 5)
            {
                vendorWindow.vendor.MyItemsToBuyBack.Add(item);
            }
            else
            {
                vendorWindow.vendor.MyItemsToBuyBack.RemoveAt(0);
                vendorWindow.vendor.MyItemsToBuyBack.Add(item);
            }
        }
        else
        {
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Sprzedano: {0} x 1 za {1} złota", item.name,item.price));
            PlayerManager.instance.player.GetComponent<PlayerStats>().money += item.price;

            Inventory.instance.Remove(item);
            quantityText.text = item.amount.ToString();

            if (!Inventory.instance.items.Contains(item))
            {
                RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                Destroy(gameObject);
            }

            Item copyItem = Instantiate(item);
            if(copyItem.isStackable)
            {
                copyItem.amount = 1;
            }

            if(vendorWindow.vendor.MyItemsToBuyBack.Count < 5)
            {
                vendorWindow.vendor.MyItemsToBuyBack.Add(copyItem);
            }
            else
            {
                vendorWindow.vendor.MyItemsToBuyBack.RemoveAt(0);
                vendorWindow.vendor.MyItemsToBuyBack.Add(copyItem);
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SellItem();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        RealToolTip.instance.gameObject.transform.position = transform.position;
        RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;
        RealToolTip.instance.SetTooltip(item);
        gameObject.GetComponent<Image>().color = new Color(
            gameObject.GetComponent<Image>().color.r,
            gameObject.GetComponent<Image>().color.g,
            gameObject.GetComponent<Image>().color.b, .75f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        gameObject.GetComponent<Image>().color = new Color(
            gameObject.GetComponent<Image>().color.r,
            gameObject.GetComponent<Image>().color.g,
            gameObject.GetComponent<Image>().color.b, 1f);
    }
}
