using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using FMODUnity; 

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set;}
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

        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }
    }
