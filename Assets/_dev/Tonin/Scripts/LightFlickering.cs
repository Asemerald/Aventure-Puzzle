using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LightFlickering : MonoBehaviour
{

    private float _defaultIntensity;
    private int seedX = 0;
    private int seedY = 0;

    public Vector2 noiseRange = new Vector2(0,0);
    private Vector2 noiseSample = new Vector2(0, 0);
    public float noiseSpeed;

    private Light lightRef;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Light>() != null)
        {
            lightRef = gameObject.GetComponent<Light>();
            _defaultIntensity = lightRef.intensity;
        }
        seedX = Random.Range(-100000, 100000);
        seedY = Random.Range(-100000, 100000);
}

    // Update is called once per frame
    void Update()
    {
        
        lightRef.intensity = _defaultIntensity + Map(Mathf.PerlinNoise(noiseSample.x+seedX, noiseSample.y+seedY), 0, 1, noiseRange.x, noiseRange.y);
        noiseSample.x += noiseSpeed * Time.deltaTime;
    }
    
    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
}
