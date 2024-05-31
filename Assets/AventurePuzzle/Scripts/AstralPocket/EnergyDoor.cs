using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    [Header("Interactibles Required Energy")]
    public Interactible[] interactiblesRequired;
    [HideInInspector] public List<Interactible> interactiblePowering;

    [Header("Lock")]
    [SerializeField] GameObject lockMesh;
    List<GameObject> lockObjects = new();

    public bool isOpen;

    private void Start()
    {
        foreach(var d in interactiblesRequired)
        {
            GameObject go = Instantiate(lockMesh, transform.position, transform.rotation, transform);
            lockObjects.Add(go);
        }

        if(lockObjects.Count > 0)
        {
            int sum = lockObjects.Count;

            if (sum > 1)
            {
                lockObjects[0].transform.localPosition = new Vector3(lockObjects[0].transform.localPosition.x - .25f, lockObjects[0].transform.localPosition.y, lockObjects[0].transform.localPosition.z - .55f);
                lockObjects[1].transform.localPosition = new Vector3(lockObjects[1].transform.localPosition.x + .25f, lockObjects[1].transform.localPosition.y, lockObjects[1].transform.localPosition.z - .55f);
            }
            else
                lockObjects[0].transform.localPosition = new Vector3(0, 0,-.55f);
        }
    }

    private void Update()
    {
        if(interactiblePowering.Count >= interactiblesRequired.Length && !isOpen)
        {
            Debug.Log("Energy Door : Door is powered");
            isOpen = true;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoorNoCollision");
            GetComponent<MeshRenderer>().enabled = false;
        }
        else if(interactiblePowering.Count < interactiblesRequired.Length && isOpen)
        {
            Debug.Log("Energy Door : Door is no longer powered");
            isOpen = false;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoor");
            GetComponent<MeshRenderer>().enabled = true;
        }

        if (interactiblePowering.Count > 0)
            CheckIfInteractibleStillOn();

        for(int i = 0; i < interactiblesRequired.Length; i++)
        {
            lockObjects[i].SetActive(true);
        }
        for(int i = 0; i < interactiblePowering.Count; i++)
        {
            if (i >= lockObjects.Count) continue;
            lockObjects[i].SetActive(false);
        }

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
