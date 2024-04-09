using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

    public bool inTarotInventory = false;
    public bool gameIsPause = false;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void PauseGame()
    {
        gameIsPause = !gameIsPause;

        if (gameIsPause)
        {
            HUD.Instance.pausePanel.SetActive(true);
            Time.timeScale = 0;
            if(inTarotInventory)
                HUD.Instance.tarotInventory.SetActive(false);
        }
        else
        {
            HUD.Instance.pausePanel.SetActive(false);
            if (inTarotInventory)
            {
                HUD.Instance.tarotInventory.SetActive(true);
                return;
            }
            Time.timeScale = 1;
        }
    }

    public void CheckTarotInventory()
    {
        inTarotInventory = !inTarotInventory;

        if (inTarotInventory)
        {
            HUD.Instance.tarotInventory.SetActive(true);
            Time.timeScale = 0;
            TarotInventory.Instance.UpdateHUDState();
        }
        else
        {
            TarotInventory.Instance.ApplyCard();
            HUD.Instance.tarotInventory.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
