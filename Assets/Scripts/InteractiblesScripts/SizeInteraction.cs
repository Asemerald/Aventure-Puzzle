using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeInteraction : InteractibleTemplate
{
    [Header("Size Settings")]
    [SerializeField] Vector3 initialSize;
    [SerializeField] Vector3 UpsideSize;
    [SerializeField] Vector3 UpsideDownSize;


    public override void BaseState()
    {
        transform.localScale = initialSize;
    }

    public override void UpsideState()
    {
        transform.localScale = UpsideSize;
    }

    public override void UpsideDownState()
    {
        transform.localScale = UpsideDownSize;
    }
}
