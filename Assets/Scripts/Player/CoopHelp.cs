using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoopHelp : MonoBehaviour
{
    private PlayerInputManager inputManager;

    [Header("Control Scheme Names")]
    [Tooltip("The exact name of your Player 1 Keyboard scheme in the Input Actions window.")]
    public string p1KeyboardScheme = "KeyboardLeft";

    [Tooltip("The exact name of your Player 2 Keyboard fallback scheme in the Input Actions window.")]
    public string p2KeyboardScheme = "KeyboardRight";

    [Tooltip("The exact name of your Gamepad scheme in the Input Actions window (e.g., 'Gamepad' or 'Controller').")]
    public string gamepadControlSchemeName = "Gamepad";

    void Start()
    {
        inputManager = GetComponent<PlayerInputManager>();
        if (inputManager == null) return;

        // 1. Initialize Player 1 on Keyboard/Mouse
        InputDevice[] p1Devices = new InputDevice[] { Keyboard.current, Mouse.current };

        PlayerInput p1 = inputManager.JoinPlayer(
            playerIndex: 0,
            splitScreenIndex: 0,
            controlScheme: p1KeyboardScheme,
            pairWithDevices: p1Devices
        );
        Debug.Log($"Forced Player 1 into the scene with Keyboard/Mouse using scheme: {p1KeyboardScheme}");

        var p1Controller = p1.GetComponentInChildren<CinemachineInputAxisController>();
        if (p1Controller != null)
        {
            p1Controller.PlayerIndex = 0;
        }

        // 2. Determine Player 2 settings based on hardware
        InputDevice[] p2Devices;
        string activeControlScheme;

        if (Gamepad.current != null)
        {
            // Controller is connected -> Pair with the physical Gamepad device
            p2Devices = new InputDevice[] { Gamepad.current };
            
            // FIXED: Now uses the dedicated gamepad string instead of splitting keyboard rules
            activeControlScheme = gamepadControlSchemeName; 

            Debug.Log($"CoopHelp: Gamepad detected. Routing Player 2 to scheme: {activeControlScheme}");
        }
        else
        {
            // No controller -> fallback safely to your split keyboard parameters
            p2Devices = new InputDevice[] { Keyboard.current };
            activeControlScheme = p2KeyboardScheme;
            Debug.Log($"CoopHelp: No Gamepad detected. Defaulting Player 2 to fallback scheme: {activeControlScheme}");
        }

        // Spawn Player 2 using the dynamically matched device and scheme parameters
        PlayerInput p2 = inputManager.JoinPlayer(
            playerIndex: 1,
            splitScreenIndex: 1,
            controlScheme: activeControlScheme,
            pairWithDevices: p2Devices
        );
        Debug.Log($"Forced Player 2 into the scene using scheme: {activeControlScheme}");

        if (p2 != null)
        {
            var p2Controller = p2.GetComponentInChildren<CinemachineInputAxisController>();
            if (p2Controller != null)
            {
                p2Controller.PlayerIndex = 1;
            }

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
