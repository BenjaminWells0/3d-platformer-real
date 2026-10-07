using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] SceneChanger changer;
    EnemyState state;
    PlayerMovement playerMovement;
    public bool canKillPlayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = GetComponent<EnemyState>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            state.agent.speed = 0;
            canKillPlayer = true;
            StartCoroutine(KillAfterDelay());
        }
    }

    private System.Collections.IEnumerator KillAfterDelay()
    {
        yield return new WaitForSeconds(4);
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
