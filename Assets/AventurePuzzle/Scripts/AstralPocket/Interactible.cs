using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactible : MonoBehaviour
{
    /* None -> Aucun �tat
     * Moveable -> l'objet � une collision et peut �tre d�placer
     * UnMoveable -> l'objet � une collision et ne peut pas �tre d�placer
     * NoCollider -> l'objet n'a pas de collision et ne peut pas �tre d�placer
     * EmitEnergy -> l'objet est dans l'�tat Moveable et �met de l'energie dans un rayon autour de lui
     * EnergyUnMoveable -> l'objet �met de l'energie dans un rayon autour de lui, � une collision et ne peut pas �tre d�placer
     * EnergyNoCollider -> l'objet �met de l'energie dans un rayon autour de lui, n'a pas de collision et ne peut pas �tre d�placer
     * Size -> l'objet change de taille/de forme et � une collision
     * EnergySize -> l'objet change de taille/de forme et � une collision et �met de l'energie dans un rayon autour de lui
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
            switch (astralState)
            {
                case ObjectState.None: Debug.Log("No State : Astral");
                    break;
                case ObjectState.Moveable:
                    break;
                case ObjectState.UnMoveable:
                    break;
                case ObjectState.NoCollider:
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
            switch (worldState)
            {
                case ObjectState.None: Debug.Log("No State : World");
                    break;
                case ObjectState.Moveable:

                    break;
                case ObjectState.UnMoveable:

                    break;
                case ObjectState.NoCollider:

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


    Collider objectCol;

    private void Start()
    {
        objectCol = GetComponent<Collider>();
    }

    private void Update()
    {
        if (emitEnergy)
        {

        }
    }
}
