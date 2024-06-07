using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerCam : MonoBehaviour
{
    public Transform player;

    void LateUpdate()
    {
        if(Application.isPlaying)
            if (PlayerController.Instance.enteringAPortal) return;
                transform.position = player.position;
    }

    private void Update()
    {
        if(!Application.isPlaying)
            transform.position = player.position;
    }
}