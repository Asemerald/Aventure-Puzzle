using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightMenu : MonoBehaviour
{

    private Light light;
    private bool FadeIn = false;
    
    //make a list of gameobjects
    public List<GameObject> objectsToActivate;
    
    
    async void Start()
    {
        light = GetComponent<Light>();
        await WaitFor3Seconds();
        FadeIn = true;
        SetFlameActive();
    }

    private void SetFlameActive()
    {
        //loop through the list of gameobjects and set them active
        foreach (GameObject obj in objectsToActivate)
        {
            if (!obj.activeSelf)
            {
                obj.SetActive(true);
            }
        }
    }
    
    private Task WaitFor3Seconds()
    {
        return Task.Delay(5000);
    }

    private void Update()
    {
        if (FadeIn)
        {
            light.intensity += Time.deltaTime * 10;
            if (light.intensity >= 17)
            {
                FadeIn = false;
                GetComponent<LightFlickering>().enabled = true;
            }
        }
    }
}
