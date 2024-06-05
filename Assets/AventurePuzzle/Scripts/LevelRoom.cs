using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour
{
    public int roomNum;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
            LevelLoader.Instance.currentRoom = roomNum;
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.CompareTag("Player"))
            LevelLoader.Instance.currentRoom = roomNum;
    }
}
