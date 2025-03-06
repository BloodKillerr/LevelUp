using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupMesh : MonoBehaviour
{
    private GameObject itemMesh;
    
    void Start()
    {
        itemMesh = gameObject.GetComponent<ItemPickup>().item.mesh;
        Instantiate(itemMesh, gameObject.transform.position, itemMesh.transform.rotation, gameObject.transform);
    }
}