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
    public InputAction AbilityAction => _playerInput.Player.ChangeAbility;
    public InputAction ActiveAction => _playerInput.Player.Active;
    
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
