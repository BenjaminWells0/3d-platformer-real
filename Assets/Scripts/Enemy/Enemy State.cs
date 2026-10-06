
using UnityEngine;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] private Transform[] patrolPoints;
    private NavMeshAgent agent;
    [SerializeField] private float speed;
    private Vector3 targetLockerPosition;
    private int currentWayPoint = 0;
    [SerializeField] private float viewDistance = 10;
    [SerializeField] private float viewAngle = 90;
    [SerializeField] private LayerMask obstacleMask;
    [SerializeField] private KillPlayer killPlayerScript;
    private bool isHuntingLocker = false;

    public enum m_currentState
    {
        patrol,
        seePlayer,
        chasePlayer,
        playerHidden,
        playerHideWhileSeen,
        killPlayer,

    }
    public m_currentState state;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;
        agent.updatePosition = true;
        agent.updateRotation = true;

        if (patrolPoints.Length > 0)
        {
            GoToNextWaypoint();
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case m_currentState.patrol:
                patrol();
                break;
            case m_currentState.seePlayer:
                seePlayer();
                break;
            case m_currentState.chasePlayer:
                chasePlayer();
                break;
            case m_currentState.playerHidden:
                playerHidden();
                break;
            case m_currentState.playerHideWhileSeen:
                playerHideWhileSeen();
                break;
            case m_currentState.killPlayer:
                killPlayer();
                break;
        }

    }
    private void GoToNextWaypoint()
    {
        
        Debug.Log("Going to waypoint " + currentWayPoint);

        agent.SetDestination(patrolPoints[currentWayPoint].position);
    }
    private void patrol()
    {
        if (CanSeePlayer())
        {
            
            state = m_currentState.chasePlayer;
            return;
        }
        if(patrolPoints.Length == 0)
        {
            return;
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            currentWayPoint++;

            if (currentWayPoint >= patrolPoints.Length)
            {
                currentWayPoint = 0;
            }
            GoToNextWaypoint();
        }
    }

    private void seePlayer()
    {

    }
    private void chasePlayer()
    {
        
        if (state == m_currentState.playerHideWhileSeen)
        {
            return;
        }

        if (!CanSeePlayer())
        {
            
            if (player.isHidden)
            {
                return;
            }

            
            state = m_currentState.patrol;
            GoToNextWaypoint();
            return;
        }

        agent.SetDestination(player.transform.position);
    }

    private bool playerHidden()
    {
        return player.isHidden;
    }

    private void playerHideWhileSeen()
    {
agent.SetDestination(targetLockerPosition);

        if(!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            state = m_currentState.killPlayer;
        }
    }

    private void killPlayer()
    {
        Debug.Log("EnemyOpendLocker");

        if (killPlayerScript != null)
        {
            killPlayerScript.ExecuteKill();
        }
        else
        {
            FindFirstObjectByType<SceneChanger>();
        }
    }

    private bool CanSeePlayer()
    {
        if (player != null)
        {
            if (player.isHidden == true)
            {
                player.isSeen = false;
                return false;
            }

            Vector3 directionToPlayer = player.transform.position - agent.transform.position;
            if (directionToPlayer.magnitude > viewDistance)
            {
                player.isSeen = false;
                return false;
            }

            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle > viewAngle / 2f)
            {
                player.isSeen = false;
                return false;
            }

            if (Physics.Raycast(transform.position, directionToPlayer.normalized, out RaycastHit hit, viewDistance, obstacleMask))
            {

                if (!hit.collider.CompareTag("Player"))
                {
                    player.isSeen = false;
                    return false;
                }

            }
            if (player.isHidden == true)
            {
                return false;
            }
            player.isSeen = true;
            return true;
        }
        else
        {
            
            Debug.Log("No Player Detected");
            return false;
        }
    }
    public void NotifyPlayerHidInLocker(Vector3 lockerPosition)
    {
        targetLockerPosition = lockerPosition;
        state = m_currentState.playerHideWhileSeen;
    }
    // Call this when the player exits the locker safely to reset the AI states
    public void ResetLockerHunt()
    {
        isHuntingLocker = false;
        state = m_currentState.patrol;
        GoToNextWaypoint();
    }
    private void OnDrawGizmos()
    {
        // Draw the enemy's view cone for debugging
        Gizmos.color = Color.yellow;
        Vector3 forward = transform.forward * viewDistance;
        Vector3 leftBoundary = Quaternion.Euler(0, -viewAngle / 2f, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, viewAngle / 2f, 0) * forward;

        Gizmos.DrawRay(transform.position, leftBoundary);
        Gizmos.DrawRay(transform.position, rightBoundary);

        // Draw patrol points if available
        if (patrolPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (var point in patrolPoints)
            {
                if (point != null)
                {
                    Gizmos.DrawSphere(point.position, 0.2f);
                }
            }
        }
    }
}
