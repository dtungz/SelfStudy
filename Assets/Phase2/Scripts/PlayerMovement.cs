using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("REFERENCE")]
    [SerializeField] private Rigidbody playerRigidbody;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private List<MonoBehaviour> movementComponents;

    [Header("CONFIG")] 
    [SerializeField] private MoveType moveType;
    
    private Vector3 _moveDirection;
    private List<IMovement> _movementsType = new List<IMovement>();
    private IMovement _currentMovement;

    private void Awake()
    {
        for (int i = 0; i < movementComponents.Count; i++)
        {
            if (movementComponents[i] is IMovement movement)
            {
                _movementsType.Add(movement);
            }
        }
    }

    private void Start()
    {
        ChangeMoveState();
    }

    private void Update()
    {
        SetMoveDirection();
        ChangeGravity();
    }

    private void FixedUpdate()
    {
        if (_currentMovement == null)
            return;
        _currentMovement.Move(playerRigidbody, _moveDirection);
    }

    // ----- HEALPER -----


    private void SetMoveDirection()
    {
        Vector2 direction = inputManager.MoveAction.ReadValue<Vector2>();
        _moveDirection = new Vector3(direction.x, 0, direction.y);
    }
    
    [ContextMenu("Move")]
    private void ChangeMoveState()
    {
        for (int i = 0; i < _movementsType.Count; i++)
        {
            if(_movementsType[i].MoveType == moveType)
            {
                _currentMovement = _movementsType[i];
                return;
            }
        }
    }

    private void ChangeGravity()
    {
        playerRigidbody.useGravity = moveType != MoveType.Fly;
    }
}
