using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoopHelp : MonoBehaviour
{
    private PlayerInputManager inputManager;

    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        if (inputManager == null) return;

        
        InputDevice[] sharedDevices = new InputDevice[] { Keyboard.current, Mouse.current };

        
        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: 0,
            controlScheme: "KeyboardLeft",
            pairWithDevices: sharedDevices 
        );
        Debug.Log("Forced Player 1 into the scene with shared hardware.");
        var p1Controller = p1.GetComponentInChildren<CinemachineInputAxisController>();
        if (p1Controller != null)
        {
            p1Controller.PlayerIndex = 0; 
        }
        
        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: 1,
            controlScheme: "KeyboardRight",
            pairWithDevices: sharedDevices // Give P2 access to the keyboard AND mouse
        );
        Debug.Log("Forced Player 2 into the scene with shared hardware.");
        var p2Controller = p2.GetComponentInChildren<CinemachineInputAxisController>();
        if (p2Controller != null)
        {
            p2Controller.PlayerIndex = 1; 
        }
        
        if (p2 != null)
        {
            CinemachineBrain p2Brain = p2.GetComponentInChildren<CinemachineBrain>();
            CinemachineCamera p2Cam = p2.GetComponentInChildren<CinemachineCamera>();
            if (p2Brain != null && p2Cam != null)
            {
                p2Brain.ChannelMask = OutputChannels.Channel02;
                p2Cam.OutputChannel = OutputChannels.Channel02;
            }
        }
    }

}
