using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    /* None -> Aucun état
     * Moveable -> l'objet à une collision et peut être déplacer
     * UnMoveable -> l'objet à une collision et ne peut pas être déplacer
     * NoCollider -> l'objet n'a pas de collision et ne peut pas être déplacer
     * EmitEnergy -> l'objet est dans l'état Moveable et émet de l'energie dans un rayon autour de lui
     * EnergyUnMoveable -> l'objet émet de l'energie dans un rayon autour de lui, à une collision et ne peut pas être déplacer
     * EnergyNoCollider -> l'objet émet de l'energie dans un rayon autour de lui, n'a pas de collision et ne peut pas être déplacer
     * Size -> l'objet change de taille/de forme et à une collision
     * EnergySize -> l'objet change de taille/de forme et à une collision et émet de l'energie dans un rayon autour de lui
     * Portal -> l'objet se transforme en portail
     */

    public enum ObjectState { None, Moveable, UnMoveable, NoCollider, EmitEnergy, EnergyUnMoveable, EnergyNoCollider, Size, EnergySize, Portal, NPC }

    [Header("Global Settings")]
    public ObjectState worldState;
    public ObjectState astralState;
    public bool sizeIsModify;

    public bool inAstralState;
    public bool isMoveable;
    public bool isPortal;

    [Header("Energy Emition")]
    public bool emitEnergy;
    public float energyRadius;
    public LayerMask energyDoor;
    public GameObject energySphere;

    [Header("Size/Mesh Mods")]
    public GameObject astraldObj;
    MeshRenderer astralRenderer;

    [Header("Materials States")]
    public Material moveableMat;
    public Material unMoveableMat, noColliderMat, emitEnergyMat, energyUnMoveableMat, energyNoColliderMat, sizeMat, energySizeMat, portalMat, npcMat, npcEnergyMat;

    List<EnergyDoor> doorsList = new List<EnergyDoor>();
    MeshRenderer mesh;
    [HideInInspector]
    public Collider col;

    [HideInInspector]
    public bool isGrabed;

    [Header("GrabSettings")]
    public float heightToAdd;
    public float macDistanceToHigher;
    public float distanceToCheckForGround = 0.1f;
    public float timeToResetPos;
    public LayerMask colToHigher;

    [HideInInspector]
    public Vector3 localPosInit;
    Vector3 newLocalPos;
    [HideInInspector]
    public Vector3 placePos;
    
    bool higheringObject = false;

    [HideInInspector]
    public Rigidbody _rb;
    bool resetVel = false;
    float timeToResetVel = .5f;

    [Header("Mesh")]
    [SerializeField] GameObject moveableMesh;
    [SerializeField] GameObject unMoveableMesh;
    [SerializeField] GameObject energyUnMoveableMesh;
    [SerializeField] GameObject energyMoveableMesh;
    [SerializeField] GameObject sizeMesh;

    [SerializeField] GameObject groundMesh;

    Transform parent;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        _rb = GetComponent<Rigidbody>();

        if(astraldObj != null)
            astralRenderer = astraldObj.GetComponent<MeshRenderer>();

        parent = transform.parent;

        ResetMesh();
        SwitchMode(false);

        if (astralState == ObjectState.Portal)
            isPortal = true;

        if(worldState == ObjectState.NPC && energySphere != null)
            energySphere.transform.localScale = Vector3.one * .1f * (energyRadius * 2);
        else if(energySphere != null)
            energySphere.transform.localScale = Vector3.one * .1f * energyRadius;
    }

    #region Switch
    public void SwitchMode()
    {
        ResetMesh();

        if (inAstralState)
        {
            if (sizeIsModify)
                ResetSize();

            if (isPortal)
                ResetPortal();

            switch (astralState)
            {
                case ObjectState.None:
                    Debug.Log(gameObject.name + " : " + "No State : Astral");
                    break;
                case ObjectState.Moveable:
                    MoveableState();
                    break;
                case ObjectState.UnMoveable:
                    UnMoveableState();
                    break;
                case ObjectState.NoCollider:
                    NoColldierState();
                    break;
                case ObjectState.EmitEnergy:
                    EnergyMoveable();
                    break;
                case ObjectState.EnergyUnMoveable:
                    EnergyUnMoveable();
                    break;
                case ObjectState.EnergyNoCollider:
                    EnergyNoCollider();
                    break;
                case ObjectState.Size:
                    Size();
                    break;
                case ObjectState.EnergySize:
                    EnergySize();
                    break;
                case ObjectState.Portal:
                    PortalSwitch();
                    break;
                case ObjectState.NPC:
                    SwitchToNPC();
                    break;
            }
        }
        else
        {
            if (sizeIsModify)
                ResetSize();

            if (isPortal)
                ResetPortal();

            switch (worldState)
            {
                case ObjectState.None:
                    Debug.Log(gameObject.name + " : " + "No State : World");
                    break;
                case ObjectState.Moveable:
                    MoveableState();
                    break;
                case ObjectState.UnMoveable:
                    UnMoveableState();
                    break;
                case ObjectState.NoCollider:
                    NoColldierState();
                    break;
                case ObjectState.EmitEnergy:
                    EnergyMoveable();
                    break;
                case ObjectState.EnergyUnMoveable:
                    EnergyUnMoveable();
                    break;
                case ObjectState.EnergyNoCollider:
                    EnergyNoCollider();
                    break;
                case ObjectState.Size:
                    Size();
                    break;
                case ObjectState.EnergySize:
                    EnergySize();
                    break;
                case ObjectState.Portal:
                    PortalSwitch();
                    break;
                case ObjectState.NPC:
                    SwitchToNPC();
                    break;
            }
        }

    }

    public void SwitchMode(bool astral)
    {
        ResetMesh();

        if (astral)
        {
            inAstralState = true;

            if (sizeIsModify)
                ResetSize();

            if (isPortal)
                ResetPortal();

            switch (astralState)
            {
                case ObjectState.None: Debug.Log(gameObject.name + " : " + "No State : Astral");
                    break;
                case ObjectState.Moveable: MoveableState();
                    break;
                case ObjectState.UnMoveable: UnMoveableState();
                    break;
                case ObjectState.NoCollider: NoColldierState();
                    break;
                case ObjectState.EmitEnergy: EnergyMoveable();
                    break;
                case ObjectState.EnergyUnMoveable: EnergyUnMoveable();
                    break;
                case ObjectState.EnergyNoCollider: EnergyNoCollider();
                    break;
                case ObjectState.Size: Size();
                    break;
                case ObjectState.EnergySize: EnergySize();
                    break;
                case ObjectState.Portal: PortalSwitch();
                    break;
                case ObjectState.NPC: SwitchToNPC();
                    break;
            }
        }
        else
        {
            inAstralState = false;

            if (sizeIsModify)
                ResetSize();

            if (isPortal)
                ResetPortal();

            switch (worldState)
            {
                case ObjectState.None: Debug.Log(gameObject.name + " : " + "No State : World");
                    break;
                case ObjectState.Moveable: MoveableState();
                    break;
                case ObjectState.UnMoveable: UnMoveableState();
                    break;
                case ObjectState.NoCollider: NoColldierState();
                    break;
                case ObjectState.EmitEnergy: EnergyMoveable();
                    break;
                case ObjectState.EnergyUnMoveable: EnergyUnMoveable();
                    break;
                case ObjectState.EnergyNoCollider: EnergyNoCollider();
                    break;
                case ObjectState.Size: Size();
                    break;
                case ObjectState.EnergySize: EnergySize();
                    break;
                case ObjectState.Portal: PortalSwitch();
                    break;
                case ObjectState.NPC: SwitchToNPC();
                    break;
            } 
        }
    }

    #endregion

    void ResetMesh()
    {
        if(astraldObj != null)
        {
            if (!astraldObj.activeSelf)
            {
                mesh.enabled = true;
                astralRenderer.enabled = false;
            }
            else if(astraldObj.activeSelf)
            {
                mesh.enabled = false;
                astralRenderer.enabled = true;
            }
        }

        if (moveableMesh != null)
            moveableMesh.SetActive(false);
        if(unMoveableMesh != null)
            unMoveableMesh.SetActive(false);
        if(energyUnMoveableMesh != null)
            energyUnMoveableMesh.SetActive(false);
        if(energyMoveableMesh != null)
            energyMoveableMesh.SetActive(false);
        if(sizeMesh != null)
            sizeMesh.SetActive(false);
    }

    private void Update()
    {
        if (emitEnergy)
            EmitEnergy();
        else if(!emitEnergy && doorsList.Count > 0)
            doorsList.Clear();

        if(energySphere != null)
        {
            if(emitEnergy && !energySphere.activeSelf)
                energySphere.SetActive(true);
            else if(!emitEnergy && energySphere.activeSelf)
                energySphere.SetActive(false);
        }

        if(isMoveable)
            GrabCheck();

        if(_rb != null)
            ReduceVelocity();

        if(groundMesh != null)
            if (!isGrabed && groundMesh.activeInHierarchy)
                groundMesh.SetActive(false);
    }

    public void GrabCheck()
    {
        if (!isGrabed && higheringObject)
        {
            StopCoroutine(HigherObject());
            higheringObject = false;
            transform.position = placePos;
        }

        if (isGrabed && !groundMesh.activeInHierarchy)
            groundMesh.SetActive(true);

        if (isGrabed && HittingGround())
        {
            if (!higheringObject)
                StartCoroutine(HigherObject());
        }
    }

    public void ReduceVelocity()
    {
        if (isGrabed)
        {
            StopCoroutine(SetVelocity());
            resetVel = false;
            return;
        }
        if(Mathf.Abs(_rb.velocity.x) > 0 || Mathf.Abs(_rb.velocity.z) > 0 && !resetVel)
        {
            StartCoroutine(SetVelocity());
        }
    }

    #region Ienumerator
    IEnumerator SetVelocity()
    {
        resetVel = true;

        float elapsedTime = 0;
        while (elapsedTime < timeToResetVel)
        {
            if (_rb == null)
                break;

            elapsedTime += Time.deltaTime;

            Vector3 newVel = Vector3.Lerp(_rb.velocity, new Vector3(0,_rb.velocity.y, 0), elapsedTime / .5f);
            _rb.velocity = newVel;
            yield return null;
        }

        resetVel = false;
    }

    IEnumerator HigherObject()
    {
        higheringObject = true;
        float elapsedTime = 0;
            newLocalPos = localPosInit + new Vector3(0, heightToAdd / 2, 0);

        while (elapsedTime < .5f)
        {
            if (!isGrabed) break;
            elapsedTime += Time.deltaTime;

            if(HittingGround())
                newLocalPos += new Vector3(0, heightToAdd / 2, 0);

            if(newLocalPos.y > macDistanceToHigher)
                newLocalPos.y = macDistanceToHigher;

            Vector3 newPos = Vector3.Lerp(localPosInit, newLocalPos, elapsedTime / .5f);
            transform.localPosition  = newPos;
            yield return null;
        }

        if (!isGrabed) yield break;

        yield return new WaitForSeconds(timeToResetPos);

        if (!isGrabed) yield break;

        elapsedTime = 0;
        while (elapsedTime < .5f)
        {
            if (!isGrabed) break;

            elapsedTime += Time.deltaTime;

            Vector3 newPos = Vector3.Lerp(newLocalPos, localPosInit, elapsedTime / .5f);
            transform.localPosition = newPos;
            yield return null;
        }

        higheringObject = false;
    }
