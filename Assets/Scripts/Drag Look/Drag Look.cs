using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class DragLook : MonoBehaviour
{
    private PlayerInput playerInput;
    public InputActionReference lookActionReference;
    public InputActionReference lookAroundActionReference;

    private CinemachineInputAxisController axisController;
    private void Start()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        axisController = GetComponentInChildren<CinemachineInputAxisController>();

        if (lookActionReference != null) lookActionReference.action.Enable();
        if(lookAroundActionReference != null) lookAroundActionReference.action.Enable();
        ClearCinemachineInput();
    }

    private void Update()
    {
        if(lookAroundActionReference == null ||  axisController == null || playerInput == null) return;
        if(Mouse.current.rightButton.isPressed)
        {
            Vector2 mouseScreenPos = Mouse.current.position.ReadValue();
            float screenMidpoint = Screen.width / 2f;

            if(playerInput.playerIndex == 0 && mouseScreenPos.x < screenMidpoint)
            {
                InjectCinemachineInput();
            }
            else if(playerInput.playerIndex == 1 && mouseScreenPos.x >= screenMidpoint)
            {
                InjectCinemachineInput();
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

    private void InjectCinemachineInput()
    {
        if (axisController.Controllers == null || axisController.Controllers.Count < 2) return;
        axisController.Controllers[0].Input.InputAction = lookActionReference;
        axisController.Controllers[1].Input.InputAction = lookActionReference;
    }

    private void ClearCinemachineInput()
    {
        if (axisController.Controllers.Count < 2) return;
        axisController.Controllers[0].Input.InputAction = null;
        axisController.Controllers[1].Input.InputAction = null;
    }

    

   
}
