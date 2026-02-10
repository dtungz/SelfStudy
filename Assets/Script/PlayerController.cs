using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----- FIELD -----
    [Header("REFERENCES")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Solider solider;

    private void Update()
    {
        solider.MoveHorizontal(inputManager.MoveDirection);
    }
}
