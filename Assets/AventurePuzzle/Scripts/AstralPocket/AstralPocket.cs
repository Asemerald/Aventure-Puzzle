using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralPocket : MonoBehaviour
{
    public static AstralPocket Instance {  get; private set; }

    public float sphereRadius = 5f; // Adjust the radius as needed

    //create a taskbar menu that call the function

    Vector3 previousPocketCastPos;
    Vector3 newPocketCastPos;

    bool astralPocketCasted;

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
        Collider[] colliders = Physics.OverlapSphere(newPocketCastPos, sphereRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Interactible>())
            {
                collider.GetComponent<Interactible>().SwitchMode(true); // Switch state of interactible to astral state
                // Add the object to the player's inventory (you need to implement this part)
                // interactible.AddItemToInventory();

                // Destroy the object in the scene -- Pourquoi ?
                //Destroy(collider.gameObject);
            }
        }
        astralPocketCasted = true;
        previousPocketCastPos = newPocketCastPos;
    }

    void DecastAstralPocket()
    {
        Debug.Log("Astral Pocket : Decasted");
        Collider[] colliders = Physics.OverlapSphere(previousPocketCastPos, sphereRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.GetComponent<Interactible>())
            {
                collider.GetComponent<Interactible>().SwitchMode(false); // Switch state of interactible to world state
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}