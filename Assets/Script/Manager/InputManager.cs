using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;

    public float MoveDirection{get; private set; }

    private void OnEnable()
    {
        moveAction.action.Enable();

        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;
    }
    private void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        moveAction.action.Disable();
    }
    
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<float>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MoveDirection = 0f;
    }
}
