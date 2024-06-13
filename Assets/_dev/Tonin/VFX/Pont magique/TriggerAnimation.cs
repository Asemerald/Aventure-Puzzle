using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public float triggerDistance = 2f;
    [SerializeField] private float _objectDistance = 0;

    [Header("Alternative trigger object")]
    public bool alternativeTrigger = false;
    public GameObject triggerObject;

    private Animation _anim;
    [SerializeField] private bool _isTriggered = false;
    
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
	if (!alternativeTrigger)
	{
            _objectDistance = Vector3.Distance(PlayerController.Instance.transform.position, gameObject.transform.position);
	}
	else if (triggerObject != null)
	{ _objectDistance = Vector3.Distance(triggerObject.transform.position, gameObject.transform.position) ;}
        if (_anim != null && !_isTriggered)
        {
            if (_objectDistance < triggerDistance)
            {
                _isTriggered = true;
                _anim.Play();
            }
            
        }
    }
}
