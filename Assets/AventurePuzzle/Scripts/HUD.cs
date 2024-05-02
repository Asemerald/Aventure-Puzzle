using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    [Header("Tarot Settings")]
    public GameObject tarotInventory;
    public Transform tarotSelect;
    public Animator[] cardsPos;

    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Grab")]
    public GameObject grabObj;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
    
}
