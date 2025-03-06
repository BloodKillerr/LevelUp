using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Vector3 center;
    public float radius = 3f;

    protected bool triggered;

    public InteractIcon interactIcon;
    protected GameObject gameManager;

    protected PauseMenu pauseMenu;
    protected InventoryUI inventoryUI;

    void Start()
    {
        SphereCollider cd = gameObject.AddComponent<SphereCollider>();
        cd.isTrigger = true;
        cd.center = center;
        cd.radius = radius;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        pauseMenu = gameManager.GetComponent<PauseMenu>();
        inventoryUI = InventoryUI.instance;
        interactIcon = GameObject.FindGameObjectWithTag("InteractIcon").GetComponent<InteractIcon>();
    }

    private void Update()
    {
        if(pauseMenu.gameIsPaused==false && inventoryUI.isEQDisplayed==false)
        {
            if (triggered)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Interact();
                }
            }
        }
    }

    public virtual void Interact()
    {
        //Meant to be overwritten
    }

    public virtual void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactIcon.Open();
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggered = true;
            interactIcon.Open();
        }
    }

    public virtual void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            triggered = false;
            interactIcon.Close();
        }
    }

    public virtual void OnDestroy()
    {
        if(interactIcon && triggered)
        {
            interactIcon.Close();
            triggered = false;
        }
    }
}
