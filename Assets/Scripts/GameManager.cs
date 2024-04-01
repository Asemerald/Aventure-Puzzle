using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public bool inTarotInventory = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void CheckTarotInventory()
    {
        inTarotInventory = !inTarotInventory;

        if (inTarotInventory)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
