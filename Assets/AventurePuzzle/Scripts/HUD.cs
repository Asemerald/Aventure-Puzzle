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

    public void UpdateHUDCard(int i, CardTemplate.CardState state)
    {
        cardsPos[i].Play(ChooseState(state));
    }

    string ChooseState(CardTemplate.CardState state)
    {
        switch (state)
        {
            case CardTemplate.CardState.None:
                return "CardIdle";
            case CardTemplate.CardState.Endroit:
                return "CardEndroit";
            case CardTemplate.CardState.Envers:
                return "CardEnvers";
        }

        return "";
    }
}
