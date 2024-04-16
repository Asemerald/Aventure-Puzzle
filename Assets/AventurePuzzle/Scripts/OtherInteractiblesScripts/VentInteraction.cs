using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInteraction : MonoBehaviour
{

    [Header("Vent Settings")] 
    [SerializeField] public List<VentInteraction> vents = new List<VentInteraction>();
    public GameObject fireMesh;
    [SerializeField] Vector3 fireboxLitSize;
    [SerializeField] Vector3 fireboxBurningSize;
    [SerializeField] LayerMask burningLayers;
    [SerializeField] Transform fireOutPos;
    public enum TorchState { Burning, Extinguished }
    public TorchState state;


    public void Burning()
    {
        state = TorchState.Burning;
        fireMesh.transform.localScale = fireboxBurningSize;
        fireMesh.transform.localPosition = fireOutPos.localPosition + new Vector3(0, fireMesh.transform.localScale.y / 2, 0);
        fireMesh.SetActive(true);
    }
    
    
    
    

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(fireOutPos.localPosition + new Vector3(0, fireboxLitSize.y / 2, 0), fireboxLitSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(fireOutPos.localPosition + new Vector3(0, fireboxBurningSize.y / 2, 0), fireboxBurningSize);
    }
}
