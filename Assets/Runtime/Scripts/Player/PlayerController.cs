using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10;
    [SerializeField] private float flapVelocity = 10;

    [SerializeField] private float gravity = 1.8f * 9.8f;

    [SerializeField]
    [Range(0, 180)]
    private float flapAngleDegress = 20;

    [SerializeField]
    private float rotateDownSpeed = 5;

    private Vector3 velocity;
    private float zRot;

    private PlayerInput input;

    public Vector3 Velocity => velocity;

    private void Awake()
    {
        input = GetComponent<PlayerInput>();
    }

    void Update()
    {
        ModifyVelocity();
        RotateDown();
        ProcessInput();

        transform.rotation = Quaternion.Euler(Vector3.forward * zRot);
        transform.position += velocity * Time.deltaTime;
    }

    private float ProcessInput()
    {
        if (input.TapUp())
        {
            velocity.y = flapVelocity;
            zRot = flapAngleDegress;
        }

        return zRot;
    }

    private void ModifyVelocity()
    {
        velocity.x = forwardSpeed;
        velocity.y -= gravity * Time.deltaTime;
    }

    private void RotateDown()
    {
        if (velocity.y < 0)
        {
            zRot -= rotateDownSpeed * Time.deltaTime;
            zRot = Mathf.Max(-90, zRot);
        }
    }
}
