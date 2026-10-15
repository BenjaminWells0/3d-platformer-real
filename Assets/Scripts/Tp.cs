using UnityEngine;

public class Tp : MonoBehaviour
{
    [SerializeField] private GameObject TpLocation;
    Player player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetClosestPlayer();
    }
    private void GetClosestPlayer()
    {
        Player[] players = FindObjectsByType<Player>(FindObjectsSortMode.None);
        float closestPlayer = Mathf.Infinity;
        Player nearestPlayer = null;
        foreach(Player p in players)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, p.transform.position);
            if(distanceToPlayer < closestPlayer)
            {
                closestPlayer = distanceToPlayer;
                nearestPlayer = p;
            }
        }
        player = nearestPlayer;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player != null)
            {
                player.transform.position = TpLocation.transform.position;
            }
        }
    }
    
}
