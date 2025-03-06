using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Interactable
{
    public int sceneIndex;

    public GameObject portalCanvas;
    public GameObject acceptPanel;

    [SerializeField]
    private TMP_Text sceneNameText;

    private bool accept = false;
    private string sceneName;

    public TMP_Text MySceneNameText { get => sceneNameText; set => sceneNameText = value; }

    public override void Interact()
    {
        base.Interact();

        if(accept)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                if(sceneIndex == 33)
                {
                    PlayerManager.instance.DestroyUndestroyable();
                } 
                SceneManager.LoadScene(sceneIndex);
            }
        }
        else
        {
            acceptPanel.SetActive(true);
            accept = true;
        }
    }

    public override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.CompareTag("Player"))
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(sceneIndex);
            sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            MySceneNameText.text = string.Format("Portal do: {0}", sceneName);
            portalCanvas.SetActive(true);
        }
    }

    public override void OnTriggerExit(Collider other)
    {
        base.OnTriggerExit(other);
        if(other.CompareTag("Player"))
        {
            portalCanvas.SetActive(false);
            acceptPanel.SetActive(false);
            accept = false;
        }       
    }
}
