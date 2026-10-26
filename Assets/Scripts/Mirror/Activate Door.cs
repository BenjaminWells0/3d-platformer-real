using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float amount = 5f;
    private bool canClickToOpen = false;

    private void OnMouseUpAsButton()
    {
        if (door == null)
        {
            return;
        }

        // Rotate the door around the Y axis by 'speed' degrees
        door.transform.Rotate(Vector3.up, amount);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (door == null)
        {
            if(Keyboard.current.bKey.wasPressedThisFrame)
            {
                door.transform.Rotate(Vector3.up, amount);
            }
            if(Gamepad.current.bButton.wasPressedThisFrame)
            {
                door.transform.Rotate(Vector3.up, amount);
            }
        }
    }
}