using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChangeTrigger : MonoBehaviour
{
    [Header("Area")] 
    [SerializeField] private MusicArea area;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            AudioManager.instance.SetMusicArea(area);
        }
    }
}
