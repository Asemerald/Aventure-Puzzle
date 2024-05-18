using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoorRelay : MonoBehaviour
{
    [Header("Relay Required Energy")]
    public DoorRelays[] interactiblesRequired;

    public bool isOpen;

    private void Update()
    {
        if (isOpen) return;

        int sum = 0;
        foreach(var i in interactiblesRequired)
            if(i.isPowered) sum++;
        

        if(sum == interactiblesRequired.Length)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
        }
    }

}
