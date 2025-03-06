using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathLoadSystem : MonoBehaviour
{
    #region Singleton

    public static DeathLoadSystem instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField]
    private SavedGame[] saveSlots = null;

    [SerializeField]
    private GameObject dialogue = null;

    [SerializeField]
    private TMP_Text dialogueText = null;

    private SavedGame currentSavedGame;

    private string action;

    public SavedGame[] MySaveSlots { get => saveSlots; set => saveSlots = value; }

    public void ShowSavedFiles(SavedGame savedGame)
    {
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void ShowDialogue(GameObject clickButton)
    {
        CloseDialogue();
        currentSavedGame = clickButton.GetComponentInParent<SavedGame>();

        action = clickButton.name;

        if (File.Exists(Application.persistentDataPath + "/" + currentSavedGame.gameObject.name + ".dat"))
        {
            switch (action)
            {
                case "Load":
                    dialogueText.text = "Wczytać grę?";
                    break;
            }
            dialogue.SetActive(true);
        }
    }

    public void ExecuteAction()
    {
        switch (action)
        {
            case "Load":
                LoadScene(currentSavedGame);
                break;
        }
    }

    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    private void LoadScene(SavedGame savedGame)
    {
        if (File.Exists(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + savedGame.gameObject.name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("Load", savedGame.MyIndex);
            PlayerPrefs.Save();
            SceneManager.LoadScene(data.MyScene);
            DeathWindow.instance.Close();
            CloseDialogue();
        }
    }
}
