using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TorchInteraction : InteractibleTemplate
{
    [Header("Torch Settings")]
    public GameObject fireMesh;
    [SerializeField] Vector3 fireboxLitSize;
    [SerializeField] Vector3 fireboxBurningSize;
    [SerializeField] LayerMask burningLayers;
    [SerializeField] Transform fireOutPos;
    public enum TorchState { Extinguished, LitUp, Burning, Ember }
    public TorchState state;

    public override void BaseState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.LitUp;
        fireMesh.transform.localScale = fireboxLitSize;
        fireMesh.transform.localPosition = fireOutPos.localPosition + new Vector3(0, fireMesh.transform.localScale.y / 2, 0);
        fireMesh.SetActive(true);
    }

    public override void UpsideState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.Burning;
        fireMesh.transform.localScale = fireboxBurningSize;
        fireMesh.transform.localPosition = fireOutPos.localPosition + new Vector3(0, fireMesh.transform.localScale.y / 2, 0);
        fireMesh.SetActive(true);
    }

    public override void UpsideDownState()
    {
        if (state == TorchState.Extinguished) return;
        state = TorchState.Ember;
        fireMesh.SetActive(false);
    }

    private void Update()
    {
        if (state != TorchState.Burning) return;

        if (CollidindSomething())
        {
            Debug.Log("Torch Interaction : Debugging burn area");
            Collider[] hits = Physics.OverlapBox(fireOutPos.position + new Vector3(0, fireboxLitSize.y / 2, 0), fireboxBurningSize, transform.rotation, burningLayers);
            foreach(Collider collider in hits)
            {
                //Code pour faire bruler les trucs
                if (collider.GetComponent<PlantsInteraction>())
                {
                    collider.GetComponent<PlantsInteraction>().BurnPlant();
                }

                //Code pour allumer une torche
                if (collider.GetComponent<TorchInteraction>())
                {
                    LitUpTorch(collider.GetComponent<TorchInteraction>());
                }
            }
        }
    }

    void LitUpTorch(TorchInteraction t)
    {
        if (t.state != TorchState.Extinguished) return;
        t.state = TorchState.LitUp;
        t.UpsideState();
    }

    public void SetOnFire()
    {
        switch (currentCardState)
        {
            case CardTemplate.CardState.None:
                state = TorchState.LitUp;
                break;
            case CardTemplate.CardState.Endroit:
                state = TorchState.Burning;
                break;
            case CardTemplate.CardState.Envers:
                state = TorchState.Ember;
                break;
        }

        fireMesh.SetActive(true);
    }

    bool CollidindSomething()
    {
        return Physics.OverlapBox(fireOutPos.position + new Vector3(0, fireboxLitSize.y / 2, 0), fireboxBurningSize, transform.rotation, burningLayers).Length > 0 ;
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
