using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public float triggerDistance = 2f;

    private Animation _anim;
    private bool _isTriggered = false;
    [SerializeField] private float _playerDistance = 0;
    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animation>() != null)
        {
            _anim = GetComponent<Animation>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _playerDistance = Vector3.Distance(PlayerController.Instance.transform.position, gameObject.transform.position);
        if (_anim != null && !_isTriggered)
        {
            if (_playerDistance < triggerDistance)
            {
                _isTriggered = true;
                _anim.Play();
            }
        }
    }
}
