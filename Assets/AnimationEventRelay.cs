using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventRelay : MonoBehaviour
{
    [SerializeField] private PlayerAnimator _playerAnimator;
    
    private void Start()
    {
        if(_playerAnimator == null)
        {
            Debug.LogError("No PlayerAnimator component found on " + gameObject.name);
        }
        
    }
    
    public void TriggerAlternativeIdle()
    {
        _playerAnimator.AlternativeIdle();
    }
}
