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

    
    public bool isOptionsButton;
    public bool isChapterButton;
    public bool isPlayButton;
    public bool isCreditsButton;
    public bool isQuitButton;
    
    private void Start()
    {
        button = GetComponent<Button>();
        eventSystem = EventSystem.current;
        
    }
    
    public void IsHovering(bool isHovering)
    {
        card.SetBool("Hover", isHovering);
    }
    
    public void IsClicked()
    {
        card.SetTrigger("Clicked");
        
        if (isOptionsButton)
        {
           MainMenuManager.Instance.OpenMenuPanel(1);
        }
        else if (isChapterButton)
        {
            MainMenuManager.Instance.OpenMenuPanel(2);
        }
        else if (isPlayButton)
        {
            MainMenuManager.Instance.OpenMenuPanel(3);
        }
        else if (isCreditsButton)
        {
            MainMenuManager.Instance.OpenMenuPanel(4);
        }
        else if (isQuitButton)
        {
            MainMenuManager.Instance.OpenMenuPanel(5);
        }
    }
    
    
}
