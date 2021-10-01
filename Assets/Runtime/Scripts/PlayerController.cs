using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(PlayerInput))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float forwardSpeed = 10;
    [SerializeField] private float flapVelocity = 10;

    [SerializeField]
    [Range(0, 180)]
    private float flapAngleDegress = 20;

    [SerializeField]
    private float rotateDownSpeed = 5;

    private Rigidbody2D rb;

    private PlayerInput input;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 velocity = MoveForward(rb.velocity);
        float zRot = RotateDown(transform.rotation.eulerAngles.z);

        if (input.TapUp())
        {
            velocity.y = flapVelocity;
            zRot = flapAngleDegress;
        }

        rb.velocity = velocity;
        transform.rotation = Quaternion.Euler(Vector3.forward * zRot);
    }

    private Vector2 MoveForward(Vector2 velocity)
    {
        return velocity + Vector2.right * forwardSpeed;
    }

    private float RotateDown(float zRot)
    {
        return Mathf.Max(-180, zRot - rotateDownSpeed * Time.deltaTime);
    }
}
