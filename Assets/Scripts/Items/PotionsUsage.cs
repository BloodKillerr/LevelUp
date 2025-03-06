using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PotionsUsage : MonoBehaviour
{
    #region Singleton
    public static PotionsUsage instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        int numSlots = System.Enum.GetNames(typeof(PotionType)).Length;
        currentPotions = new Potions[numSlots];
    }
    #endregion

    public Potions[] currentPotions;

    public delegate void OnPotionChanged(Potions newItem, Potions oldItem);
    public OnPotionChanged onPotionsChanged;

    public Image[] icons;

    public GameObject equipAudioObj;

    public void EquipPotion(Potions newItem)
    {
        int slotIndex = (int)newItem.potionType;

        Potions oldItem = null;

        if (currentPotions[slotIndex] != null)
        {
            oldItem = currentPotions[slotIndex];
            bool added = Inventory.instance.Add(oldItem);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(oldItem);
            }
        }

        if (onPotionsChanged != null)
        {
            onPotionsChanged.Invoke(newItem, oldItem);
        }

        icons[slotIndex].sprite = newItem.icon;
        icons[slotIndex].enabled = true;

        Potions newPotion = Instantiate(newItem);
        newPotion.amount = 1;
        currentPotions[slotIndex] = newPotion;
        GameObject Eaudio = Instantiate(equipAudioObj, PlayerManager.instance.player.transform.position, Quaternion.identity, PlayerManager.instance.player.transform);
        Destroy(Eaudio, 1f);
    }

    public void EquipPotionWithoutSound(Potions newItem)
    {
        int slotIndex = (int)newItem.potionType;

        Potions oldItem = null;

        if (currentPotions[slotIndex] != null)
        {
            oldItem = currentPotions[slotIndex];
            bool added = Inventory.instance.Add(oldItem);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(oldItem);
            }
        }

        if (onPotionsChanged != null)
        {
            onPotionsChanged.Invoke(newItem, oldItem);
        }

        icons[slotIndex].sprite = newItem.icon;
        icons[slotIndex].enabled = true;

        Potions newPotion = Instantiate(newItem);
        newPotion.amount = 1;
        currentPotions[slotIndex] = newPotion;
    }

    public void UsePotionAlt(int which)
    {
        Potions inventoryPotion = (System.Array.Find(Inventory.instance.items.ToArray(), x => x.name == currentPotions[which].name)) as Potions;
        if (Inventory.instance.items.Contains(inventoryPotion))
        {
            Inventory.instance.RemovePotion(inventoryPotion);
        }
        else
        {
            currentPotions[which] = null;
            icons[which].sprite = null;
            icons[which].enabled = false;
        }
    }

    public void ClearPotions(int index)
    {
        icons[index].sprite = null;
        icons[index].enabled = false;
    }
}
