using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralPocket : MonoBehaviour
{
    public float sphereRadius = 5f; // Adjust the radius as needed
    
    //create a taskbar menu that call the function
    
    

    public void CastAstralPocket()
    {
        Debug.Log("Astral Pocket Casted");

        // Cast a sphere around the player
        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius);

        foreach (Collider collider in colliders)
        {
            // Check if the object has the Interactible Script
            Interactible interactible = collider.GetComponent<Interactible>();
            if (interactible != null)
            {
                // Add the object to the player's inventory (you need to implement this part)
                // interactible.AddItemToInventory();

                // Destroy the object in the scene
                Destroy(collider.gameObject);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }
}