using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfinerSwitcher : MonoBehaviour
{

    private GameObject _CameraFollowPlayer;
    
    private void Start()
    {
        _CameraFollowPlayer = GameObject.FindGameObjectWithTag("");
    }
}
