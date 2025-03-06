using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class InventorySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Item item;

    public Image icon;

    public TMP_Text count;

    public Button removeButton;

    private GameObject player;

    public int slotIndex;

    private void Start()
    {
        player = PlayerManager.instance.player;
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        if (item.amount > 1)
        {
            count.text = item.amount.ToString();
        }
        else
        {
            count.text = "";
        }
        icon.enabled = true;
        removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        count.text = "";
        removeButton.interactable = false;
    }

    public void OnRemoveButton()
    {
        Inventory.instance.Drop(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            item.Use();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item)
        {
            RealToolTip.instance.gameObject.transform.position = this.transform.position;
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 1;
            RealToolTip.instance.SetTooltip(item);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
