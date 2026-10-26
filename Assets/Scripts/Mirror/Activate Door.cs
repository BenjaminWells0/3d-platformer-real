using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float amount = 5f;
    [SerializeField] private bool canClickToOpen = false;
    private bool pressedB = false;
    private void Start()
    {
        
    }
    private void Update()
    {
        if (door != null && canClickToOpen == true && pressedB == false)
        {

                if (Keyboard.current.bKey.wasPressedThisFrame)
                {
                    Debug.Log("keyboard pressed b");
                    door.transform.Rotate(Vector3.up, amount);
                pressedB = true;
                }
                if (Gamepad.current.bButton.wasPressedThisFrame)
                {
                    Debug.Log("gamepad pressed b");
                    door.transform.Rotate(Vector3.up, amount);
                pressedB = true;
                }

        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        canClickToOpen = true;
        
    }
    private void OnTriggerExit(Collider other)
    {
        canClickToOpen = false;
    }
}