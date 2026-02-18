using UnityEngine;

public class WalkMovement :MonoBehaviour, IMovement
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform orientation;
    
    private Vector3 _direction;

    public MoveType MoveType => MoveType.Walk;

    public void Move(Rigidbody Rb, Vector3 input)
    {
        CalculateDirection(input);
        Vector3 velocity = Rb.linearVelocity;
        Vector3 moveDirection = _direction.normalized * moveSpeed;
        Rb.linearVelocity = new Vector3(moveDirection.x, velocity.y, moveDirection.z);
    }

    private void CalculateDirection(Vector3 input)
    {
        _direction = input.x * orientation.right + orientation.forward * input.z;
    }
}
