using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {  get; private set; }

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
        }
        else
        {
            HUD.Instance.pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }
    
}
