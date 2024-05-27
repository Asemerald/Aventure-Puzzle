using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }

    [Header("Pause Panel")]
    public GameObject pausePanel;

    [Header("Grab")]
    public GameObject grabObj;

    [Header("Astral Pocket")]
    public Slider astralSlider;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        astralSlider.minValue = 0;
        astralSlider.maxValue = AstralPocket.Instance.timeToReset;
    }

}
