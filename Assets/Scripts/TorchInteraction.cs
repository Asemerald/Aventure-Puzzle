using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchInteraction : InteractibleTemplate
{
    [Header("Torch Settings")]
    [SerializeField] GameObject fireBox;
    [SerializeField] Vector3 fireboxLitSize;
    [SerializeField] Vector3 fireboxBurningSize;

    public enum TorchState { Extinguished, LitUp, Burning, Ember }
    public TorchState state;

    public override void BaseState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.LitUp;
        fireBox.transform.localScale = fireboxLitSize;
        fireBox.SetActive(true);
    }

    public override void UpsideState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.Burning;
        fireBox.transform.localScale = fireboxBurningSize;
        fireBox.SetActive(true);
    }

    public override void UpsideDownState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.Ember;
        fireBox.SetActive(false);
    }

    public void SetOnFire()
    {
        switch (currentCardState)
        {
            case CardTemplate.CardState.None:
                state = TorchState.LitUp;
                break;
            case CardTemplate.CardState.Endroit:
                state = TorchState.Burning;
                break;
            case CardTemplate.CardState.Envers:
                state = TorchState.Ember;
                break;
        }

        fireBox.SetActive(true);
    }
}
