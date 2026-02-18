using UnityEngine;

public class FlyMovement :MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed;
    //[SerializeField] private float verticalSpeed;
    [SerializeField] private Transform orientation;
    //[SerializeField] private InputManager inputManager;
    
    private Vector3 _direction;

    public MoveType MoveType => MoveType.Fly;

    public void Move(Rigidbody Rb, Vector3 input)
    {
        CalculateDirection(input);
            Rb.linearVelocity = _direction *  moveSpeed;
    }

    private void CalculateDirection(Vector3 input)
    {
        _direction = input.x * orientation.right + orientation.forward * input.z;
    }
}
