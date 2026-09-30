using Unity.VisualScripting;
using UnityEngine;

public class CrouchObject : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Collider collider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (collider != null)
        {
            TurnOffCollider(collider);
        }
    }

    private void TurnOffCollider(Collider other)
    {
        if (player.isCrouched)
        {
            if (other != null)
            {
                other.enabled = false;
            }
        }
        else
        {
            other.enabled = true;
        }
    }
}
