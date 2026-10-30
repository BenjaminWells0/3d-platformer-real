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
      
        if (Keyboard.current != null && Keyboard.current.bKey.wasPressedThisFrame)
        {
            if (playersAtDoor.Contains(keyboardPlayer) || currentPlayerInside.Contains(keyboardPlayer))
            {
                Locker(keyboardPlayer);
            }
        }


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

                if (!playersAtDoor.Contains(movingPlayer))
                {
                    playersAtDoor.Add(movingPlayer);
                }


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

                playersAtDoor.Remove(movingPlayer);
            }
        }
    }
}
