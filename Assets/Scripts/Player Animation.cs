using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement movement;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Player player;

    private void Update()
    {
        Vector3 horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0f;

        float speed = horizontalVelocity.magnitude;

        animator.SetFloat("Speed", speed);
        animator.SetBool("IsCrouching", player.isCrouched);
        animator.SetBool("IsGrounded", IsGrounded());
        //animator.SetBool("IsSprinting", movement.IsSprinting);
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(
            transform.position,
            Vector3.down,
            1f
        );
    }
}