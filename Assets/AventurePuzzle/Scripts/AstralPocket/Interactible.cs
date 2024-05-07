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

    public bool inAstralState;
    
    public bool isMoveable;

    [Header("Energy Emition")]
    public bool emitEnergy;
    public float energyRadius;
    public LayerMask energyDoor;

    [Header("Size/Mesh Mods")]
    public GameObject worldObj;
    public GameObject astraldObj;

    List<EnergyDoor> doorsList = new List<EnergyDoor>();

    private void Start()
    {
        SwitchMode(false);
    }

    public void SwitchMode(bool astral)
    {
        if(astral)
        {
            inAstralState = true;
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
                case ObjectState.Size:
                    break;
                case ObjectState.EnergySize:
                    break;
                case ObjectState.Portal:
                    break;

            }
        }
        else
        {
            inAstralState = false;
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
                case ObjectState.Size:
                    break;
                case ObjectState.EnergySize:
                    break;
                case ObjectState.Portal:
                    break;
            } 
        }
    }

    private void Update()
    {
        if (emitEnergy)
        {
            Debug.Log(gameObject.name + " : " + "Emitting energy : " + (inAstralState ? "Astral" : "World"));
            Collider[] colliders = Physics.OverlapSphere(transform.position, energyRadius, energyDoor);
            if(colliders.Length > 0)
            {
                if (!doorsList.Contains(colliders[0].GetComponent<EnergyDoor>()))
                {
                    doorsList.Add(colliders[0].GetComponent<EnergyDoor>());
                    colliders[0].GetComponent<EnergyDoor>().CheckForEnergy(this);
                }
            }

            if(doorsList.Count > 1)
            {
                foreach (var col in doorsList)
                    if (!colliders.Contains(col.GetComponent<Collider>()))
                    {
                        col.RemoveEnergy(this);
                        doorsList.Remove(col);
                        if(doorsList.Count == 0) break;
                    }
            }
            else if(doorsList.Count == 1) 
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
        Debug.Log(gameObject.name + " : " + "No Collider State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleNoCollision");
        isMoveable = false;
        emitEnergy = false;
    }

    void UnMoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Unmoveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        isMoveable = false;
        emitEnergy = false;
    }

    void MoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Moveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleMoveable");
        isMoveable = true;
        emitEnergy = false;
    }

    void EnergyMoveable()
    {
        MoveableState();
        emitEnergy = true;
    }

    void EnergyUnMoveable()
    {
        UnMoveableState();
        emitEnergy = true;
    }

    void EnergyNoCollider()
    {
        NoColldierState();
        emitEnergy = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, energyRadius);
    }
}
