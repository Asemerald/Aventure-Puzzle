using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [Header("Link Portal Settings")]
    public Portal linkedPortal;
    public Transform spawnPoint;

    public bool isActive;

    [Header("Trigger Settings")]
    [SerializeField] LayerMask playerMask;
    [SerializeField] Vector3 portalSize;
    Interactible interactable;

    private void Start()
    {
        TryGetComponent(out Interactible i);
        interactable = i;
    }

    private void Update()
    {
        if (PlayerInPortal())
        {
            //Tp le joueur
            if (linkedPortal.isActive && isActive)
            {
                PlayerController.Instance.transform.position = linkedPortal.spawnPoint.position;
            }
        }
    }

    bool PlayerInPortal()
    {
        Collider[] hitted = Physics.OverlapBox(interactable.astraldObj.transform.position, portalSize, transform.rotation, playerMask);
        if(hitted.Length > 0)
        {
            return true;
        }
        else
            return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Gizmos.color = Color.red;
        if(interactable != null)
            Gizmos.DrawWireCube(interactable.astraldObj.transform.localPosition, portalSize);
    }
}