using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossDeath : MonoBehaviour
{
    public GameObject[] objects;

    private void OnDestroy()
    {
        objects = GameObject.FindGameObjectsWithTag("blockObj");
        SwitchObjects();
    }

    public void SwitchObjects()
    {
        foreach (GameObject obj in objects)
        {
            if (obj.activeInHierarchy)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
    }
}
