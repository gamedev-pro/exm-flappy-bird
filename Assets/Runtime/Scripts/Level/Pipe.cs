using UnityEngine;

[RequireComponent(typeof(BoxCollider2D), typeof(DeathTrigger))]
public class Pipe : MonoBehaviour
{
    [SerializeField] private Transform headTransform;
    [SerializeField] private Transform tailTransform;

    public Vector2 Head => headTransform.position;
    public Vector2 Tail => tailTransform.position;

    private BoxCollider2D col;
    private BoxCollider2D Collider => col == null ? col = GetComponent<BoxCollider2D>() : col;

    public float Width => Collider.bounds.size.x;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(Head, Vector3.one * 0.25f);

        Gizmos.color = Color.blue;
        Gizmos.DrawCube(Tail, Vector3.one * 0.25f);
    }
}
