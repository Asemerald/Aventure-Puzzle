using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    List<Collider> currentObjects = new List<Collider>();
    List<Collider> previousObjects = new List<Collider>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    #region Test
    /*public void CastPocket()
    {
        newPocketCastPos = transform.position;

        astralPocketCasted = true;
        previousPocketCastPos = newPocketCastPos;

        astralPocketMesh.SetActive(true);
        astralPocketMesh.transform.position = newPocketCastPos;
    }

    public void DeCastPocket()
    {
        astralPocketMesh.SetActive(false);

        astralPocketCasted = false;
    }

    private void Update()
    {
        
        CheckForInteractibles();

    }

    void CheckForInteractibles()
    {
        if (!astralPocketCasted)
        {
            if(previousObjects.Count > 0)
            {
                foreach (Collider coll in previousObjects)
                    DecastAstralPocket(coll.gameObject);
                currentObjects.Clear();
                previousObjects.Clear();
            }
            return;
        }

        if (previousObjects.Count == GetObjects().Count) return;

        foreach (var obj in previousObjects)
            DecastAstralPocket(obj.gameObject);


        currentObjects.Clear();

        foreach (var o in GetObjects())
        {
            currentObjects.Add(o);
            CastAstralPocket(o.gameObject);
        }

        previousObjects.Clear();
        previousObjects.AddRange(currentObjects);
    }

    void CastAstralPocket(GameObject obj)
    {
        if (obj.TryGetComponent(out Interactible interactible))
        {
            interactible.SwitchMode(true); // Switch state of interactible to astral state
        }
        else if (obj.TryGetComponent(out InteractibleMesh mesh))
        {
            mesh.parent.SwitchMode(true);
        }

        astralPocketCasted = true;
        previousPocketCastPos = newPocketCastPos;

        astralPocketMesh.SetActive(true);
        astralPocketMesh.transform.position = newPocketCastPos;
    }

    void DecastAstralPocket(GameObject obj)
    {
        if (obj.TryGetComponent(out Interactible interactible))
        {
            interactible.SwitchMode(false); // Switch state of interactible to world state
        }
        else if (obj.TryGetComponent(out InteractibleMesh mesh))
        {
            mesh.parent.SwitchMode(false);
        }
    }*/

    /*List<Collider> GetObjects()
    {
        if (astralPocketCasted)
            return Physics.OverlapSphere(newPocketCastPos, sphereRadius, interactibleMask).ToList();
        else
            return null;
    }*/
    #endregion

    public void CastAstralPocket()
    {
        Debug.Log("Astral Pocket : Casted");
        
        /*if(astralPocketCasted)
            DecastAstralPocket();*/

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
        astralPocketMesh.transform.position = newPocketCastPos;
        astralPocketMesh.SetActive(true);

        //StartCoroutine(DisplayAstralPocket());
    }

    public void DecastAstralPocket()
    {
        Debug.Log("Astral Pocket : Decasted");

        Interactible[] obj = FindObjectsOfType<Interactible>();

        foreach(var o in obj)
        { o.SwitchMode(false); }

        /*Collider[] colliders = Physics.OverlapSphere(previousPocketCastPos, sphereRadius);
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
        }*/

        astralPocketMesh.SetActive(false);
    }

    /*IEnumerator DisplayAstralPocket()
    {
        astralPocketMesh.SetActive(true);
        yield return new WaitForSeconds(.25f);
        astralPocketMesh.SetActive(false);
    }*/

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