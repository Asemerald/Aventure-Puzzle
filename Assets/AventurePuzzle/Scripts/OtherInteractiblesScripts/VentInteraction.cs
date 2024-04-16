using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VentInteraction : MonoBehaviour
{

    [Header("Vent Settings")] 
    [SerializeField] public List<VentInteraction> vents = new List<VentInteraction>();
    public GameObject fireMesh;
    [SerializeField] Vector3 fireboxBurningSize;
    [SerializeField] LayerMask burningLayers;
    [SerializeField] Transform fireOutPos;
    public enum TorchState { FireIn, FireOff, FireOut }
    public TorchState state;


    public void Update()
    {
       
        //get the distance between this vent and all vents
        foreach (var vent in vents)
        {
            
            float distance = Vector3.Distance(vent.transform.position, transform.position);
            
            //the coroutine will have a delay depending of the distance, from 1 to 3 seconds 
            
            float delay = distance / 5;
            
            
            
            
            /*if (distance < 5)
            {
                //Launch Burning coroutine
                vent.StartCoroutine(BurningCoroutine(1));
            }
            else if (distance < 10)
            {
                //Launch Burning coroutine
                StartCoroutine(BurningCoroutine(2));
            }
            else if (distance < 15)
            {
                //Launch Burning coroutine
                StartCoroutine(BurningCoroutine(3));
            }*/
            
            
            
            
            
            
        }
        
        //Launch Burning coroutine
        //StartCoroutine(BurningCoroutine(5));
        
        
    }

    IEnumerator BurningCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        state = TorchState.FireIn;
        fireMesh.transform.localScale = fireboxBurningSize;
        fireMesh.transform.localPosition = fireOutPos.localPosition + new Vector3(0, fireMesh.transform.localScale.y / 2, 0);
        fireMesh.SetActive(true);
        
    }
    
    
    
    

    private void OnDrawGizmos()
    {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(fireOutPos.localPosition + new Vector3(0, fireboxBurningSize.y / 2, 0), fireboxBurningSize);
    }
}
