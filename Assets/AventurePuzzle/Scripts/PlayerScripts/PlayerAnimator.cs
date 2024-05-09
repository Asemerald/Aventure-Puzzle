using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private InputsBrain _inputsBrain;

    private void Awake()
    {
        if (TryGetComponent(out Animator animator))
        {
            _animator = animator;
        }
        else Debug.LogError("No Animator component found on " + gameObject.name);
        
        if (TryGetComponent(out PlayerController playerController))
        {
            _playerController = playerController;
        }
        else Debug.LogError("No PlayerController component found on " + gameObject.name);
        
        if (TryGetComponent(out InputsBrain inputsBrain))
        {
            _inputsBrain = inputsBrain;
        }
        else Debug.LogError("No InputsBrain component found on " + gameObject.name);
    }


    private void Update()
    {
        //_animator.SetFloat("Speed", );
    }
}
