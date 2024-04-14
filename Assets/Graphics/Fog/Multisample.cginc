
#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

#include "Assets/Graphics/Fog/SimplexNoise3D.hlsl"

//#include "Assets/Graphics/Fog/Offset along view direction.shadersubgraph"


int sampleAmount = 0;
float3 position;


void Multisample_float(float3 WorldPos, float SampleDensity, float CubeDepth, float3 ViewDir, float MaxSamples, float3 NoiseScale, float3 NoiseOffset, out float Value)
{
    Value = 0.0f;
    position = WorldPos;
    /*
     // Sample with fixed density
    for (float i = 0; i < CubeDepth; i += SampleDensity)
    {
        sampleAmount++;
        Value += max(0, snoise(position* NoiseScale + NoiseOffset)) * SampleDensity;
        position += ViewDir * SampleDensity;
        if (sampleAmount > MaxSamples) {
            break;
        }
    }
    */

    // Sample with fixed amount of samples
    int targetSampleAmount = 20;
    float stepSize = CubeDepth / targetSampleAmount;
    for (int i = 0; i < targetSampleAmount; i ++)
    {
        Value += max(0, snoise(position * NoiseScale + NoiseOffset)) * stepSize;
        position += ViewDir * stepSize;
        if (sampleAmount > MaxSamples) {
            break;
        }
    }
#endif

}