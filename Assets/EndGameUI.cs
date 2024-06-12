using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndGameUI : MonoBehaviour
{
    
    private EventSystem _eventSystem;
    public GameObject Button;

    private void Start()
    {
        _eventSystem = FindObjectOfType<EventSystem>();
        _eventSystem.SetSelectedGameObject(Button);
    }

    public void Controller()
    {
        SceneManager.LoadScene(4);
    }

    public void Liberer()
    {
        SceneManager.LoadScene(5);
    }
    
    
    
}
