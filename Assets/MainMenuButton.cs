using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MonoBehaviour
{
    [SerializeField] private Animator card;
    [SerializeField] private Light light;
    
    private Button button;
    
    private EventSystem eventSystem;

    
    
    private void Start()
    {
        button = GetComponent<Button>();
        eventSystem = EventSystem.current;
        
    }

    private void Update()
    {
        card.SetBool("Hover", eventSystem.currentSelectedGameObject == gameObject);
        light.gameObject.SetActive(eventSystem.currentSelectedGameObject == gameObject);
    }

    public void IsClicked()
    {
        card.SetTrigger("Clicked");
        light.gameObject.SetActive(false);
    }

    public void BackPress()
    {
        card.SetTrigger("Back");
    }
    
    
}
