using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableSave : MonoBehaviour
{
    public void SaveOff()
    {
        SaveButtonHolder sbh = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveButtonHolder>();
        sbh.saveButton.GetComponent<Button>().interactable = false;
    }

    public void SaveOn()
    {
        SaveButtonHolder sbh = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SaveButtonHolder>();
        sbh.saveButton.GetComponent<Button>().interactable = true;
    }
}
