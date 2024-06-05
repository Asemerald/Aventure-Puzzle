using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ConfinerSwitcher : MonoBehaviour
{
    
    private CinemachineConfiner _cinemachineConfiner;
    
    private void Start()
    {
        _cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
    }

    private void OnTriggerExit(Collider other)
    {
        //TRY GET collider in parent, if no collider found get mesh collider in parent
       
        if (transform.parent.TryGetComponent(out Collider parentCollider))
        {
            _cinemachineConfiner.m_Damping = 2;
            _cinemachineConfiner.m_BoundingVolume = parentCollider;
            
            //Start a coroutine to reset damping after 2s
            StartCoroutine(ResetDamping());
        }
        else if (transform.parent.TryGetComponent(out MeshCollider meshCollider))
        {
            _cinemachineConfiner.m_Damping = 2f;
            _cinemachineConfiner.m_BoundingVolume = meshCollider;
            
            //Start a coroutine to reset damping after 2s
            StartCoroutine(ResetDamping());
        }
    }
    
    private IEnumerator ResetDamping()
    {
        yield return new WaitForSeconds(2f);
        _cinemachineConfiner.m_Damping = 0f;
    }
    
}
