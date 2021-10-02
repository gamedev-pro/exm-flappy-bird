using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    private PlayerController playerController;

    private static readonly int velocityYId = Animator.StringToHash("SpeedY");

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    private void LateUpdate()
    {
        animator.SetFloat(velocityYId, playerController.Velocity.y);
    }

    public void Die()
    {
        animator.enabled = false;
        enabled = false;
    }
}
