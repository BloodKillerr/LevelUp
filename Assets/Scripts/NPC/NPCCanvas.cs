﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCanvas : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerManager.instance.player.transform);
    }
}
