using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Fire_Torch : MonoBehaviour
{
    [Tooltip("Reference to the parent GO's torch script")]
    public TorchInteraction torchInteractionScript;
    [Tooltip("Reference to the colliding cube marking the end of the beam")]
    public Transform endCube;
    private TorchInteraction.TorchState lastState;
    // Start is called before the first frame update
    void Start()
    {
        lastState = torchInteractionScript.state;
        if (endCube != null && endCube.GetComponent<MeshRenderer>() != null)
        { endCube.GetComponent<MeshRenderer>().enabled = false; }
    }

    // Update is called once per frame
    void Update()
    {
        endCube.transform.localPosition = new Vector3(0, 0, torchInteractionScript.fireBox.transform.localScale.y);
        if (lastState != torchInteractionScript.state)
        {
            if (torchInteractionScript.state == TorchInteraction.TorchState.Extinguished || torchInteractionScript.state == TorchInteraction.TorchState.Ember)
            {
                gameObject.GetComponent<ParticleSystem>().Stop(true);
                //gameObject.GetComponent<ParticleSystem>().Clear();
                
            }
            else
            {
                gameObject.GetComponent<ParticleSystem>().Play(true);
            }
            lastState = torchInteractionScript.state;
            Debug.Log(lastState.ToString());
        }
        
        
    }
}
