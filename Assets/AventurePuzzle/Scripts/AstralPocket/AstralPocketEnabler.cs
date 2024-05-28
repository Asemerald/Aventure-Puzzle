using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AstralPocketEnabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.hasAstralPocket = true;
            StartCoroutine(HUD.Instance.Tutorial());
        }
    }
}
