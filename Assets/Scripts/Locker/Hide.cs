using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hide : MonoBehaviour
{
    private List<Player> currentPlayerInside = new List<Player>();
    private List<Player> playersAtDoor = new List<Player>();

    [SerializeField] private Transform insideLockerPosition;
    [SerializeField] private Transform outOfLockerPosition;
    [SerializeField] private Player keyboardPlayer;
    [SerializeField] private Player gamepadPlayer;

    private void Locker(Player localPlayer)
    {
        if (localPlayer == null) return;

        // IF THEY ARE NOT IN THE LOCKER -> HIDE THEM
        if (!currentPlayerInside.Contains(localPlayer))
        {
            currentPlayerInside.Add(localPlayer);
            localPlayer.transform.position = insideLockerPosition.position;
            localPlayer.isHidden = true;

            bool wasSeenEntering = localPlayer.WasSeenRecently(0.5f);

            if (wasSeenEntering && currentPlayerInside.Count == 1)
            {
                Debug.Log("Enemy Saw you get in");
                localPlayer.isHidden = false;

                EnemyState enemy = FindAnyObjectByType<EnemyState>();
                if (enemy != null)
                {
                    enemy.tellPlayerHidInLocker(insideLockerPosition.transform.position);
                }
            }
            else
            {
                Debug.Log("Enemy didnt see you go in");
                localPlayer.isHidden = true;
            }
        }
        // IF THEY ARE ALREADY IN THIS LOCKER -> LET THEM OUT
        else
        {
            currentPlayerInside.Remove(localPlayer);
            localPlayer.transform.position = outOfLockerPosition.position;
            localPlayer.isHidden = false;
            localPlayer.isSeen = false;

            if (currentPlayerInside.Count == 0)
            {
                EnemyState enemy = FindAnyObjectByType<EnemyState>();
                if (enemy != null)
                {
                    enemy.ResetLockerHunt();
                }
            }
        }
    }

    private void Update()
    {
        // KEYBOARD PLAYER DEVICE INPUT (Null-checked first)
        if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame)
        {
            if (playersAtDoor.Contains(keyboardPlayer) || currentPlayerInside.Contains(keyboardPlayer))
            {
                Locker(keyboardPlayer);
            }
        }

        // GAMEPAD PLAYER DEVICE INPUT (Null-checked first)
        if (Gamepad.current != null && Gamepad.current.bButton.wasPressedThisFrame)
        {
            if (playersAtDoor.Contains(gamepadPlayer) || currentPlayerInside.Contains(gamepadPlayer))
            {
                Locker(gamepadPlayer);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player movingPlayer = other.GetComponent<Player>();

            if (movingPlayer != null)
            {
                // Track who is physically standing at the interaction door zone
                if (!playersAtDoor.Contains(movingPlayer))
                {
                    playersAtDoor.Add(movingPlayer);
                }

                // Automatically link references based on their control configuration type
                if (movingPlayer.isGamepadPlayer)
                {
                    gamepadPlayer = movingPlayer;
                }
                else
                {
                    keyboardPlayer = movingPlayer;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player movingPlayer = other.GetComponent<Player>();

            if (movingPlayer != null)
            {
                // Remove from zone safely without risking negative integer values
                playersAtDoor.Remove(movingPlayer);
            }
        }
    }
}
