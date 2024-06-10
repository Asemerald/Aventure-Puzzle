using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class LightMenu : MonoBehaviour
{

    private Light light;
    private bool FadeIn = false;
    
    
    async void Start()
    {
        light = GetComponent<Light>();
        await WaitFor3Seconds();
        FadeIn = true;
    }
    
    private Task WaitFor3Seconds()
    {
        return Task.Delay(3000);
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
