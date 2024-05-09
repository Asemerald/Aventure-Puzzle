using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    [Header("Interactibles Required Energy")]
    public Interactible[] interactiblesRequired;
    public List<Interactible> interactiblePowering;

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
            Debug.Log("Energy Door : interactible is emitting ? " + interactiblePowering[i].emitEnergy);
            if (!interactiblePowering[i].emitEnergy) interactiblePowering.RemoveAt(i);
            if (interactiblePowering.Count == 0) break;
        }
    }

    public void RemoveEnergy(Interactible energy)
    {
        Debug.Log("Energy Door : Remove Power");
        interactiblePowering.Remove(energy);
    }

    public void CheckForEnergy(Interactible energy)
    {
        Debug.Log("Energy Door : Check called");
        if (!interactiblePowering.Contains(energy))
        {
            Debug.Log("Energy Door : energy added");
            interactiblePowering.Add(energy);
        }
    }
}
