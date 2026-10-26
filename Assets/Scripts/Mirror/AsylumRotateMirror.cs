using UnityEngine;
using UnityEngine.InputSystem;

public class AsylumRotateMirror : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
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

        var keyboard = Keyboard.current;
        if (keyboard == null)
            return;

        float dir = 0f;
        if (keyboard.downArrowKey.isPressed || Gamepad.current.dpad.down.isPressed) dir -= 1f;
        if (keyboard.upArrowKey.isPressed || Gamepad.current.dpad.up.isPressed) dir += 1f;

        if (Mathf.Approximately(dir, 0f))
            return;

        _puzzleCamTransform.Rotate(Vector3.right, dir * rotationSpeed * Time.deltaTime, Space.World);
    }
}
