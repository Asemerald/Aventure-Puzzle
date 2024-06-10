using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonCadre : MonoBehaviour
{
    private TMP_Dropdown dropdown;
    private Toggle toggle;
    private Button button;

    private bool Button;
    private bool Toggle;
    private bool Dropdown;
    
    private EventSystem eventSystem;
    
    [SerializeField] private GameObject Cadre;

    private void Start()
    {
        eventSystem = EventSystem.current;
        
        Cadre.SetActive(false);
        //try to get button, else try to get toggle, else try to get dropdown
        if(GetComponent<Button>() != null)
        {
            button = GetComponent<Button>();
        }
        else if(GetComponent<Toggle>() != null)
        {
            toggle = GetComponent<Toggle>();
        }
        else if(GetComponent<TMP_Dropdown>() != null)
        {
            dropdown = GetComponent<TMP_Dropdown>();
        }
        
    }

    private void Update()
    {
        //if button is not null, check if button is selected, if so Enable cadre, else disable it
        if (button != null)
        {
            if (button == EventSystem.current.currentSelectedGameObject)
            {
                Cadre.SetActive(true);
            }
            else
            {
                Cadre.SetActive(false);
            }
        }
        
        //if toggle is not null, check if toggle is selected, if so Enable cadre, else disable it
        if (toggle != null)
        {
            if (toggle == EventSystem.current.currentSelectedGameObject)
            {
                Cadre.SetActive(true);
            }
            else
            {
                Cadre.SetActive(false);
            }
        }
        
        //if dropdown is not null, check if dropdown is selected, if so Enable cadre, else disable it
        if (dropdown != null)
        {
            if (dropdown == eventSystem.currentSelectedGameObject == gameObject)
            {
                Cadre.SetActive(true);
            }
            else
            {
                Cadre.SetActive(false);
            }
        }
    }
}
