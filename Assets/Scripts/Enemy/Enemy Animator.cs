using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    EnemyState state;
    KillPlayer kill;
    Animator animator;
    Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        state = GetComponent<EnemyState>();
        kill = GetComponent<KillPlayer>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 horizontalVelocity = rb.linearVelocity;
        horizontalVelocity.y = 0f;

        float speed = horizontalVelocity.magnitude;

        animator.SetFloat("Speed", state.speed);
        animator.SetBool("canKillPlayer", kill.canKillPlayer);
    }
}
