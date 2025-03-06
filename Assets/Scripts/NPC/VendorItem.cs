using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class VendorItem
{
    [SerializeField]
    private Item item = null;

    [SerializeField]
    private int quantity = 0;

    [SerializeField]
    private bool unlimited = false;

    public Item MyItem { get => item; set => item = value; }

    public int MyQuantity { get => quantity; set => quantity = value; }

    public bool MyUnlimited { get => unlimited; set => unlimited = value; }
}
