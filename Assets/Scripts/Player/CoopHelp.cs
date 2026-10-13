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

        // THE FIX: Define a list containing BOTH pieces of hardware to share
        InputDevice[] sharedDevices = new InputDevice[] { Keyboard.current, Mouse.current };

        // 1. Spawn Player 1 and pass the shared devices list
        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: 0,
            controlScheme: "KeyboardLeft",
            pairWithDevices: sharedDevices // Give P1 access to the keyboard AND mouse
        );
        Debug.Log("Forced Player 1 into the scene with shared hardware.");
        var p1Controller = p1.GetComponentInChildren<CinemachineInputAxisController>();
        if (p1Controller != null)
        {
            p1Controller.PlayerIndex = 0; // Force this camera to ONLY listen to Player 1's inputs!
        }
        // 2. Spawn Player 2 and pass the EXACT SAME shared devices list
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
            p2Controller.PlayerIndex = 1; // Force this camera to ONLY listen to Player 2's inputs!
        }
        // Your existing Cinemachine channel code remains perfectly untouched right below...
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
