using Unity.Cinemachine;
using UnityEngine;

public class CameraOnPlayer : MonoBehaviour
{
    public CinemachineBrain brain;

    public ICinemachineCamera cam;

    int blendTime = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = GetComponent<CinemachineBrain>();
        

        brain.SetCameraOverride(1,1,cam,cam,1,blendTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
