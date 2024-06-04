using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{
    [field: Header ("Poche SFX")]
    [field: SerializeField] public EventReference PochePose { get; private set; }
    
    [field: Header ("FootStepsRock")]
    [field: SerializeField] public EventReference FootStepsRock { get; private set; }
    
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

