using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralPocket : MonoBehaviour
{
    public static AstralPocket Instance {  get; private set; }

    [Header("Settings")]
    public float sphereRadius = 5f; // Adjust the radius as needed
    public LayerMask interactibleMask;
    public float timeToReset = 2;


    [HideInInspector] public bool ShowAstralPocket = false;

    //create a taskbar menu that call the function

    Vector3 previousPocketCastPos;
    Vector3 newPocketCastPos;

    bool astralPocketCasted;

    public GameObject astralPocketMesh;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CastAstralPocket()
    {
        Debug.Log("Astral Pocket : Casted");
        
        if(astralPocketCasted)
            DecastAstralPocket();

        // Cast a sphere around the player
        newPocketCastPos = transform.position;

        Collider[] colliders = Physics.OverlapSphere(newPocketCastPos, sphereRadius, interactibleMask);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Interactible interactible))
            {
                interactible.SwitchMode(true); // Switch state of interactible to astral state
            }
            else if (collider.TryGetComponent(out InteractibleMesh mesh))
            {
                mesh.parent.SwitchMode(true);
            }
        }
        astralPocketCasted = true;
        previousPocketCastPos = newPocketCastPos;

        astralPocketMesh.SetActive(true);
        astralPocketMesh.transform.position = newPocketCastPos;
    }

    public void DecastAstralPocket()
    {
        Debug.Log("Astral Pocket : Decasted");
        Collider[] colliders = Physics.OverlapSphere(previousPocketCastPos, sphereRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Interactible interactible))
            {
                interactible.SwitchMode(false); // Switch state of interactible to world state
            }
            else if (collider.TryGetComponent(out InteractibleMesh mesh))
            {
                mesh.parent.SwitchMode(false);
            }
        }

        astralPocketMesh.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        if (!ShowAstralPocket) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(newPocketCastPos, sphereRadius);
    }
    
    private void OnDrawGizmosSelected()
    {
        //if (!ShowAstralPocket) return;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}