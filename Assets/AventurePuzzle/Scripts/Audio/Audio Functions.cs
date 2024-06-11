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
}
