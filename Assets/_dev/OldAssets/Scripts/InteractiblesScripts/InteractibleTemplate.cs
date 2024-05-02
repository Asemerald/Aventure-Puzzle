using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractibleTemplate : MonoBehaviour
{
    //All the children will herit of the variable and fonction of this script
    public enum CardDirectModification { None, Fire, Death, Size }
    public CardDirectModification cardInteract;

    public CardTemplate.CardState currentCardState;

    public virtual void BaseState()
    {
        Debug.Log("Interactible Template : no effects");
    }

    public virtual void UpsideState()
    {
        Debug.Log("Interactible Template : upside effect");
    }

    public virtual void UpsideDownState()
    {
        Debug.Log("Interactible Template : upside down effect");
    }
}
