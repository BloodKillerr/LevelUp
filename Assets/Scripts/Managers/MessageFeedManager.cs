using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MessageFeedManager : MonoBehaviour
{
    [SerializeField]
    private GameObject messagePrefab = null;

    private static MessageFeedManager instance;

    public static MessageFeedManager MyInstance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MessageFeedManager>();
            }
            return instance;
        }
    }

    public void WriteMessage(string message)
    {
        if(gameObject.transform.childCount < 8)
        {
            GameObject go = Instantiate(messagePrefab, transform);
            go.GetComponent<TMP_Text>().text = message;

            go.transform.SetAsFirstSibling();

            Destroy(go, 3f);
        }
        else
        {
            Destroy(gameObject.transform.GetChild(7).gameObject);
            GameObject go = Instantiate(messagePrefab, transform);
            go.GetComponent<TMP_Text>().text = message;

            go.transform.SetAsFirstSibling();

            Destroy(go, 3f);
        }
    }

    public void ClearFeed()
    {
        foreach(Transform child in gameObject.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
