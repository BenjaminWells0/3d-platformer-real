using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hide : MonoBehaviour
{
    private List<Player> currentPlayerInside = new List<Player>();
    [SerializeField] private Transform insideLockerPosition;
    [SerializeField] private Transform outOfLockerPosition;
    [SerializeField] private InsideLocker insideScript;
    [SerializeField] private bool canGoInLocker = false;
    private void Locker()
    {
        if (insideScript == null) return;
        Player localPlayer = GetLocalPlayer();
        if (!currentPlayerInside.Contains(localPlayer))
        {
            if (localPlayer == null) return;
            currentPlayerInside.Add(localPlayer);

            if (insideScript.insideLocker == false)
            {
                
                bool wasSeenEntering = localPlayer.WasSeenRecently(0.5f);

                localPlayer.transform.position = insideLockerPosition.position;
                
                insideScript.insideLocker = true;

                if (wasSeenEntering)
                {
                    Debug.Log("Enemy Saw you get in");
                    localPlayer.isHidden = false;

                    EnemyState enemy = FindAnyObjectByType<EnemyState>();
                    if (enemy != null)
                    {
                        enemy.tellPlayerHidInLocker(insideScript.transform.position);
                    }
                }
                else
                {
                    Debug.Log("Enemy didnt see you go in");
                    localPlayer.isHidden = true;
                }
            }
        }
        else
        {
            
            currentPlayerInside.Remove(localPlayer);

            localPlayer.transform.position = outOfLockerPosition.position;

            localPlayer.isHidden = false;
            localPlayer.isSeen = false;

            EnemyState enemy = FindAnyObjectByType<EnemyState>();
            if (enemy != null)
            {
                enemy.ResetLockerHunt();
            }
            if(currentPlayerInside.Count == 0)
            {
                insideScript.insideLocker = false;
            }
            
            
        }
    }
    private void Update()
    {
        if(Keyboard.current.bKey.wasPressedThisFrame && canGoInLocker == true)
        {
            Locker();

        }
        if(Gamepad.current.bButton.wasPressedThisFrame && canGoInLocker == true)
        {
            Locker();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canGoInLocker = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        canGoInLocker = false;
    }

    private Player GetLocalPlayer()
            {
                Player[] allPlayers = FindObjectsByType<Player>(FindObjectsInactive.Exclude);
                if (allPlayers.Length == 0) return null;

                Player nearestPlayer = null;
                float shortestDistance = Mathf.Infinity;
        
                foreach (Player p in allPlayers)
                {
                    float distance = Vector3.Distance(transform.position, p.transform.position);
                    if (distance < shortestDistance)
                    {
                        shortestDistance = distance;
                        nearestPlayer = p;
                    }
                }
                return nearestPlayer;
            }
}
