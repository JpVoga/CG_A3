using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput: MonoBehaviour {
    private InputActions actions;

    public event Action OnGoRight, OnGoLeft;

    private void Awake() {
        actions = new InputActions();
    }

    private void Start() {
        actions.Enable();
        actions.Default.Enable();

        actions.Default.GoRight.performed += context => OnGoRight?.Invoke();
        actions.Default.GoLeft.performed  += context => OnGoLeft?.Invoke();
    }

    private void OnDestroy() {
        actions.Enable();
        actions.Default.Enable();
        actions.Dispose();
    }

    public InputAction GetRotateCameraAction() {return actions.Default.RotateCamera;}
}