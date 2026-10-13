using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    [Tooltip("The position where newly joined players will spawn.")]
    public Transform spawnPoint1;
    public Transform spawnPoint2;
    // This method is called automatically by your PlayerInputManager component
    public void OnPlayerJoined(PlayerInput pi)
    {
        if (spawnPoint1 == null)
        {
            Debug.LogWarning("Spawn Point is not assigned on the SpawnPlayer script!", this);
            return;
        }

        // Direct position assignment works instantly for basic Transforms and Rigidbodies
        pi.transform.position = spawnPoint1.position;

        Debug.Log($"Player {pi.playerIndex} successfully spawned at {spawnPoint1.position}");
    }
}
