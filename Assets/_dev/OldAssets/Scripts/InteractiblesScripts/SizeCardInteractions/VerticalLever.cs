using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalLever : MonoBehaviour
{

    [Header("Settings")]
    public Transform castPos;
    public Vector3 castSize;

    public bool open = false;

    private void Update()
    {

        Collider[] hits = Physics.OverlapBox(castPos.position, castSize, transform.rotation);
        if (hits.Length <= 0) return;

        foreach (Collider collider in hits)
        {
            if (collider.GetComponent<SizeInteraction>())
                switch (collider.GetComponent<SizeInteraction>().currentState)
                {
                    case SizeInteraction.SizeState.Shrink: 
                        open = false;
                        break;
                    case SizeInteraction.SizeState.Normal: 
                        open = false;
                        break;
                    case SizeInteraction.SizeState.Bigger:
                        open = true;
                        Debug.Log("IsOpen");
                        return;

                }
        }

    }

    

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(castPos.localPosition, castSize);
    }

}
