using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChestDisplay : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup = null;

    public TMP_Text chestText;

    public Button yesButton;

    public Button noButton;

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
