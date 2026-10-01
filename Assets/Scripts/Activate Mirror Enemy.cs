using UnityEngine;

public class ActivateMirrorEnemy : MonoBehaviour
{

    [SerializeField] private GameObject MirrorEnemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MirrorEnemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            MirrorEnemy.SetActive(true);

        }
    }
}
