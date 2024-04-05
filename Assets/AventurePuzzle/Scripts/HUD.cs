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
    public Transform[] cardsPos;

    [Header("Pause Panel")]
    public GameObject pausePanel;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void UpdateHUDCard(int i, string state)
    {
        cardsPos[i].GetComponentInChildren<TextMeshProUGUI>().text = state;
    }
}
