using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private Transform CameraPosition;
    private void Update()
    {
        transform.position = CameraPosition.position;
    }
}
