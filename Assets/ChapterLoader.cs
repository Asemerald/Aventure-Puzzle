using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        StartCoroutine(LoadChapterAsync(index));
    }
    
    private IEnumerator LoadChapterAsync(int index)
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        
        
    }
    
    
    
    
}
