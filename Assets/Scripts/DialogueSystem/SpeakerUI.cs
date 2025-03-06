using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpeakerUI : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    private Character speaker;

    public Character Speaker
    {
        get
        {
            return speaker;
        }
        set
        {   
            speaker = value;
            nameText.text = speaker.characterName;
        }
    }

    public string Dialogue
    {
        set
        {
            dialogueText.text = value;
        }
    }

    public bool HasSpeaker()
    {
        return speaker != null;
    }

    public bool SpeakerIs(Character character)
    {
        return speaker == character;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
