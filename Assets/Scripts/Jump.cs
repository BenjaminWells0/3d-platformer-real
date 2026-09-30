using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 720f; // Degrees per second

    private Vector2 inputVector;
    private Vector3 movementDirection;
    private Transform cameraTransform;

    void Start()
    {
        // Cache the main camera's transform for direction math
        if (Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    // 1. Read the Input Vector (Hook this up to Player Input component events)
    public void OnMove(InputValue value)
    {
        inputVector = value.Get<Vector2>();
    }

    void Update()
    {
        CalculateMovementDirection();
        MovePlayer();
        RotatePlayer();
    }

    private void CalculateMovementDirection()
    {
        // 2 & 3. Map 2D input to 3D space relative to the camera and normalize
        if (cameraTransform != null)
        {
            // Get camera vectors and flatten them (ignore vertical tilt)
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;
            camForward.y = 0;
            camRight.y = 0;

            camForward.Normalize();
            camRight.Normalize();

            // Calculate movement relative to where the camera is looking
            movementDirection = (camForward * inputVector.y + camRight * inputVector.x).normalized;
        }
        else
        {
            // Fallback to world axes if no camera is found
            movementDirection = new Vector3(inputVector.x, 0f, inputVector.y).normalized;
        }
    }

    private void MovePlayer()
    {
        // Basic transform movement (Swap this out if you use Rigidbody/CharacterController)
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime, Space.World);
    }

    private void RotatePlayer()
    {
        // 4. Check for Movement (The Golden Rule) - Only rotate if inputting direction
        if (movementDirection != Vector3.zero)
        {
            // 5. Calculate and apply the smooth rotation
            Quaternion targetRotation = Quaternion.LookRotation(movementDirection);

            transform.rotation = Quaternion.RotateTowards(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}
