using UnityEngine;
using UnityEngine.SceneManagement;
public class MoveEnemy : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
   [SerializeField] Player player;
    InsideLocker locker;
    private Vector3 playerLocation;
   // [SerializeField] private EnemyState enemyState;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        locker = FindFirstObjectByType<InsideLocker>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLocation = player.transform.position;
        
        
        transform.position = Vector3.MoveTowards(transform.position, playerLocation, speed * Time.deltaTime);
        transform.LookAt(playerLocation);
    }


}
