using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SizeInteraction : InteractibleTemplate
{
    [Header("Size Settings")]
    [SerializeField] Vector3 initialSize;
    [SerializeField] Vector3 UpsideSize;
    [SerializeField] Vector3 UpsideDownSize;

    public enum SizeState { Shrink, Normal, Bigger}
    [Header("Current object state")]
    public SizeState currentState;

    [Header("Move Settings")]
    [SerializeField] MoveableObject moveableObject;

    private void Start()
    {
        moveableObject = GetComponent<MoveableObject>();
        UpdateMoveAbility();
    }

    public override void BaseState()
    {
        transform.localScale = initialSize;
        currentState = SizeState.Normal;
        UpdateMoveAbility();
    }

    public override void UpsideState()
    {
        transform.localScale = UpsideSize;
        currentState = SizeState.Bigger;
        UpdateMoveAbility();
    }

    public override void UpsideDownState()
    {
        transform.localScale = UpsideDownSize;
        currentState = SizeState.Shrink;
        UpdateMoveAbility();
    }

    void UpdateMoveAbility()
    {
        if(currentState == SizeState.Shrink)
            moveableObject.canBeMoved = true;
        else
            moveableObject.canBeMoved = false;
    }
}
