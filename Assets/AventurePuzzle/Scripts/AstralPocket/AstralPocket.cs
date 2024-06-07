using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering;

public class AstralPocket : MonoBehaviour
{
    public static AstralPocket Instance {  get; private set; }
    
    [Header("Settings")]
    public float sphereRadius = 5f; // Adjust the radius as needed
    public LayerMask interactibleMask;
    public float timeToReset = 2;
    public int maxPocketAvailable = 3;

    int currentPocketNum;

    [HideInInspector] public bool ShowAstralPocket = false;

    Vector3 newPocketCastPos;

    public GameObject astralPocketMesh;

    List<GameObject> sphereCasted = new List<GameObject>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void CastAstralPocket()
    {
        if (currentPocketNum >= maxPocketAvailable)
            return;

        currentPocketNum++;
        Debug.Log("Astral Pocket : Casted");

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

        AudioManager.instance.PlayOneShot(FMODEvents.instance.PochePose, this.transform.position);
        sphereCasted.Add(Instantiate(astralPocketMesh, newPocketCastPos, Quaternion.identity, transform.parent));
    }

    public void DecastAstralPocket()
    {
        Debug.Log("Astral Pocket : Decasted");

        Interactible[] obj = FindObjectsOfType<Interactible>();

        foreach(var o in obj)
        { o.SwitchMode(false); }

        foreach (var o in sphereCasted)
            Destroy(o.gameObject);

        sphereCasted.Clear();

        currentPocketNum = 0;
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