using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    [Header("Interactibles Required Energy")]
    public Interactible[] interactiblesRequired;
    [HideInInspector] public List<Interactible> interactiblePowering;

    public bool isOpen;

    private void Update()
    {
        if(interactiblePowering.Count == interactiblesRequired.Length && !isOpen)
        {
            Debug.Log("Energy Door : Door is powered");
            isOpen = true;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoorNoCollision");
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if(interactiblePowering.Count != interactiblesRequired.Length && isOpen)
        {
            Debug.Log("Energy Door : Door is no longer powered");
            isOpen = false;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoor");
            GetComponent<MeshRenderer>().enabled = true;
        }

        if (interactiblePowering.Count > 0)
            CheckIfInteractibleStillOn();
    }

    void CheckIfInteractibleStillOn()
    {
        for(int i = interactiblePowering.Count -1; i >= 0; i--) //Can cause some error :/
        {
            if (!interactiblePowering[i].emitEnergy) interactiblePowering.RemoveAt(i);
            if (interactiblePowering.Count == 0) break;
        }
    }

    public void RemoveEnergy(Interactible energy)
    {
        interactiblePowering.Remove(energy);
    }

    public void CheckForEnergy(Interactible energy)
    {
        if (!interactiblePowering.Contains(energy))
        {
            interactiblePowering.Add(energy);
        }
    }
}
