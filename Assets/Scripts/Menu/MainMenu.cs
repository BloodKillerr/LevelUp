using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainPanel;

    public GameObject blackCanvas;

    void Start()
    {
        MainPanel.SetActive(true);
    }

    void Update()
    {

    }

    public void StartGame(int sceneIndex)
    {
        PlayerPrefs.DeleteKey("Load");
        blackCanvas.SetActive(true);
        SceneManager.LoadScene(sceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
