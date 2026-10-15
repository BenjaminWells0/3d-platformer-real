using UnityEngine;

public class MirrorManager : MonoBehaviour
{
    [SerializeField] private Camera mirrorCamera;
    private GameObject[] mirrorWorldObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        mirrorWorldObjects = GameObject.FindGameObjectsWithTag("MirrorWorldObject");
    }

    // Update is called once per frame
    void Update()
    {
        
        TurnOnMirrorWorldObjects();
    }

    private void TurnOnMirrorWorldObjects()
    {
        Plane[] mirrorPlanes = GeometryUtility.CalculateFrustumPlanes(mirrorCamera);
        foreach (GameObject obj in mirrorWorldObjects)
        {
            Renderer meshRenderer = obj.GetComponentInChildren<Renderer>();
           Collider collider = obj.GetComponentInChildren<Collider>();
            collider.enabled = GeometryUtility.TestPlanesAABB(mirrorPlanes, collider.bounds);
            meshRenderer.enabled = GeometryUtility.TestPlanesAABB(mirrorPlanes, collider.bounds);
        }
    }
}
