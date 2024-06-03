using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBrain : MonoBehaviour
{
    public static InputsBrain Instance { get; private set; }

    InputsController inputs;
    [HideInInspector]
    public InputAction move, pocket, interact, pause, rotateGrab;

    private void Awake()
    {
        inputs = new InputsController();

        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        move = inputs.Player.Move;
        pocket = inputs.Player.Pocket;
        interact = inputs.Player.Interact;
        pause = inputs.Player.Pause;
        rotateGrab = inputs.Player.RotateGrab;

        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
