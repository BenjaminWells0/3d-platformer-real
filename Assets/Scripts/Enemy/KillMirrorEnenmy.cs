using UnityEngine;

public class KillMirrorEnenmy : MonoBehaviour
{
    [SerializeField] private SceneChanger changer;
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
        if (collision.gameObject.CompareTag("Player"))
        {
            changer.LoadDeath();
        }
    }
}
