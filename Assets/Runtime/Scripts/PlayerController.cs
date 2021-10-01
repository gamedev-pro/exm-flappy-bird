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

    public Vector2 Velocity => velocity;

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

    public void Die()
    {
        forwardSpeed = 0;
        velocity = Vector3.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
