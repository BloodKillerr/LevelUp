using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gold Pouch", menuName = "Inventory/Consumables/Gold Pouch")]
public class GoldPouch : Consumables
{
    public int gold = 20;

    public GameObject coinAudio;

    public GoldPouch()
    {
        property2 = "Złoto: " + gold;
    }

    public override void Use()
    {
        base.Use();

        if(Input.GetKey(KeyCode.LeftShift))
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().money += amount * gold;
        }
        else
        {
            PlayerManager.instance.player.GetComponent<PlayerStats>().money += gold;
        }
        RemoveFromInventory();
        GameObject audio = Instantiate(coinAudio, PlayerManager.instance.player.transform.position, Quaternion.identity, PlayerManager.instance.player.transform);
        Destroy(audio, .5f);
    }
}
