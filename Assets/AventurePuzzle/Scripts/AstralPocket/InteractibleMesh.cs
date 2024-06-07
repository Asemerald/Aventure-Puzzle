using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleMesh : MonoBehaviour
{
    public Interactible parent;

    private void Start()
    {
        parent = GetComponentInParent<Interactible>();
    }

    private void Update()
    {
        if (parent._rb != null)
            parent.ReduceVelocity();

        if (parent.isMoveable)
            parent.GrabCheck();
    }
}
