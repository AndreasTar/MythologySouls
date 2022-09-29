using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Speeds")]
    [SerializeField] float moveSpeed;

    [Header("Physics")]
    [SerializeField] float gravity;
    [SerializeField] float jumpHeight;
    [SerializeField] float turnSmoothTime;

    [Header("External")]
    [SerializeField] CharacterController controller;

    [Header("Surface Control")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDistance;

    [Header("Debug")]
    [SerializeField] bool debugMode;
    Vector3 velocity;
    Vector3 direction = Vector3.zero;
    float turnSmoothVelocity;
    bool grounded;

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        HandleGrounded(horizontal, vertical);
        //velocity.y += gravity * Time.deltaTime;
        //controller.Move(velocity * Time.deltaTime);
        HandleRotationSmoothing();
        controller.SimpleMove((velocity + direction) * moveSpeed);
    }

    void HandleGrounded(float horizontal, float vertical)
    {
        if (grounded) 
        { 
            velocity.y = 0.0001f; 
        }
        if (!grounded && !debugMode)
        {
            return;
        }
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (Input.GetButton("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleRotationSmoothing()
    {
        if (!(direction.magnitude >= 0.1f)) return;
        if (!grounded && !debugMode) return;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

        Mathf.SmoothDampAngle(
            transform.eulerAngles.y,
            targetAngle,
            ref turnSmoothVelocity,
            turnSmoothTime);
    }
}
