using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPath : MonoBehaviour
{
    public GameObject[] objects;

    public void SwitchObjects()
    {
        foreach(GameObject obj in objects)
        {
            if(obj.activeInHierarchy)
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
