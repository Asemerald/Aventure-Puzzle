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

    [SerializeField] private EventReference testSound;
    
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
        if (currentPocketNum >= maxPocketAvailable)
            return;

        currentPocketNum++;
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

        AudioManager.instance.PlayOneShot(testSound, this.transform.position);
        sphereCasted.Add(Instantiate(astralPocketMesh, newPocketCastPos, Quaternion.identity, transform.parent));
        
        //astralPocketMesh.SetActive(true);

        //StartCoroutine(DisplayAstralPocket());
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

        //astralPocketMesh.SetActive(false);
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