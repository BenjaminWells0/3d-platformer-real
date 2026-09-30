using UnityEngine;

public class Mirror : MonoBehaviour
{
    public Transform playerCamera;
    public Transform mirrorCamera;

    private void LateUpdate()
    {
        Vector3 localPosition =
            transform.InverseTransformPoint(playerCamera.position);

        localPosition.z *= -1;

       // mirrorCamera.position =
           // transform.TransformPoint(localPosition);
    }
}