#endregion

    #region States

    void EmitEnergy()
    {
        //Debug.Log(gameObject.name + " : " + "Emitting energy : " + (inAstralState ? "Astral" : "World"));
        if (CollideDoor())
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, energyRadius, energyDoor);
            if (colliders.Length > 0)
            {
                if (colliders[0].TryGetComponent(out EnergyDoor door))
                {
                    if (!doorsList.Contains(door))
                    {
                        doorsList.Add(door);
                        door.CheckForEnergy(this);
                    }
                }
            }

            if (doorsList.Count > 1)
            {
                foreach (var col in doorsList)
                    if (!colliders.Contains(col.GetComponent<Collider>()))
                    {
                        col.RemoveEnergy(this);
                        doorsList.Remove(col);
                        if (doorsList.Count == 0) break;
                    }
            }
            else if (doorsList.Count == 1)
            {
                if (!colliders.Contains(doorsList[0].GetComponent<Collider>()))
                {
                    doorsList[0].RemoveEnergy(this);
                    doorsList.RemoveAt(0);
                }
            }
        }
    }

    void NoColldierState()
    {
        //Debug.Log(gameObject.name + " : " + "No Collider State : " + (inAstralState ? "Astral" : "World"));

        if (TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;

        gameObject.layer = LayerMask.NameToLayer("InteractibleNoCollision");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleNoCollision");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = noColliderMat;
        astralRenderer.material = noColliderMat;
    }

    void UnMoveableState()
    {
        //Debug.Log(gameObject.name + " : " + "Unmoveable State : " + (inAstralState ? "Astral" : "World"));

        if (TryGetComponent(out Rigidbody rb))
            rb.isKinematic = true;

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        astraldObj.layer = LayerMask.NameToLayer("Interactible");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = unMoveableMat;
        astralRenderer.material = unMoveableMat;

        if (unMoveableMesh != null)
        {
            mesh.enabled = false;
            unMoveableMesh.SetActive(true);
        }
        else
            Debug.Log("UnMoveable Mesh is missing");
    }

    void MoveableState()
    {
        //Debug.Log(gameObject.name + " : " + "Moveable State : " + (inAstralState ? "Astral" : "World"));

        if (TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        gameObject.layer = LayerMask.NameToLayer("InteractibleMoveable");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleMoveable");
        isMoveable = true;
        emitEnergy = false;

        mesh.material = moveableMat;
        astralRenderer.material = moveableMat;

        if(moveableMesh != null)
        {
            mesh.enabled = false;
            astralRenderer.enabled = false;
            moveableMesh.SetActive(true);
        }
        else
            Debug.Log("Moveable Mesh is missing");
    }

    void EnergyMoveable()
    {
        MoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = emitEnergyMat;
        astralRenderer.material = emitEnergyMat;

        ResetMesh();
        if(energyMoveableMesh != null)
        {
            mesh.enabled = false;
            astralRenderer.enabled = false;
            energyMoveableMesh.SetActive(true);
        }
        else
            Debug.Log("EnergyMoveable Mesh is missing");
    }

    void EnergyUnMoveable()
    {
        UnMoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyUnMoveableMat;
        astralRenderer.material = energyUnMoveableMat;

        ResetMesh();
        if (energyUnMoveableMesh != null)
        {
            mesh.enabled = false;
            astralRenderer.enabled = false;
            energyUnMoveableMesh.SetActive(true);            
        }
        else
            Debug.Log("EnergyUnMoveable Mesh is missing");
    }

    void EnergyNoCollider()
    {
        NoColldierState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyNoColliderMat;
        astralRenderer.material = energyNoColliderMat;

        ResetMesh();
    }

    void Size()
    {
        if (inAstralState)
        {
            astraldObj.SetActive(true);
            col.enabled = false;
        }
        else
        {
            astraldObj.SetActive(false);
            col.enabled = true;
        }

        UnMoveableState();
        if (TryGetComponent(out Rigidbody rb))
            rb.isKinematic = false;

        astralRenderer.material = sizeMat;
        mesh.material = sizeMat;

        sizeIsModify = true;

        ResetMesh();
        if(sizeMesh != null)
        {
            mesh.enabled = false;
            astralRenderer.enabled = false;
            sizeMesh.SetActive(true);
        }
        else
            Debug.Log("Size Mesh is missing");

    }

    void EnergySize()
    {
        Size();
        emitEnergy = true;

        astralRenderer.material = energySizeMat;
        mesh.material = energySizeMat;
    }

    void ResetSize()
    {
        if (inAstralState)
        {
            astraldObj.SetActive(true);
            col.enabled = false;
        }
        else
        {
            astraldObj.SetActive(false);
            col.enabled = true;
        }

        sizeIsModify = false;
        ResetMesh();
    }

    void SwitchToNPC()
    {
        TryGetComponent(out NPC_Controller _npc);
        if (inAstralState)
        {
            _npc.SwitchAstralState(true);
            emitEnergy = true;
            mesh.material = npcEnergyMat;
        }
        else
        {
            _npc.SwitchAstralState(false);
            emitEnergy = false;
            mesh.material = npcMat;
        }
    }

    void PortalSwitch()
    {
        UnMoveableState();

        TryGetComponent(out Portal p);
        p.isActive = true;

        _rb.isKinematic = true;

        astraldObj.SetActive(true);
        col.enabled = false;

        gameObject.layer = LayerMask.NameToLayer("Portal");
        astraldObj.layer = LayerMask.NameToLayer("Portal");

        astralRenderer.material = portalMat;

        ResetMesh();
    }

    void ResetPortal()
    {
        MoveableState();
        TryGetComponent(out Portal p);
        p.isActive = false;
        
        astraldObj.SetActive(false);
        col.enabled = true;

        _rb.isKinematic = false;


        if (TryGetComponent(out Rigidbody rb))
            return;
        else
        {
            Rigidbody _rb = gameObject.AddComponent<Rigidbody>();
            _rb.mass = 100;
            _rb.freezeRotation = true;
        }

        ResetMesh();

    }

    #endregion

    public void AttachToParent()
    {
        transform.parent = parent;
    }

    bool HittingGround()
    {
        if(!isMoveable) return false;

        if (astraldObj.activeInHierarchy)
        {
            astraldObj.TryGetComponent(out Collider collider);

            if (Physics.Raycast(collider.bounds.center + new Vector3(collider.bounds.extents.x, -collider.bounds.extents.y, collider.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(collider.bounds.center + new Vector3(-collider.bounds.extents.x, -collider.bounds.extents.y, collider.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(collider.bounds.center + new Vector3(-collider.bounds.extents.x, -collider.bounds.extents.y, -collider.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(collider.bounds.center + new Vector3(collider.bounds.extents.x, -collider.bounds.extents.y, -collider.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else
                return false;
        }
        else
        {
            if (Physics.Raycast(col.bounds.center + new Vector3(col.bounds.extents.x, -col.bounds.extents.y, col.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(col.bounds.center + new Vector3(-col.bounds.extents.x, -col.bounds.extents.y, col.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(col.bounds.center + new Vector3(-col.bounds.extents.x, -col.bounds.extents.y, -col.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else if (Physics.Raycast(col.bounds.center + new Vector3(col.bounds.extents.x, -col.bounds.extents.y, -col.bounds.extents.z), Vector3.down, distanceToCheckForGround, colToHigher))
                return true;
            else
                return false;
        }
    }

    bool CollideDoor()
    {
        if(!emitEnergy) return false;
        return Physics.OverlapSphere(transform.position, energyRadius, energyDoor).Length > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if(TryGetComponent(out Rigidbody rb))
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
                rb.freezeRotation = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (TryGetComponent(out Rigidbody rb))
            {
                rb.constraints = RigidbodyConstraints.None;
                rb.freezeRotation = true;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if(emitEnergy)
            Gizmos.DrawWireSphere(transform.position, energyRadius);
    }
}
