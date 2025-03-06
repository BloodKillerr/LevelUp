using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBossRoom : MonoBehaviour
{
    BlockPath blockPath;
    DisableSave disableSave;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            blockPath = GetComponent<BlockPath>();
            blockPath.SwitchObjects();
            disableSave = GetComponent<DisableSave>();
            disableSave.SaveOff();
            Destroy(gameObject);
        }
    }
}
