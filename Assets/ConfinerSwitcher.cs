using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Cinemachine.PostFX;
using UnityEngine;

public class ConfinerSwitcher : MonoBehaviour
{
    
    private CinemachineConfiner _cinemachineConfiner;
    private CinemachineVirtualCamera _cameraTrackedDolly;
    
    private CinemachineVirtualCamera _cinemachineVirtualCamera;

    [SerializeField] private bool SwitchToDollyTrack;
     
    private void Start()
    {
        _cinemachineConfiner = FindObjectOfType<CinemachineConfiner>();
        _cinemachineVirtualCamera = _cinemachineConfiner.GetComponent<CinemachineVirtualCamera>();
        if (FindObjectOfType<CinemachinePostProcessing>())
        {
            _cameraTrackedDolly = FindObjectOfType<CinemachinePostProcessing>().gameObject.GetComponent<CinemachineVirtualCamera>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
        if (_cinemachineVirtualCamera.Priority != 10)
        {
            _cinemachineVirtualCamera.Priority = 10;   
        }
        
        
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

    private void OnTriggerEnter(Collider other)
    {
        if (SwitchToDollyTrack)
        {
            _cinemachineVirtualCamera.Priority = 0;
            //switch _cinemachineConfiner.GetComponent<CinemachineVirtualCamera>() to dolly track
            _cameraTrackedDolly.GetComponent<CinemachineTrackedDolly>().m_Path = transform.parent.GetComponent<CinemachinePath>();
        }
    }

    private IEnumerator ResetDamping()
    {
        yield return new WaitForSeconds(2f);
        _cinemachineConfiner.m_Damping = 0f;
    }
    
}
