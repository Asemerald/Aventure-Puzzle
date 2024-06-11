using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRoom : MonoBehaviour
{
    public int roomNum;


    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelLoader.Instance.currentRoom = roomNum;
            LevelLoader.Instance.ManageLevels();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelLoader.Instance.ManageLevels();
        }
    }
}
