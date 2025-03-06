using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];

        onEquipmentChanged += PlayerManager.instance.player.GetComponent<PlayerStats>().OnEquipmentChanged;
    }
    #endregion

    public Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    public Image[] icons;

    Inventory inventory;

    public GameObject equipAudioObj;

    private void Start()
    {
        inventory = Inventory.instance;
    }

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            bool added = Inventory.instance.Add(oldItem);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(oldItem);
            }
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        icons[slotIndex].sprite = newItem.icon;
        icons[slotIndex].enabled = true;

        currentEquipment[slotIndex] = newItem;

        GameObject Eaudio = Instantiate(equipAudioObj, PlayerManager.instance.player.transform.position, Quaternion.identity, PlayerManager.instance.player.transform);
        Destroy(Eaudio, 1f);
    }

    public void EquipWithoutSound(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipSlot;

        Equipment oldItem = null;

        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex];
            bool added = Inventory.instance.Add(oldItem);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(oldItem);
            }
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        icons[slotIndex].sprite = newItem.icon;
        icons[slotIndex].enabled = true;

        currentEquipment[slotIndex] = newItem;
    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            bool added = Inventory.instance.Add(oldItem);

            if (!added)
            {
                Inventory.instance.DropOnTheGround(oldItem);
            }

            icons[slotIndex].sprite = null;
            icons[slotIndex].enabled = false;

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            RealToolTip.instance.gameObject.GetComponent<CanvasGroup>().alpha = 0;
            GameObject Uaudio = Instantiate(equipAudioObj, PlayerManager.instance.player.transform.position, Quaternion.identity, PlayerManager.instance.player.transform);
            Destroy(Uaudio, 1f);
        }
    }

    public void Clear(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {
            //PlayerManager.instance.player.GetComponent<PlayerStats>().armor.RemoveModifier(currentEquipment[slotIndex].armorModifier);
            //PlayerManager.instance.player.GetComponent<PlayerStats>().damage.RemoveModifier(currentEquipment[slotIndex].damageModifier);
            icons[slotIndex].sprite = null;
            icons[slotIndex].enabled = false;
            currentEquipment[slotIndex] = null;
        }
    }
}
