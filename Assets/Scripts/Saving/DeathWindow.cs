using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathWindow : MonoBehaviour
{
    #region Singleton

    public static DeathWindow instance;

    private void Awake()
    {
        if (instance != null)
        {
            return;
        }
        instance = this;
    }

    #endregion

    [SerializeField]
    private CanvasGroup canvasGroup = null;

    public void Open()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public void Close()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
    }
}
