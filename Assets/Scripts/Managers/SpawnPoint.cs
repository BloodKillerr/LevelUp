using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private Transform player;

    [SerializeField]
    private bool wasUsed = false;

    [SerializeField]
    private int id = 0;

    public bool MyWasUsed { get => wasUsed; set => wasUsed = value; }
    public int MyID { get => id; set => id = value; }

    public void Start()
    {         
        if(!wasUsed)
        {
            player = PlayerManager.instance.player.transform;

            player.gameObject.GetComponent<CharacterController>().enabled = false;
            player.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
            player.gameObject.GetComponent<CharacterController>().enabled = true;

            MyWasUsed = true;
        }
    }
}