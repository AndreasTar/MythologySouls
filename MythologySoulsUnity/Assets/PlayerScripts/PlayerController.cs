using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] float moveSpeed = 5f;

    [Header("Physics")]
    [SerializeField] float gravity = -9.81f;
    float finGravity;
    [SerializeField] float jumpHeight;
    [SerializeField] float turnSmoothTime = 0.1f;

    [Header("External")]
    [SerializeField] CharacterController controller;

    [Header("Surface Control")]
    [SerializeField] LayerMask groundMask;

    [Header("Debug")]
    [SerializeField] bool debugMode;

    Vector3 velocity = Vector3.zero;
    Vector3 direction = Vector3.zero;
    float turnSmoothVelocity;
    bool isGrounded;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 dist = new Vector3(0, .55f, 0);
        isGrounded = Physics.CheckCapsule(transform.position - dist, transform.position + dist, 0.48f, groundMask);
        HandleGrounded(horizontal, vertical);
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        HandleRotationSmoothing();
        velocity.y += gravity * Time.deltaTime;
        controller.SimpleMove((velocity + (direction * moveSpeed)) * Time.deltaTime);
    }

    void HandleGrounded(float horizontal, float vertical)
    {
        finGravity = isGrounded ? 0 : gravity;
        velocity = isGrounded ? Vector3.zero : velocity;

        if (!isGrounded && !debugMode) return;

        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetButton("Jump"))
        {
            //velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            controller.SimpleMove(new Vector3(0, Mathf.Sqrt(jumpHeight * -2f * gravity), 0));
        }
    }

    void HandleRotationSmoothing()
    {
        if (!(direction.magnitude >= 0.1f)) return;
        if (!isGrounded && !debugMode) return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
    }
}
