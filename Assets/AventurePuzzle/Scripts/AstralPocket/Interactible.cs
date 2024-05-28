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

    [Header("Size/Mesh Mods")]
    public GameObject astraldObj;

    [Header("Materials States")]
    public Material moveableMat;
    public Material unMoveableMat, noColliderMat, emitEnergyMat, energyUnMoveableMat, energyNoColliderMat, sizeMat, energySizeMat, portalMat, npcMat, npcEnergyMat;

    List<EnergyDoor> doorsList = new List<EnergyDoor>();
    MeshRenderer mesh;
    Collider col;


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        SwitchMode(false);

        if (astralState == ObjectState.Portal)
            isPortal = true;
    }

    public void SwitchMode(bool astral)
    {
        if(astral)
        {
            inAstralState = true;

            if (sizeIsModify)
                ResetSize();

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

            if (isPortal) PortalSwitch();
        }
        else
        {
            inAstralState = false;

            if (sizeIsModify)
                ResetSize();

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

    private void Update()
    {
        if (emitEnergy)
            EmitEnergy();
        else if(!emitEnergy)
            doorsList.Clear();
    }

    void EmitEnergy()
    {
        //Debug.Log(gameObject.name + " : " + "Emitting energy : " + (inAstralState ? "Astral" : "World"));
        Collider[] colliders = Physics.OverlapSphere(transform.position, energyRadius, energyDoor);
        if (colliders.Length > 0)
        {
            if (colliders[0].GetComponent<EnergyDoor>())
            {
                if (!doorsList.Contains(colliders[0].GetComponent<EnergyDoor>()))
                {
                    doorsList.Add(colliders[0].GetComponent<EnergyDoor>());
                    colliders[0].GetComponent<EnergyDoor>().CheckForEnergy(this);
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

    void NoColldierState()
    {
        //Debug.Log(gameObject.name + " : " + "No Collider State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleNoCollision");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleNoCollision");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = noColliderMat;
        astraldObj.GetComponent<MeshRenderer>().material = noColliderMat;
    }

    void UnMoveableState()
    {
        //Debug.Log(gameObject.name + " : " + "Unmoveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        astraldObj.layer = LayerMask.NameToLayer("Interactible");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = unMoveableMat;
        astraldObj.GetComponent<MeshRenderer>().material = unMoveableMat;
    }

    void MoveableState()
    {
        //Debug.Log(gameObject.name + " : " + "Moveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleMoveable");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleMoveable");
        isMoveable = true;
        emitEnergy = false;

        mesh.material = moveableMat;
        astraldObj.GetComponent<MeshRenderer>().material = moveableMat;
    }

    void EnergyMoveable()
    {
        MoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = emitEnergyMat;
        astraldObj.GetComponent<MeshRenderer>().material = emitEnergyMat;
    }

    void EnergyUnMoveable()
    {
        UnMoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyUnMoveableMat;
        astraldObj.GetComponent<MeshRenderer>().material = energyUnMoveableMat;
    }

    void EnergyNoCollider()
    {
        NoColldierState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyNoColliderMat;
        astraldObj.GetComponent<MeshRenderer>().material = energyNoColliderMat;
    }

    void Size()
    {
        if (inAstralState)
        {
            astraldObj.SetActive(true);
            mesh.enabled = false;
            col.enabled = false;
        }
        else
        {
            astraldObj.SetActive(false);
            mesh.enabled = true;
            col.enabled = true;
        }

        UnMoveableState();

        if (inAstralState) //ça veut dire que c'est la version astrale qui prend l'état de taille et jaune
            astraldObj.GetComponent<MeshRenderer>().material = sizeMat;
        else //ça veut dire que c'est la version normal qui est à l'état de taille et donc jaune
            mesh.material = sizeMat;

        sizeIsModify = true;
    }

    void EnergySize()
    {
        Size();
        emitEnergy = true;

        if (inAstralState) //ça veut dire que c'est la version astrale qui prend l'état de taille et jaune
            astraldObj.GetComponent<MeshRenderer>().material = energySizeMat;
        else //ça veut dire que c'est la version normal qui est à l'état de taille et donc jaune
            mesh.material = energySizeMat;
    }

    void ResetSize()
    {
        if (inAstralState)
        {
            astraldObj.SetActive(true);
            mesh.enabled = false;
            col.enabled = false;
        }
        else
        {
            astraldObj.SetActive(false);
            mesh.enabled = true;
            col.enabled = true;
        }

        sizeIsModify = false;
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
        TryGetComponent(out Portal p);

        if (inAstralState)
        {
            UnMoveableState();

            TryGetComponent(out Rigidbody rb);
            Destroy(rb);

            astraldObj.SetActive(true);
            mesh.enabled = false;
            col.enabled = false;

            astraldObj.GetComponent<MeshRenderer>().material = portalMat;
            p.isActive = true;
        }
        else
        {
            MoveableState();
            astraldObj.SetActive(false);
            mesh.enabled = true;
            col.enabled = true;

            p.isActive = false;

            if (TryGetComponent(out Rigidbody rb))
                return;
            else
            {
                Rigidbody _rb = gameObject.AddComponent<Rigidbody>();
                _rb.mass = 100;
                _rb.freezeRotation = true;
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
