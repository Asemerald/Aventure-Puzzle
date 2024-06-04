using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header ("Poche SFX")]
    [field: SerializeField] public EventReference PochePose { get; private set; }
    
    [field: Header ("FootSteps1")]
    [field: SerializeField] public EventReference FootSteps1 { get; private set; }
    
    [field: Header ("FootSteps2")]
    [field: SerializeField] public EventReference FootSteps2 { get; private set; }
    
    [field: Header ("FootSteps3")]
    [field: SerializeField] public EventReference FootSteps3 { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
            Destroy(this);
        }
        else
            instance = this;
    }
}

