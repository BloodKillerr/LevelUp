using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item;

    public GameObject itemPickupAudioObj;

    public int howMany = 1;

    public override void Interact()
    {
        base.Interact();

        PickUp();
    }

    public void PickUp()
    {
        item.amount = howMany;
        bool wasPickedUp = Inventory.instance.Add(item);
        GameObject go = Instantiate(itemPickupAudioObj, gameObject.transform.position, Quaternion.identity);
        Destroy(go, 1f);
        item.amount = 1;

        if(wasPickedUp)
        {
            Destroy(gameObject);
        }
    }
}
