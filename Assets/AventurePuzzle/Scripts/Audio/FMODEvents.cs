using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class FMODEvents : MonoBehaviour
{

    [field: Header ("BackGrounds Sounds")]
    [field: SerializeField] public EventReference WindBlow { get; private set; }
    
    [field: Header ("Music")]
    [field: SerializeField] public EventReference Music1 { get; private set; }
    
    
    
    [field: Header ("Poche SFX")]
    [field: SerializeField] public EventReference PochePose { get; private set; }
    
    [field: Header ("FootStepsRock")]
    [field: SerializeField] public EventReference FootStepsRock { get; private set; }
    
    [field: Header ("Landing")]
    [field: SerializeField] public EventReference Landing { get; private set; }
    
    [field: Header ("BlockPickUp")]
    [field: SerializeField] public EventReference PickUpBlock { get; private set; }
    
    [field: Header ("BlockUnPickUp")]
    [field: SerializeField] public EventReference UnPickUpBlock { get; private set; }
    
    [field: Header ("PortalSound")]
    [field: SerializeField] public EventReference PortalSFX { get; private set; }
    
     [field: Header ("CardReceive")]
        [field: SerializeField] public EventReference CardReceive { get; private set; }
    
     [field: Header ("CardDeplace")]
        [field: SerializeField] public EventReference CardDeplace { get; private set; }
     
     [field: Header ("Crash")]
     [field: SerializeField] public EventReference Crash { get; private set; }
     
     [field: Header ("ChuteMeteore")]
     [field: SerializeField] public EventReference ChuteMeteore { get; private set; }


    
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

