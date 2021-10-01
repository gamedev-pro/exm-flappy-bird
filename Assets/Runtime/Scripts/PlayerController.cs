using UnityEngine;
using UnityEngine.SceneManagement;

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

    private Vector3 targetVelocity;
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

    public void Die()
    {
        forwardSpeed = 0;
        rb.velocity = Vector2.zero;
        GetComponent<BoxCollider2D>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private Vector2 MoveForward(Vector2 velocity)
    {
        velocity.x = forwardSpeed;
        return velocity;
    }

    private float RotateDown(float zRot)
    {
        return Mathf.Max(-90, zRot - rotateDownSpeed * Time.deltaTime);
    }
}
