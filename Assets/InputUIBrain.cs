using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputsUIBrain : MonoBehaviour
{
    public static InputsUIBrain Instance { get; private set; }

    InputsController inputs;
    [HideInInspector] public InputAction click, back;

    private void Awake()
    {
        inputs = new InputsController();

        if (Instance == null)
            Instance = this;
    }

    private void OnEnable()
    {
       click = inputs.UI.Click;
       back = inputs.UI.Back;

        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }
}

