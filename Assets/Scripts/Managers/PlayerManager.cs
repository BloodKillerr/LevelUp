using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using TMPro;
using System.Runtime.Serialization.Formatters.Binary;

public class PlayerManager : MonoBehaviour
{
    #region Singleton

    public static PlayerManager instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    #endregion

    GameObject gameManager;

    [HideInInspector]
    public GameObject player;

    public void KillPlayer()
    {
        Time.timeScale = 0f;
        InventoryUI.instance.canBeOpened = false;
        gameManager.GetComponent<PauseMenu>().canBeOpened = false;
        DeathWindow.instance.Open();

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        GameObject playerCamera = GameObject.FindGameObjectWithTag("PlayerCamera");
        playerCamera.GetComponent<PlayerLook>().enabled = false;

        foreach (SavedGame saved in DeathLoadSystem.instance.MySaveSlots)
        {
            //We need to show the saved files here
            DeathLoadSystem.instance.ShowSavedFiles(saved);
        }
    }

    public void DestroyUndestroyable()
    {
        SceneManager.MoveGameObjectToScene(player, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(gameManager, SceneManager.GetActiveScene());
        SceneManager.MoveGameObjectToScene(InventoryUI.instance.gameObject, SceneManager.GetActiveScene());

        SceneManager.sceneLoaded -= SaveManager.instance.OnSceneWasChanged;
    }

    public void SetDefaultValues()
    {
        PlayerStats ps = player.GetComponent<PlayerStats>();
        LevelingSystem ls = LevelingSystem.instance;
        TraitsSystem ts = TraitsSystem.instance;

        ps.MyCurrentHealth = 100;
        ps.maxHealth = 100;
        ps.currentMana = 100;
        ps.maxMana = 100;
        ps.MyTitle = "Początkujący";

        ls.currentExp = 0;
        ls.requiredExp = 100;
        ls.level = 1;
        ls.skillPoints = 0;
        ls.hpUpgrade = 50;
        ls.mpUpgrade = 25;
    }
}
