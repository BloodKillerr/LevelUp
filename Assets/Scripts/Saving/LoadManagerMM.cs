using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadManagerMM : MonoBehaviour
{
    [SerializeField]
    private SavedGame[] saveSlots = null;

    [SerializeField]
    private GameObject dialogue = null;

    [SerializeField]
    private TMP_Text dialogueText = null;

    private SavedGame currentSavedGame;

    private string action;

    public SavedGame[] MySaveSlots { get => saveSlots; set => saveSlots = value; }

    public GameObject blackCanvas;

    // Start is called before the first frame update
    void Start()
    {
        foreach (SavedGame saved in MySaveSlots)
        {
            //We need to show the saved files here
            ShowSavedFiles(saved);
        }
    }

    private void ShowSavedFiles(SavedGame savedGame)
    {
        string name = savedGame.gameObject.name;
        name = name.Remove(0, 1);

        if (File.Exists(Application.persistentDataPath + "/" + name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            savedGame.ShowInfo(data);
        }
    }

    public void CloseDialogue()
    {
        dialogue.SetActive(false);
    }

    public void CloseSave()
    {
        SaveWindow.instance.Close();
        CloseDialogue();
    }

    public void ShowDialogue(GameObject clickButton)
    {
        CloseDialogue();
        currentSavedGame = clickButton.GetComponentInParent<SavedGame>();

        string name = currentSavedGame.gameObject.name;
        name = name.Remove(0, 1);

        action = clickButton.name;

        if (File.Exists(Application.persistentDataPath + "/" + name + ".dat"))
        {
            switch (action)
            {
                case "Load":
                    dialogueText.text = "Wczytać grę?";
                    break;
                case "Delete":
                    dialogueText.text = "Usunąć zapis gry?";
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
            case "Delete":
                Delete(currentSavedGame);
                break;
        }
        CloseDialogue();
    }

    public void Delete(SavedGame savedGame)
    {
        string name = savedGame.gameObject.name;
        name = name.Remove(0, 1);
        File.Delete(Application.persistentDataPath + "/" + name + ".dat");
        PlayerPrefs.DeleteKey("Load");
        PlayerPrefs.Save();
        savedGame.HideVisuals();
    }

    private void LoadScene(SavedGame savedGame)
    {
        string name = savedGame.gameObject.name;
        name = name.Remove(0, 1);
        if (File.Exists(Application.persistentDataPath + "/" + name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + name + ".dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            PlayerPrefs.SetInt("Load", savedGame.MyIndex);
            PlayerPrefs.Save();
            blackCanvas.SetActive(true);
            SceneManager.LoadScene(data.MyScene);
        }
    }
}