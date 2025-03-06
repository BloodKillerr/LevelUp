using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponEquip : MonoBehaviour
{
    public GameObject hand;

    void Awake()
    {
        hand = GameObject.FindGameObjectWithTag("hand");      
    }

    private void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += EquipWeapon;
    }

    public void EquipWeapon(Equipment newItem, Equipment oldItem)
    {
        if (oldItem != null)
        {
            if((int)oldItem.equipSlot == 5)
            {
                foreach (Transform child in hand.transform)
                {
                    Destroy(child.gameObject);
                }
            }          
        }

        if (newItem != null)
        {
            if((int)newItem.equipSlot == 5)
            {
                GameObject mesh = Instantiate(newItem.mesh,hand.transform);
                mesh.transform.localPosition = new Vector3(0f, 0f, 0f);
                mesh.transform.localEulerAngles = new Vector3(0f, -75f, -40f);
            }         
        }
    }
}
