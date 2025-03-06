using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public GameObject MainCanvas;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(MainCanvas);
    }

    public void GoBackToMainMenu(int sceneIndex)
    {
        PlayerManager.instance.DestroyUndestroyable();
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneIndex);      
    }
}
