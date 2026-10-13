using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Player player;
    public Rigidbody rb;

    
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpAction;
    private InputAction crouchAction;


    public float speed = 6f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float groundDepth = 1f;
    [SerializeField] private float rotationSpeed = 720f;

    private Transform cameraTransform;

    private Vector2 dir;
    private Vector3 movementDirection;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            // Find the specific actions inside this player's isolated asset map instance
            // Make sure these strings match the exact names of your actions in the editor asset!
            moveAction = playerInput.actions.FindAction("Move");
            jumpAction = playerInput.actions.FindAction("Jump");
            crouchAction = playerInput.actions.FindAction("Crouch");
        }

        // Your existing camera code remains right here...
        // Replace the old Camera.main line inside Start() with this:
        Camera localCam = GetComponentInChildren<Camera>();
        if (localCam != null)
        {
            cameraTransform = localCam.transform;
        }

    }

    

    private void Update()
    {
        
        if (moveAction == null) return;

        
        dir = moveAction.ReadValue<Vector2>();

        WhereToMove();

        if (jumpAction.WasPressedThisFrame() && IsGrounded())
        {
            Jump();
        }

        if (crouchAction.WasPressedThisFrame())
        {
            Crouch();
        }

        if (crouchAction.WasReleasedThisFrame())
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