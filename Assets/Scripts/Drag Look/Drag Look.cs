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

        axisController = GetComponentInChildren<CinemachineInputAxisController>();

        if (lookActionReference != null)
            lookActionReference.action.Enable();


        ClearCinemachineInput();
    }

    private void Update()
    {
        if (axisController == null || Mouse.current == null) return;
        if (Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            float screenMidpoint = Screen.width / 2f;


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
