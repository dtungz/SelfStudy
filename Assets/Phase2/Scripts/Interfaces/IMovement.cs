using UnityEngine;

public interface IMovement
{
    public MoveType MoveType { get; }
    public void Move(Rigidbody direction, Vector3 input);
    
}
