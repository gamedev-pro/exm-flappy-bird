using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters frozenParams;

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer Sprite => spriteRenderer == null ? spriteRenderer = GetComponent<SpriteRenderer>() : spriteRenderer;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player != null)
        {
            player.MovementParameters = frozenParams;
            player.OnHitGround();
        }
    }
}
