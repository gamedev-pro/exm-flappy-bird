using UnityEngine;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;

    [field: SerializeField]
    public PlayerMovementParameters MovementParameters { get; set; }

    [SerializeField]
    private AudioClip flapClip;

    private Vector3 velocity;
    private float zRot;

    private PlayerInput input;

    public Vector2 Velocity => velocity;

    public bool IsDead { get; private set; }

    public bool IsOnGroud { get; private set; }

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        ModifyVelocity();
        RotateDown();
        ProcessInput();

        transform.rotation = Quaternion.Euler(Vector3.forward * zRot);
        transform.position += velocity * Time.deltaTime;
    }

    private void ProcessInput()
    {
        if (input.TapUp())
        {
            Flap();
        }
    }

    public void Flap()
    {
        velocity.y = MovementParameters.FlapVelocity;
        zRot = MovementParameters.FlapAngleDegress;
        AudioUtility.PlayAudioCue(flapClip);
    }

    private void ModifyVelocity()
    {
        velocity.x = MovementParameters.ForwardSpeed;
        velocity.y -= MovementParameters.Gravity * Time.deltaTime;
    }

    private void RotateDown()
    {
        if (velocity.y < 0)
        {
            zRot -= MovementParameters.RotateDownSpeed * Time.deltaTime;
            zRot = Mathf.Max(-90, zRot);
        }
    }

    public void Die()
    {
        if (!IsDead)
        {
            IsDead = true;
            input.enabled = false;
            velocity = Vector3.zero;
            GetComponent<PlayerAnimationController>().Die();
            gameMode.GameOver();
        }
    }

    public void IncrementScore()
    {
        gameMode.IncrementScore();
    }

    public void OnHitGround()
    {
        IsOnGroud = true;
        enabled = false;
    }
}
