using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using JetBrains.Annotations;
using UnityEngine;
using FMODUnity; 
using FMOD.Studio;
using Unity.VisualScripting;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;

    private EventInstance ambianceEventInstance;
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

            eventInstances = new List<EventInstance>();
        }

        public void PlayOneShot(EventReference sound, Vector3 worldPos)
        {
            RuntimeManager.PlayOneShot(sound, worldPos);
        }

        public EventInstance CreateEventInstance(EventReference eventReference)
        {
            EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstances.Add(eventInstance);
            return eventInstance;
        }

        private void CleanUp()
        {
            foreach (EventInstance eventInstance in eventInstances)
            {
                eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                eventInstance.release();

            }
        }

        private void Start()
        {
            InitializeAmbiance(FMODEvents.instance.WindBlow);
        }

        private void InitializeAmbiance(EventReference ambienceEventReference)
        {
            ambianceEventInstance = CreateEventInstance(ambienceEventReference);
            ambianceEventInstance.start();
        }
        
        private void OnDestroy()
        {
            CleanUp();
        }
}
