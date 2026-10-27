using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CoopHelp : MonoBehaviour
{
    public GameObject player1Prefab;

    [Tooltip("Drag your Player2_Prefab asset here (Gamepad only, no DragLook).")]
    public GameObject player2Prefab;

    public string p1KeyboardScheme = "KeyboardLeft";


    public string gamepadControlSchemeName = "KeyboardRight";
    public string p2KeyboardFallbackScheme = "KeyboardRight";

    void Start()
    {
        if (player1Prefab == null || player2Prefab == null)
        {
            Debug.LogError("CoopHelp: Please assign both Player 1 and Player 2 prefabs in the Inspector.");
            return;
        }


        InputDevice[] p1Devices = new InputDevice[] { Keyboard.current, Mouse.current };

        GameObject p1Obj = Instantiate(player1Prefab, Vector3.zero, Quaternion.identity);
        PlayerInput p1Input = p1Obj.GetComponent<PlayerInput>();

        if (p1Input != null)
        {
            p1Input.neverAutoSwitchControlSchemes = true;
            p1Input.SwitchCurrentControlScheme(p1KeyboardScheme, p1Devices);
        }
        Debug.Log("CoopHelp: Initialized Player 1 using the P1 specific Prefab layout.");



        InputDevice[] p2Devices;
        string activeControlScheme;

        if (Gamepad.current != null)
        {
            p2Devices = new InputDevice[] { Gamepad.current };
            activeControlScheme = gamepadControlSchemeName;
        }
        else
        {
            p2Devices = new InputDevice[] { Keyboard.current };
            activeControlScheme = p2KeyboardFallbackScheme;
        }

        GameObject p2Obj = Instantiate(player2Prefab, Vector3.zero, Quaternion.identity);
        PlayerInput p2Input = p2Obj.GetComponent<PlayerInput>();

        if (p2Input != null)
        {
            p2Input.neverAutoSwitchControlSchemes = true;
            p2Input.SwitchCurrentControlScheme(activeControlScheme, p2Devices);
        }
        Debug.Log($"CoopHelp: Initialized Player 2 using the P2 specific Prefab layout under scheme: {activeControlScheme}");


        if (p2Obj != null)
        {
            CinemachineBrain p2Brain = p2Obj.GetComponentInChildren<CinemachineBrain>();
            CinemachineCamera p2Cam = p2Obj.GetComponentInChildren<CinemachineCamera>();
            if (p2Brain != null && p2Cam != null)
            {
                p2Brain.ChannelMask = OutputChannels.Channel02;
                p2Cam.OutputChannel = OutputChannels.Channel02;
            }
        }


    }
}
