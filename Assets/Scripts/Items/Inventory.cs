using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

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

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int space = 20;

    public List<Item> items = new List<Item>();

    public GameObject itemPrefab;

    public GameObject itemDropAudioObj;

    public bool Add(Item item)
    {
        Item copyItem = Instantiate(item);
        if (items.Count >= space && !item.isStackable)
        {
            MessageFeedManager.MyInstance.WriteMessage("Ekwipunek jest pełny");
            return false;
        }

        if (copyItem.isStackable == true)
        {
            bool itemAlreadyInInventory = false;
            foreach (Item inventoryItem in items)
            {
                if (inventoryItem.name == copyItem.name)
                {
                    if (inventoryItem.amount < 999 && inventoryItem.amount + copyItem.amount <= 999)
                    {
                        inventoryItem.amount += copyItem.amount;
                        itemAlreadyInInventory = true;
                        break;
                    }
                    else if (inventoryItem.amount < 999 && inventoryItem.amount + copyItem.amount > 999)
                    {
                        int amountToAdd1 = 999 - inventoryItem.amount;
                        inventoryItem.amount += amountToAdd1;
                        copyItem.amount -= amountToAdd1;
                    }
                }
            }
            if (!itemAlreadyInInventory && items.Count < space)
            {
                items.Add(copyItem);
            }
            else if (!itemAlreadyInInventory && items.Count == space)
            {
                MessageFeedManager.MyInstance.WriteMessage("Ekwipunek jest pełny");
                return false;
            }
        }
        else
        {
            copyItem.amount = 1;
            for (int i = 0; i < item.amount; i++)
            {
                items.Add(copyItem);
            }
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        MessageFeedManager.MyInstance.WriteMessage(string.Format("Zdobyto: {0} x {1}", item.name, item.amount));

        return true;
    }

    public void Remove(Item item)
    {
        if (item.isStackable == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                items.Remove(item);
                RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                item.amount--;

                if (item.amount == 0)
                {
                    items.Remove(item);
                    RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
        else
        {
            items.Remove(item);
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void RemovePotion(Potions potion)
    {
        potion.amount--;

        if (potion.amount == 0)
        {
            items.Remove(potion);
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }

    public void Drop(Item item)
    {
        if (item.isStackable == true)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                itemPrefab.GetComponent<ItemPickup>().item = item;
                itemPrefab.GetComponent<ItemPickup>().howMany = item.amount;
                MessageFeedManager.MyInstance.WriteMessage(string.Format("Upuszczono: {0} x {1}", item.name, item.amount));
                Instantiate(itemPrefab, new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y - .30f, PlayerManager.instance.player.transform.position.z), Quaternion.identity);
                items.Remove(item);
                itemPrefab.GetComponent<ItemPickup>().item = null;
                itemPrefab.GetComponent<ItemPickup>().howMany = 1;
                RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            }
            else
            {
                item.amount--;
                itemPrefab.GetComponent<ItemPickup>().item = Instantiate(item);
                itemPrefab.GetComponent<ItemPickup>().howMany = 1;
                MessageFeedManager.MyInstance.WriteMessage(string.Format("Upuszczono: {0} x {1}", item.name, itemPrefab.GetComponent<ItemPickup>().howMany));
                Instantiate(itemPrefab, new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y - .30f, PlayerManager.instance.player.transform.position.z), Quaternion.identity);
                itemPrefab.GetComponent<ItemPickup>().item = null;
                itemPrefab.GetComponent<ItemPickup>().howMany = 1;

                if (item.amount == 0)
                {
                    items.Remove(item);
                    RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
                }
            }
        }
        else
        {
            itemPrefab.GetComponent<ItemPickup>().item = item;
            itemPrefab.GetComponent<ItemPickup>().howMany = item.amount;
            MessageFeedManager.MyInstance.WriteMessage(string.Format("Upuszczono: {0} x {1}", item.name, item.amount));
            Instantiate(itemPrefab, new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y - .30f, PlayerManager.instance.player.transform.position.z), Quaternion.identity);
            items.Remove(item);
            itemPrefab.GetComponent<ItemPickup>().item = null;
            itemPrefab.GetComponent<ItemPickup>().howMany = 1;
            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
        }

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

        GameObject audio = Instantiate(itemDropAudioObj, PlayerManager.instance.player.transform.position, Quaternion.identity, PlayerManager.instance.player.transform);
        Destroy(audio, 1f);
    }

    public void RemoveCompletely(Item item)
    {
        items.Remove(item);
    }

    public void DropOnTheGround(Item copyItem)
    {
        itemPrefab.GetComponent<ItemPickup>().item = copyItem;
        itemPrefab.GetComponent<ItemPickup>().howMany = copyItem.amount;
        Instantiate(itemPrefab, new Vector3(PlayerManager.instance.player.transform.position.x, PlayerManager.instance.player.transform.position.y - .30f, PlayerManager.instance.player.transform.position.z), Quaternion.identity);
        itemPrefab.GetComponent<ItemPickup>().item = null;
        itemPrefab.GetComponent<ItemPickup>().howMany = 1;
    }
}
