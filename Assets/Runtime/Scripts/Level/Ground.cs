using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters frozenParams;

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
