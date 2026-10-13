using UnityEngine;
using UnityEngine.InputSystem;

public class DragLook : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction lookAction;
    private InputAction lookAroundAction;

    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        if (playerInput == null)
        {
            playerInput = GetComponent<PlayerInput>();
        }

        if (playerInput != null)
        {
            lookAction = playerInput.actions.FindAction("Look");
            lookAroundAction = playerInput.actions.FindAction("Look Around");

            if (lookAction != null && lookAroundAction != null)
            {
                lookAction.Disable();

                lookAroundAction.started += StartLooking;
                lookAroundAction.canceled += StopLooking;
            }
        }
    }

    private void OnDestroy()
    {
        if (lookAroundAction != null)
        {
            lookAroundAction.started -= StartLooking;
            lookAroundAction.canceled -= StopLooking;
        }
    }

    private void StartLooking(InputAction.CallbackContext context)
    {
        
        if (context.control.device == Mouse.current && playerInput.playerIndex == 0)
        {
            lookAction?.Enable();
        }
    }

    private void StopLooking(InputAction.CallbackContext context)
    {
        lookAction?.Disable();
    }
}
