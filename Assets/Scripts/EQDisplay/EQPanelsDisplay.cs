using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EQPanelsDisplay : MonoBehaviour
{
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
