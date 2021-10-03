using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(DeathTrigger))]
public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform headTransform;

    public Vector2 Head => headTransform.position;

    private BoxCollider2D col;
    private BoxCollider2D Collider => col == null ? col = GetComponent<BoxCollider2D>() : col;

    public float Width => Collider.bounds.size.x;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Head, Vector3.one * 0.25f);
    }
}
