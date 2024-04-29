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
    [SerializeField] private TorchInteraction.TorchState lastState;
    public ParticleSystem ember;
    public ParticleSystem litUp;
    public ParticleSystem burning;
    // Start is called before the first frame update
    void Start()
    {
        lastState = torchInteractionScript.state;
        if (endCube != null && endCube.GetComponent<MeshRenderer>() != null)
        { endCube.GetComponent<MeshRenderer>().enabled = false; }
        torchInteractionScript.fireBox.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (endCube != null && endCube.GetComponent<MeshRenderer>() != null)
        { endCube.transform.localPosition = new Vector3(0, 0, torchInteractionScript.fireBox.transform.localScale.y); }
        if (lastState != torchInteractionScript.state)
        {
            switch (torchInteractionScript.state)
            {
                case TorchInteraction.TorchState.Extinguished:
                    ember.Stop(true);
                    litUp.Stop(true);
                    burning.Stop(true);
                    break;
                case TorchInteraction.TorchState.Ember:
                    ember.Play(true);
                    litUp.Stop(true);
                    burning.Stop(true);
                    break;
                case TorchInteraction.TorchState.LitUp:
                    ember.Stop(true);
                    litUp.Play(true);
                    burning.Stop(true);
                    break;
                case TorchInteraction.TorchState.Burning:
                    ember.Stop(true);
                    litUp.Stop(true);
                    burning.Play(true);
                    break;
                default:
                    ember.Stop(true);
                    litUp.Stop(true);
                    burning.Stop(true);
                    break;
            }
            lastState = torchInteractionScript.state;
        }
        Debug.Log(torchInteractionScript.state.ToString());
        return;
    }
}
