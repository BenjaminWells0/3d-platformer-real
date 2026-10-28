using UnityEngine;
using UnityEngine.InputSystem;

public class RotateMirrorCamera : MonoBehaviour
{
    [SerializeField] private bool canRotateMirror = false;
    [SerializeField] private Camera puzzleCamera;
    [SerializeField, Range(10f, 360f)] private float rotationSpeed = 90f;

    private Transform _puzzleCamTransform;

    void Start()
    {
        if (puzzleCamera != null)
        {
            _puzzleCamTransform = puzzleCamera.transform;
        }
        else
        {
            Debug.LogWarning("RotateMirrorCamera: `puzzleCamera` is not assigned.", this);
        }
    }

    void Update()
    {
        RotateCamera();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            canRotateMirror = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other != null && other.CompareTag("Player"))
        {
            canRotateMirror = false;
        }
    }

    private void RotateCamera()
    {
        if (!canRotateMirror || _puzzleCamTransform == null)
            return;



        float horizontal = 0f;
        float vertical = 0f;

        Gamepad playerGamepad = Gamepad.all[0];
        if (Gamepad.all.Count == 0)
            return;
        if (playerGamepad != null)
        {
            if (playerGamepad.dpad.left.isPressed) horizontal += 1f;
            if (playerGamepad.dpad.right.isPressed) horizontal -= 1f;
            if (playerGamepad.dpad.up.isPressed) vertical += 1f;
            if (playerGamepad.dpad.down.isPressed) vertical -= 1f;
        }
        var keyboard = Keyboard.current;
        if (keyboard != null)
        { 
        if (keyboard.leftArrowKey.isPressed) horizontal += 1f;
        if (keyboard.rightArrowKey.isPressed) horizontal -= 1f;
        if (keyboard.upArrowKey.isPressed) vertical += 1f;
        if (keyboard.downArrowKey.isPressed) vertical -= 1f;
        }
        
        _puzzleCamTransform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime, Space.World);

        
        _puzzleCamTransform.Rotate(Vector3.right, -vertical * rotationSpeed * Time.deltaTime, Space.Self);
    }
}
