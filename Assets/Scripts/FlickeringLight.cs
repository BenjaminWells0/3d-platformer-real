 using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Transform enemy;
    public float activeDistance = 10f;
    private Light myLight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myLight = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemy == null) return;
        float currentDistance = Vector3.Distance(transform.position, enemy.position);
        if (currentDistance < activeDistance)
        {
            myLight.intensity = Random.Range(0f, 5f);
        }
        else
        {
            myLight.intensity = 5f;
        }
    }
}
