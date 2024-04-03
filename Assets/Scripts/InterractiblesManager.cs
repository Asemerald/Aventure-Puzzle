using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterractiblesManager : MonoBehaviour
{
    public static InterractiblesManager Instance {  get; private set; }

    public List<InteractibleTemplate> interactibles = new List<InteractibleTemplate>();

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    public void ApplyChanges(InteractibleTemplate.CardDirectModification card, CardTemplate.CardState newState)
    {
        foreach(var i in interactibles)
        {
            if(i.cardInteract == card)
            {
                i.currentCardState = newState;
                switch (newState)
                {
                    case CardTemplate.CardState.None:
                        i.BaseState();
                        break;
                    case CardTemplate.CardState.Endroit:
                        i.UpsideState();
                        break;
                    case CardTemplate.CardState.Envers:
                        i.UpsideDownState();
                        break;
                }
            }
        }
    }

}
