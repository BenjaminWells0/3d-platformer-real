using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoopHelp : MonoBehaviour
{
    private PlayerInputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();

        if (inputManager == null)
        {
           
            return;
        }

    
        PlayerInput p1 = inputManager.JoinPlayer(playerIndex: 0, splitScreenIndex: 0);
        Debug.Log("Forced Player 1 into the scene.");
        
       
        PlayerInput p2 = inputManager.JoinPlayer(playerIndex: 1, splitScreenIndex: 1);
        Debug.Log("Forced Player 2 into the scene.");

        CinemachineBrain p2Brain = p2.GetComponentInChildren<CinemachineBrain>();
        CinemachineCamera p2Cam = p2.GetComponentInChildren<CinemachineCamera>();

        p2Brain.ChannelMask = OutputChannels.Channel02;
        p2Cam.OutputChannel = OutputChannels.Channel02;
        
    }
}
