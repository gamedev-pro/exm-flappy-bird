using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters frozenParams;

    private SpriteRenderer spriteRenderer;
    public SpriteRenderer Sprite => spriteRenderer == null ? spriteRenderer = GetComponent<SpriteRenderer>() : spriteRenderer;
}
