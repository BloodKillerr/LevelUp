using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(4, 10)]
    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    public Character NPC;
    public Character MainCharacter;
    public Line[] lines;
}
