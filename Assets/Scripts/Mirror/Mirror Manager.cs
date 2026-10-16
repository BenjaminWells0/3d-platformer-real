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
        if (mirrorCamera == null) return;

        Plane[] mirrorPlanes = GeometryUtility.CalculateFrustumPlanes(mirrorCamera);

        foreach (GameObject obj in mirrorWorldObjects)
        {
            if (obj == null) continue; 

            Renderer meshRenderer = obj.GetComponentInChildren<Renderer>();
            Collider collider = obj.GetComponentInChildren<Collider>();

            
            if (collider != null)
            {
                bool isVisible = GeometryUtility.TestPlanesAABB(mirrorPlanes, collider.bounds);

                collider.enabled = isVisible;

                if (meshRenderer != null)
                {
                    meshRenderer.enabled = isVisible;
                }
            }
        }
    }

}
