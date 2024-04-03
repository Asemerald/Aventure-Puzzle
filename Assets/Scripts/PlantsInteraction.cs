using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantsInteraction : InteractibleTemplate
{
    public enum PlantState { Grown, NotGrown, Dead, Burned, Growing}
    [Header("Plants Settings")]
    public PlantState state;

    public override void BaseState()
    {
        if(state == PlantState.Growing)
            state = PlantState.Grown;
    }

    public override void UpsideState()
    {
        if (state == PlantState.Burned || state == PlantState.NotGrown) return;

        state = PlantState.Dead;
        GetComponent<Collider>().enabled = false;
        GetComponent<MeshRenderer>().enabled = true;
        
    }

    public override void UpsideDownState()
    {
        if (state != PlantState.Growing)
        {
            GetComponent<Collider>().enabled = true;
            GetComponent<MeshRenderer>().enabled = true;
        }

        state = PlantState.Growing;
    }

    public void BurnPlant()
    {
        if (state == PlantState.Grown)
        {
            state = PlantState.Burned;
            GetComponent<Collider>().enabled = false;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }
}
