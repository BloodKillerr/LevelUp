using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public bool gameIsPaused = false;

    public bool canBeOpened = true;

    public GameObject pausePanel;

    [HideInInspector]
    public GameObject playerCamera;

    public GameObject optionsPanel;

    public GameObject mainPanel;

    public GameObject controlsPanel;

    private GameObject eQCanvas;

    void Start()
    {
        eQCanvas = GameObject.FindGameObjectWithTag("EQCanvas");
        playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        playerCamera.GetComponent<PlayerLook>().enabled = true;
        InventoryUI.instance.canBeOpened = true;
        SaveWindow.instance.Close();
        SaveManager.instance.CloseDialogue();

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        optionsPanel.SetActive(false);
        controlsPanel.SetActive(false);
        mainPanel.SetActive(true);
        pausePanel.SetActive(false);

        Time.timeScale = 1f;

        gameIsPaused = false;

        Animator[] animators = FindObjectsOfType<Animator>();

        foreach (Animator anim in animators)
        {
            anim.enabled = true;
        }

    }

    public void Pause()
    {
        if(canBeOpened)
        {
            playerCamera.GetComponent<PlayerLook>().enabled = false;
            InventoryUI.instance.canBeOpened = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            pausePanel.SetActive(true);

            Time.timeScale = 0f;

            gameIsPaused = true;

            Animator[] animators = FindObjectsOfType<Animator>();

            foreach(Animator anim in animators)
            {
                anim.enabled = false;
            }
        }
    }
}
