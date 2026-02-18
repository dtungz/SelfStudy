using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // ----- FIELD -----
    [Header("REFERENCES")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Survivor survivor;

    private void Update()
    {
        survivor.MoveHorizontal(inputManager.MoveDirection);
    }
}
