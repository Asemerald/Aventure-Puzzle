using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterLoaderIndex : MonoBehaviour
{
    
    [SerializeField] private CinematicMarketStart VFXStart;
    [SerializeField] private LevelLoader RoomLoader;
    [SerializeField] private GameObject[] _chapters;
    [SerializeField] private GameObject[] _cameraConfiners;
    [SerializeField] private int[] _spawnRoomsNumbers;
    
    [SerializeField] private CinematicSpace ScriptToChange;
    
    public static ChapterLoaderIndex Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        
        if (ChapterLoader.Instance.ChapterToLoad == 0) return;
        
        if (ChapterLoader.Instance.ChapterToLoad == 1)
        {
            _cameraConfiners[0].GetComponentInChildren<ConfinerSwitcher>().SwitchConfiner();
            VFXStart.StartPos = _chapters[0].transform;
            RoomLoader.startingRoom = _spawnRoomsNumbers[0];
        }
        
        if (ChapterLoader.Instance.ChapterToLoad == 2)
        {
            _cameraConfiners[1].GetComponentInChildren<ConfinerSwitcher>().SwitchConfiner();
            VFXStart.StartPos = _chapters[1].transform;
            RoomLoader.startingRoom = _spawnRoomsNumbers[1];
        }
        
        if (ChapterLoader.Instance.ChapterToLoad == 3)
        {
            _cameraConfiners[2].GetComponentInChildren<ConfinerSwitcher>().SwitchConfiner();
            VFXStart.StartPos = _chapters[2].transform;
            RoomLoader.startingRoom = _spawnRoomsNumbers[2];
        }
        
        if (ChapterLoader.Instance.ChapterToLoad == 4)
        {
            ScriptToChange.posToTp = _chapters[3].transform;
            ScriptToChange.DoCinematic = false;
        }
        
        if (ChapterLoader.Instance.ChapterToLoad == 5)
        {
            ScriptToChange.posToTp = _chapters[4].transform;
            ScriptToChange.DoCinematic = false;
        }
        
        if (ChapterLoader.Instance.ChapterToLoad == 6)
        {
            ScriptToChange.posToTp = _chapters[5].transform;
            ScriptToChange.DoCinematic = false;
        }
    }
    
}
