using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragLook : MonoBehaviour
{
    private CinemachineInputAxisController axisController;

    [Header("Input References")]
    [Tooltip("Drag your global 'Look' InputActionReference asset file here.")]
    public InputActionReference lookActionReference;

    private void Start()
    {
        // Find the input axis controller component attached alongside the camera
        axisController = GetComponentInChildren<CinemachineInputAxisController>();

        // Force the action map data channel awake at start
        if (lookActionReference != null)
            lookActionReference.action.Enable();

        // Start with the input cleared so the camera doesn't drift until right-clicked
        ClearCinemachineInput();
    }

    private void Update()
    {
        if (axisController == null || Mouse.current == null) return;

        // =================================================================
        // PLAYER 1 (LEFT VIEWPORT): MOUSE DRAGGING BOUNDS TRACKING
        // =================================================================
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            float screenMidpoint = Screen.width / 2f;

            // Player 1 only moves their camera if right-click happens on the LEFT half of the screen
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

    private void InjectMouseDeltaInput()
    {
        if (axisController == null || axisController.Controllers == null || axisController.Controllers.Count < 2 || lookActionReference == null) return;

        // Safely pass the target look action tracking map back into the camera driver channels
        axisController.Controllers[0].Input.InputAction = lookActionReference; // Horizontal Pan
        axisController.Controllers[1].Input.InputAction = lookActionReference; // Vertical Tilt
    }

    private void ClearCinemachineInput()
    {
        if (axisController == null || axisController.Controllers == null || axisController.Controllers.Count < 2) return;

        // Disconnect the action map reference when not dragging so the camera freezes instantly
        foreach (var axisSource in axisController.Controllers)
        {
            axisSource.Input.InputAction = null;
        }
    }
}
