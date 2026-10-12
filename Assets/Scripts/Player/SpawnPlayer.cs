using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnPlayer : MonoBehaviour
{
    [Tooltip("The position where newly joined players will spawn.")]
    public Transform spawnPoint;

    // This method is called automatically by your PlayerInputManager component
    public void OnPlayerJoined(PlayerInput pi)
    {
        if (spawnPoint == null)
        {
            Debug.LogWarning("Spawn Point is not assigned on the SpawnPlayer script!", this);
            return;
        }

        // Direct position assignment works instantly for basic Transforms and Rigidbodies
        pi.transform.position = spawnPoint.position;

        Debug.Log($"Player {pi.playerIndex} successfully spawned at {spawnPoint.position}");
    }
}
