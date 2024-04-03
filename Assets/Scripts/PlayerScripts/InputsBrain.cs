using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsBrain : MonoBehaviour
{
    public static InputsBrain Instance { get; private set; }

    InputsController inputs;
    [HideInInspector]
    public InputAction move, tarot, interract, pause;

    private void Awake()
    {
        inputs = new InputsController();

        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
        move = inputs.Player.Move;
        tarot = inputs.Player.Tarot;
        interract = inputs.Player.Interact;
        pause = inputs.Player.Pause;

        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}
