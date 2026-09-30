using UnityEngine;

public class Tp : MonoBehaviour
{
    [SerializeField] private GameObject TpLocation;
    [SerializeField] private GameObject Player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Player.transform.position = TpLocation.transform.position;
        }
    }
}
