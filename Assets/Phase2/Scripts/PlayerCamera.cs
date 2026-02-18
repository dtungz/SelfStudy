using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [Header("REFERENCES")]
    [SerializeField] private InputManager inputManager;
    [SerializeField] private Transform orientation;

    [Header("SENTIVITYS")] 
    [SerializeField] private float xSentivity = 360f;
    [SerializeField] private float ySentivity = 360f;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        SetInputs();
        RotateCamera();
    }
    

    private void SetInputs()
    {
        Vector2 rotation = inputManager.LookAction.ReadValue<Vector2>();
        _yRotation += rotation.x * xSentivity * Time.deltaTime;
        _xRotation -= rotation.y * ySentivity * Time.deltaTime;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);
    }

    private void RotateCamera()
    {
        transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, _yRotation, 0);
    }
}
