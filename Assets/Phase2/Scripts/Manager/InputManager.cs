using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    
    private PlayerInputSystem _playerInput;
    public InputAction LookAction => _playerInput.Player.Look;
    public InputAction MoveAction => _playerInput.Player.Move;
    public InputAction JumpAction => _playerInput.Player.Jump;
    public InputAction RunAction => _playerInput.Player.Run;
    private void Awake()
    {
        _playerInput = new PlayerInputSystem();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }
}
