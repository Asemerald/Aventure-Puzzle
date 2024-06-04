using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioFunctions : MonoBehaviour
{
    public static AudioFunctions Instance {  get; private set; }

    public void PlayFootSteps1()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FootSteps1, this.transform.position);
    }
    
    public void PlayFootSteps2()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FootSteps2, this.transform.position);
    }
    
    public void PlayFootSteps3()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FootSteps3, this.transform.position);
    }
}
