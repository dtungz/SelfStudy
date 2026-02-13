using UnityEngine;

public class ChangeColorBaloWhenEnemyAttack : MonoBehaviour
{
    private Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    public void ChangeColor()
    {
        rend.material.color = Random.ColorHSV();
    }
}
