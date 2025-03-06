using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDeath : MonoBehaviour
{
    public GameObject[] objects;

    private void OnDestroy()
    {
        objects = GameObject.FindGameObjectsWithTag("blockObj");
        SwitchObjects();
        SaveOn();
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

    public void SaveOn()
    {
        if(GameObject.FindGameObjectWithTag("GameManager"))
        {
            SaveButtonHolder sbh = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveButtonHolder>();
            sbh.saveButton.GetComponent<Button>().interactable = true;
        }       
    }
}
