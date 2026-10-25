using UnityEngine;

public class ActivateDoor : MonoBehaviour
{
    [SerializeField] private GameObject door;
    [SerializeField] private float amount = 5f;

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
            return;
        }
    }
}