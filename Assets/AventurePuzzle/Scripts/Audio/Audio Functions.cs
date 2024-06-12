using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioFunctions : MonoBehaviour
{
    public static AudioFunctions Instance {  get; private set; }

    public void PlayFootStepsRock()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FootStepsRock, this.transform.position);
    }

    public void PlayLandingSFX()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.Landing, this.transform.position);
    }

    public void PlayPNJFootSteps()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.FootStepsRock, this.transform.position);
    }
}
