using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragLook : MonoBehaviour
{
    private PlayerInput playerInput;
    private CinemachineInputAxisController axisController;

    [Header("Input References")]
    [Tooltip("Drag your global 'Look' InputActionReference asset file here.")]
    public InputActionReference lookActionReference;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();
        axisController = GetComponentInChildren<CinemachineInputAxisController>();

        // Force the action map data channel awake at start
        if (lookActionReference != null) lookActionReference.action.Enable();

        // Player 2 doesn't use DragLook, so we only run this initialization for Player 1
        if (playerInput != null && playerInput.playerIndex == 0)
        {
            ClearCinemachineInput();
        }
    }

    private void Update()
    {
        if (axisController == null || playerInput == null) return;

        // =================================================================
        // PLAYER 2 (RIGHT VIEWPORT): BYPASSED entirely 
        // =================================================================
        if (playerInput.playerIndex == 1)
        {
            // Player 2 ignores this script entirely so native Input System + Cinemachine works!
            return;
        }

        // =================================================================
        // PLAYER 1 (LEFT VIEWPORT): RAW MOUSE DRAGGING BOUNDS
        // =================================================================
        if (playerInput.playerIndex == 0 && Mouse.current != null)
        {
            if (Mouse.current.rightButton.isPressed)
            {
                Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
                float screenMidpoint = Screen.width / 2f;

                // Player 1 only moves their camera if right-click started on the LEFT screen half
                if (mouseScreenPos.x < screenMidpoint)
                {
                    InjectMouseDeltaInput();
                }
                else
                {
                    ClearCinemachineInput();
                }
            }
            else
            {
                ClearCinemachineInput();
            }
        }
    }

    private void InjectMouseDeltaInput()
    {
        if (axisController == null || axisController.Controllers == null || axisController.Controllers.Count < 2 || lookActionReference == null) return;

        axisController.Controllers[0].Input.InputAction = lookActionReference;
        axisController.Controllers[1].Input.InputAction = lookActionReference;
    }

    private void ClearCinemachineInput()
    {
        if (axisController == null || axisController.Controllers == null || axisController.Controllers.Count < 2) return;

        foreach (var axisSource in axisController.Controllers)
        {
            axisSource.Input.InputAction = null;
        }
    }
}
