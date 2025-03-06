using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class HideInEditor : MonoBehaviour
{
    public bool hide = false;

    public List<GameObject> objsToHide = new List<GameObject>();

    private void Update()
    {
        if(objsToHide.Count != 0)
        {
            foreach (GameObject obj in objsToHide)
            {
                if (obj.activeSelf && hide == true)
                {
                    obj.SetActive(false);
                }
                else if (!obj.activeSelf && hide == false)
                {
                    obj.SetActive(true);
                }
            }
        }
    }
}
