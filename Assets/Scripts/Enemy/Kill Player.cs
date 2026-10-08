using UnityEngine;
using UnityEngine.Animations;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] SceneChanger changer;
    
    [SerializeField] Transform enemyHandSocket;
    [SerializeField] private Vector3 throwForceAngle = new Vector3(0, 4f, 12f);

    private EnemyState state;
    private PlayerMovement playerMovement;
    private ParentConstraint playerConstraint;
    private Rigidbody playerBody;
    [SerializeField] private float throwTimer = 3f;
    [SerializeField] private float killTimer = 7f;
    public bool canKillPlayer;
    
    void Start()
    {
        state = GetComponent<EnemyState>();
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerMovement = other.GetComponent<PlayerMovement>();
            playerBody = other.GetComponent<Rigidbody>();
            playerConstraint = other.GetComponent<ParentConstraint>();

            if (state != null && state.agent != null)
            {

                state.agent.isStopped = true;
                state.agent.velocity = Vector3.zero;
            }

            if (playerMovement != null)
            {
                playerMovement.speed = 0;
            }

            if (playerConstraint == null || enemyHandSocket == null)
            {
                Debug.LogError("Yeah... you're missing a parentConstraint or Hand Socket");
                return;
            }
           StartGrab();
            canKillPlayer = true;
            StartCoroutine(KillAfterDelay());
        }
    }

    private void StartGrab()
    {

        while (playerConstraint.sourceCount > 0)
        {
            playerConstraint.RemoveSource(0);
        }
        ConstraintSource newThing = new ConstraintSource();
        newThing.sourceTransform = enemyHandSocket;
        newThing.weight = 1.0f;

        playerConstraint.AddSource(newThing);

        playerConstraint.SetRotationOffset(0, Vector3.zero);
        playerConstraint.SetTranslationOffset(0, Vector3.zero);

        playerConstraint.constraintActive = true;
        playerConstraint.enabled = true;

        canKillPlayer = true;
        Debug.Log("Randy Ortons Got His Neck! Hes going for the SLAM");

        Invoke(nameof (ReleaseThrow), throwTimer);
    }

    public void ReleaseThrow()
{
    if (playerConstraint == null) return;

    playerConstraint.constraintActive = false;
    playerConstraint.enabled = false;

    if (playerBody != null)
    {
        Vector3 pushDirection = transform.TransformDirection(throwForceAngle);

        playerBody.AddForce(pushDirection, ForceMode.Impulse);
            Debug.Log("He's Gone Flying!!!!");
    }
        StartCoroutine(KillAfterDelay());
}

    private System.Collections.IEnumerator KillAfterDelay()
    {
        yield return new WaitForSeconds(killTimer);
        ExecuteKill();
    }

    public void ExecuteKill()
    {
        if (changer != null)
        {
            changer.LoadAsylum();
        }
        else
        {
            Debug.LogError("SceneChangerISMissingFromKillPlayer");
        }
    }

}
