using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ConfinerSwitcher : MonoBehaviour
{

    private GameObject _CameraFollowPlayer;
    
    private void Start()
    {
        _CameraFollowPlayer = GameObject.FindGameObjectWithTag("CameraFollow");
    }

    private void OnTriggerEnter(Collider other)
    {
        _CameraFollowPlayer.GetComponent<CinemachineConfiner>().m_BoundingVolume = transform.parent.GetComponent<Collider>();
    }
}
