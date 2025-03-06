using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiddleBehaviour : MonoBehaviour
{
    public delegate void RiddleEventHandler(RiddleInteraction riddle);
    public static event RiddleEventHandler OnRiddleTriggered;

    public static void RiddleTriggered(RiddleInteraction riddle)
    {
        OnRiddleTriggered?.Invoke(riddle);
    }
}
