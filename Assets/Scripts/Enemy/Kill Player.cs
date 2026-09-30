using UnityEngine;

public class KillPlayer : MonoBehaviour
{
    [SerializeField] SceneChanger changer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ExecuteKill();
        }
       
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
