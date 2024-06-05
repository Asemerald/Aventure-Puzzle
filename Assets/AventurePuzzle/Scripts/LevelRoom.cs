using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour
{
    public int roomNum;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            LevelLoader.Instance.currentRoom = roomNum;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            LevelLoader.Instance.currentRoom = roomNum;
    }
}
