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
    
    private Button button;
    
    private EventSystem eventSystem;

    public bool isPlayButton;
    
    private void Start()
    {
        button = GetComponent<Button>();
        eventSystem = EventSystem.current;
        
        button.onClick.AddListener(ButtonClicked);
        
    }

    private void Update()
    {
        card.SetBool("Hover", eventSystem.currentSelectedGameObject == gameObject);
        
        //On button click, set Selected Bool to true
        
        
    }


    public void Select()
    {
        eventSystem.SetSelectedGameObject(gameObject);
    }
    
    public void ButtonClicked()
    {
        card.SetBool("Selected", true);
        Debug.Log("submit");
    }
    
    public void OnCancel()
    {
        card.SetBool("Selected", false);
    }
    
}
