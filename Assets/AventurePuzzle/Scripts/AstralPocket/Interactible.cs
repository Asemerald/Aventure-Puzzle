using System.Collections;
using System.Collections.Generic;
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
    public bool emitEnergy;

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
                case ObjectState.EmitEnergy:
                    break;
                case ObjectState.EnergyUnMoveable:
                    break;
                case ObjectState.EnergyNoCollider:
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
                case ObjectState.EmitEnergy:
                    break;
                case ObjectState.EnergyUnMoveable:
                    break;
                case ObjectState.EnergyNoCollider:
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

        }
    }

    void NoColldierState()
    {
        Debug.Log(gameObject.name + " : " + "No Collider State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("InteractibleNoCollision");
        isMoveable = false;
    }

    void UnMoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Unmoveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        isMoveable = false;
    }

    void MoveableState()
    {
        Debug.Log(gameObject.name + " : " + "Moveable State : " + (inAstralState ? "Astral" : "World"));

        gameObject.layer = LayerMask.NameToLayer("Interactible");
        isMoveable = true;
    }

    
}
