using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public GameObject TriggerSnapShotPause;

    public bool gameIsPause = false;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PauseGame()
    {
        gameIsPause = !gameIsPause;

        if (gameIsPause)
        {
            HUD.Instance.Pause();
            TriggerSnapShotPause.SetActive(true);
        }
        else
        {
            HUD.Instance.Resume();
            TriggerSnapShotPause.SetActive(false);
        }
    }
}

