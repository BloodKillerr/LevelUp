using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RealToolTip : MonoBehaviour
{
    #region Singleton
    public static RealToolTip instance;

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

    public TMP_Text itemName;
    public TMP_Text itemRarity;
    public TMP_Text itemPropertyOne;
    public TMP_Text itemPropertyTwo;
    public TMP_Text itemDescription;
    public TMP_Text itemPrice;
    public Image itemIcon;

    public void SetTooltip(Item item)
    {
        Item itemToSet = item;
        itemName.text = itemToSet.name;
        itemRarity.text = itemToSet.rarity;
        itemPropertyOne.text = itemToSet.property1;
        itemPropertyTwo.text = itemToSet.property2;
        itemDescription.text = itemToSet.description;
        itemPrice.text = itemToSet.price.ToString();
        itemIcon.sprite = itemToSet.icon;
    }
}
