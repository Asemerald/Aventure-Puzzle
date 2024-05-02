using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsInteraction : InteractibleTemplate
{
    public enum PlantState { Grown, NotGrown, Dead, Burned, Growing}
    [Header("Plants Settings")]
    public PlantState state;
    
    private Collider collider;
    private MeshRenderer meshRenderer;

    private void Start()
    {
        collider = GetComponent<Collider>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public override void BaseState()
    {
        if(state == PlantState.Growing)
            state = PlantState.Grown;
    }

    public override void UpsideState()
    {
        if (state == PlantState.Burned || state == PlantState.NotGrown) return;

        state = PlantState.Dead;
        collider.enabled = false;
        meshRenderer.enabled = true;
        
    }

    public override void UpsideDownState()
    {
        if (state != PlantState.Growing)
        {
            collider.enabled = true;
            meshRenderer.enabled = true;
        }

        state = PlantState.Growing;
    }

    public void BurnPlant()
    {
        if (state == PlantState.Grown)
        {
            state = PlantState.Burned;
            collider.enabled = false;
            meshRenderer.enabled = false;
        }
    }
}
