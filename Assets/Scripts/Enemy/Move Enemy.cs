using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    Player player;
    InsideLocker locker;
    
    private float rotationSpeed = 5f;
    private float stoppingDistance = 1.2f;
    // [SerializeField] private EnemyState enemyState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locker = FindFirstObjectByType<InsideLocker>();
    }

    // Update is called once per frame
    void Update()
    {
        chaseBothPlayer();
        //moveItMoveIt();


        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        transform.LookAt(player.transform.position);
    }

    void chaseBothPlayer()
    {
        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);

        float closestPlayer = Mathf.Infinity;

        Player nearestPlayer = null;

        foreach (Player p in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if (distanceToPlayer < closestPlayer)
            {
                closestPlayer = distanceToPlayer;
                nearestPlayer = p;
            }
        }
        player = nearestPlayer;

    }

    
}
