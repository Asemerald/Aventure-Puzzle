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

    public enum ObjectState { None, Moveable, UnMoveable, NoCollider, EmitEnergy, EnergyUnMoveable, EnergyNoCollider, Size, EnergySize, Portal }

    public ObjectState worldState;
    public ObjectState astralState;
    public bool sizeIsModify;

    public bool inAstralState;
    
    public bool isMoveable;

    [Header("Energy Emition")]
    public bool emitEnergy;
    public float energyRadius;
    public LayerMask energyDoor;

    [Header("Size/Mesh Mods")]
    public GameObject astraldObj;

    List<EnergyDoor> doorsList = new List<EnergyDoor>();
    List<DoorRelays> doorsPointsList = new List<DoorRelays>();

    [Header("Materials States")]
    public Material moveableMat;
    public Material unMoveableMat, noColliderMat, emitEnergyMat, energyUnMoveableMat, energyNoColliderMat, sizeMat, energySizeMat, portalMat;

    MeshRenderer mesh;
    Collider col;

    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        col = GetComponent<Collider>();
        SwitchMode(false);
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
                case ObjectState.Portal:
                    break;

            }
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
                case ObjectState.Portal:
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
        Debug.Log(gameObject.name + " : " + "Emitting energy : " + (inAstralState ? "Astral" : "World"));
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
            else if (colliders[0].GetComponent<DoorRelays>())
            {
                if (!doorsPointsList.Contains(colliders[0].GetComponent<DoorRelays>()))
                {
                    doorsPointsList.Add(colliders[0].GetComponent<DoorRelays>());
                    colliders[0].GetComponent<DoorRelays>().CheckForEnergy(this);
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

        if (doorsPointsList.Count > 1)
        {
            foreach (var col in doorsPointsList)
                if (!colliders.Contains(col.GetComponent<Collider>()))
                {
                    col.RemoveEnergy(this);
                    doorsPointsList.Remove(col);
                    if (doorsPointsList.Count == 0) break;
                }
        }
        else if (doorsPointsList.Count == 1)
        {
            if (!colliders.Contains(doorsPointsList[0].GetComponent<Collider>()))
            {
                doorsPointsList[0].RemoveEnergy(this);
                doorsPointsList.RemoveAt(0);
            }
        }
    }

    void NoColldierState()
    {
        Debug.Log(gameObject.name + " : " + "No Collider State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleNoCollision");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleNoCollision");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = noColliderMat;
    }

    void UnMoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Unmoveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        astraldObj.layer = LayerMask.NameToLayer("Interactible");

        isMoveable = false;
        emitEnergy = false;

        mesh.material = unMoveableMat;
    }

    void MoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Moveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleMoveable");
        astraldObj.layer = LayerMask.NameToLayer("InteractibleMoveable");
        isMoveable = true;
        emitEnergy = false;

        mesh.material = moveableMat;
    }

    void EnergyMoveable()
    {
        MoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = emitEnergyMat;
    }

    void EnergyUnMoveable()
    {
        UnMoveableState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyUnMoveableMat;
    }

    void EnergyNoCollider()
    {
        NoColldierState();
        EmitEnergy();
        emitEnergy = true;

        mesh.material = energyNoColliderMat;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        if(emitEnergy)
            Gizmos.DrawWireSphere(transform.position, energyRadius);
    }
}
