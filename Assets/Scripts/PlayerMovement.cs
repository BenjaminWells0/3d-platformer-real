using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    public Rigidbody rb;

    public InputActionReference move;
    public InputActionReference jump;
    public InputActionReference crouch;

    [SerializeField] private float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDepth = 1f;
    [SerializeField] private float rotationSpeed = 720f;

    private Transform cameraTransform;

    private Vector2 dir;
    private Vector3 movementDirection;

    private void Start()
    {
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void OnEnable()
    {
        move.action.Enable();
        jump.action.Enable();
        crouch.action.Enable();
    }

    private void OnDisable()
    {
        move.action.Disable();
        jump.action.Disable();
        crouch.action.Disable();
    }

    private void Update()
    {
        dir = move.action.ReadValue<Vector2>();

        WhereToMove();

        if (jump.action.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if (crouch.action.WasPressedThisFrame())
        {
            Crouch();
        }

        if (crouch.action.WasReleasedThisFrame())
        {
            player.isCrouched = false;
        }
    }

    private void FixedUpdate()
    {
        RotatePlayer();

        Vector3 targetVelocity = movementDirection * speed;
        rb.linearVelocity = new Vector3(targetVelocity.x, rb.linearVelocity.y, targetVelocity.z);
    }

    private void WhereToMove()
    {
        if (cameraTransform == null) return;

        if (dir.sqrMagnitude < 0.01f)
        {
            movementDirection = Vector3.zero;
            return;
        }

        // Get camera horizontal directions
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        movementDirection = forward * dir.y + right * dir.x;

        if (movementDirection.sqrMagnitude > 1f)
        {
            movementDirection.Normalize();
        }
    }

    private void RotatePlayer()
    {
        if (movementDirection.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            // Smooth rotation without infinite spiral
            rb.MoveRotation(
                Quaternion.RotateTowards(
                    rb.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                )
            );
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void Crouch()
    {
        player.isCrouched = true;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            groundDepth
        );
    }
}