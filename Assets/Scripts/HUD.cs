using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    public GameObject tarotInventory;
    public Transform tarotSelect;
    public Transform[] cardsPos;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }


}
