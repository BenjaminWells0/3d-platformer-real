using UnityEngine.InputSystem;

using UnityEngine;
using UnityEngine.InputSystem;
public class ActivateWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private float speed = 5f;
    private bool canUseButton = false;
    private bool pressedB = false;
    private void Update()
    {
        if (wall != null && canUseButton == true && pressedB == false)
        {

            if (Keyboard.current.bKey.wasPressedThisFrame)
            {
                Debug.Log("keyboard pressed b");
                wall.transform.position = Vector3.up * speed;
                pressedB = true;
            }
            if (Gamepad.current.bButton.wasPressedThisFrame)
            {
                Debug.Log("gamepad pressed b");
                wall.transform.position = Vector3.up * speed;
                pressedB = true;
            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            canUseButton = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            {
            canUseButton = false;
        }
    }
}