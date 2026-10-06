using UnityEngine;

public class ActivateButton : MonoBehaviour
{
    [SerializeField] private GameObject teleportButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    

    void Start()
    {
       
        if (teleportButton != null )
        { 
            teleportButton.SetActive(false); 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    private void OnTriggerEnter(Collider other)
    {
        if(teleportButton != null)
        {
            teleportButton.SetActive(true);
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(teleportButton != null)
        {
            teleportButton.SetActive(false);
        }
    }
}
