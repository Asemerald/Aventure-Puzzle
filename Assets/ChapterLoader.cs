using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterLoader : MonoBehaviour
{
    
    public static ChapterLoader Instance { get; private set; }
    
    public int ChapterToLoad;
    
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    
    
    private void Start()
    {
        ChapterToLoad = 0;
        
        DontDestroyOnLoad(gameObject);
    }
    
    public void LoadChapter(int index)
    {
        switch(index)
        {
            case 1:
                ChapterToLoad = 1;
                SceneManager.LoadScene(2);
                break;
            case 2:
                ChapterToLoad = 2;
                SceneManager.LoadScene(2);
                break;
            case 3:
                ChapterToLoad = 3;
                SceneManager.LoadScene(2);
                break;
            case 4:
                ChapterToLoad = 4;
                SceneManager.LoadScene(3);
                break;
            case 5:
                ChapterToLoad = 5;
                SceneManager.LoadScene(3);
                break;
            case 6:
                ChapterToLoad = 6;
                SceneManager.LoadScene(3);
                break;
        }
    }
    
    
    
    
    
    
}
