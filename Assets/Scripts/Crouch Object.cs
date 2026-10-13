using Unity.VisualScripting;
using UnityEngine;

public class CrouchObject : MonoBehaviour
{
    [SerializeField] private Collider SolidCollider;

    private void OnTriggerStay(Collider other)
    {
        Player standingPlayer = other.gameObject.GetComponentInParent<Player>();
        if (standingPlayer.isCrouched)
        {
            Physics.IgnoreCollision(other, SolidCollider, true);
        }
        else
        {
            Physics.IgnoreCollision(other, SolidCollider, false);
        }
    }
}
