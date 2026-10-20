using UnityEngine;

public class ActivateWall : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private float speed = 5f;

    private void OnMouseUpAsButton()
    {
        if (wall == null)
        {
            return;
        }

        wall.transform.position += Vector3.up * speed;
    }
}