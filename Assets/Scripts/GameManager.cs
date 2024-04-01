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
        {
            HUD.Instance.tarotInventory.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            TarotInventory.Instance.ApplyCard();
            HUD.Instance.tarotInventory.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
