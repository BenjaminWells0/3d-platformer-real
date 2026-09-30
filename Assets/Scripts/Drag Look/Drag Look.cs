using UnityEngine;
using UnityEngine.InputSystem;

public class DragLook : MonoBehaviour
{
    [SerializeField] private InputActionReference look;
    [SerializeField] private InputActionReference lookAround;

    private void OnEnable()
    {
        lookAround.action.Enable();

        
        look.action.Disable();

        lookAround.action.started += StartLooking;
        lookAround.action.canceled += StopLooking;
    }

    private void OnDisable()
    {
        lookAround.action.started -= StartLooking;
        lookAround.action.canceled -= StopLooking;

        lookAround.action.Disable();
        look.action.Disable();
    }

    private void StartLooking(InputAction.CallbackContext context)
    {
        look.action.Enable();
    }

    private void StopLooking(InputAction.CallbackContext context)
    {
        look.action.Disable();
    }
}