using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EmissionFlickering : MonoBehaviour
{

    [SerializeField] private Color _defaultIntensity;
    private Color newColor;
    private int seedX = 0;
    private int seedY = 0;

    public Vector2 noiseRangeMultiplicator = new Vector2(1,1);
    private Vector2 noiseSample = new Vector2(0, 0);
    public float noiseSpeed;
    private float noiseValue;

    private Material matRef;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.GetComponent<Renderer>().material != null)
        {
            matRef = gameObject.GetComponent<Renderer>().material;
            _defaultIntensity = matRef.GetColor("_EmissionColor");
        }
        seedX = Random.Range(-100000, 100000);
        seedY = Random.Range(-100000, 100000);
}

    // Update is called once per frame
    void Update()
    {
        noiseValue = Map(Mathf.PerlinNoise(noiseSample.x + seedX, noiseSample.y + seedY), 0, 1, noiseRangeMultiplicator.x, noiseRangeMultiplicator.y);
        newColor = new Color(_defaultIntensity.r * noiseValue, _defaultIntensity.g * noiseValue, _defaultIntensity.b * noiseValue);
        matRef.SetColor("_EmissionColor", newColor);
        noiseSample.x += noiseSpeed * Time.deltaTime;
    }
    
    float Map(float s, float a1, float a2, float b1, float b2)
    {
        return b1 + (s-a1)*(b2-b1)/(a2-a1);
    }
}
