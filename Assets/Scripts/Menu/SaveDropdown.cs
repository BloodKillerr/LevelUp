using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class SaveDropdown : MonoBehaviour
{
    public string PrefName;
    public TMP_Dropdown dropdown;

    void Awake()
    {
        dropdown.onValueChanged.AddListener(new UnityAction<int>(index =>
        {
            PlayerPrefs.SetInt(PrefName, dropdown.value);
            PlayerPrefs.Save();
        }));
    }

    void Start()
    {
        dropdown.value = PlayerPrefs.GetInt(PrefName, 0);
    }

}
