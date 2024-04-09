using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEnd : MonoBehaviour
{
    // If the player hit the end trigger, tell GM to end the game
    private void OnTriggerEnter(Collider other)
    {
        GameManager.instance.status = Status.End;
    }
}
