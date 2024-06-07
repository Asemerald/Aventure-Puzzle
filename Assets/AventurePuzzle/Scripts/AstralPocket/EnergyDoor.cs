using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyDoor : MonoBehaviour
{
    [Header("Interactibles Required Energy")]
    public Interactible[] interactiblesRequired;
    public List<Interactible> interactiblePowering;

    [Header("Lock")]
    [SerializeField] GameObject lockMesh;
    List<GameObject> lockObjects = new();

    public bool isOpen;

    int previousCount = 0;

    [SerializeField] Animator doors;

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
        if (interactiblePowering.Count >= interactiblesRequired.Length && !isOpen)
        {
            Debug.Log("Energy Door : Door is powered");
            isOpen = true;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoorNoCollision");
            PlayAnimation(0);
        }
        else if (interactiblePowering.Count < interactiblesRequired.Length && isOpen)
        {
            Debug.Log("Energy Door : Door is no longer powered");
            isOpen = false;
            gameObject.layer = LayerMask.NameToLayer("EnergyDoor");
            PlayAnimation(1);
        }

        CheckIfInteractibleStillOn();

        if (interactiblePowering.Count != previousCount)
        {

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

    }

    void PlayAnimation(int i)
    {
        switch (i)
        {
            case 0:
                doors.Play("Open");
                break;
            case 1:
                doors.Play("Close");
                break;
        }
    }

    void CheckIfInteractibleStillOn()
    {
        for(int i = interactiblePowering.Count -1; i >= 0; i--) //Can cause some error :/
        {
            if (!interactiblePowering[i].emitEnergy) interactiblePowering.RemoveAt(i);
            if (interactiblePowering.Count == 0) break;
        }

        previousCount = interactiblePowering.Count;
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
