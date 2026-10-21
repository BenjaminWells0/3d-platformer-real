using UnityEngine;

public class Hide : MonoBehaviour
{
    private Player currentPlayerInside = null;
    [SerializeField] private Transform insideLockerPosition;
    [SerializeField] private Transform outOfLockerPosition;
    [SerializeField] private InsideLocker insideScript;
    [SerializeField] private float goInRange = 5f;
    private void OnMouseDown()
    {
        if (insideScript == null) return;

        if (currentPlayerInside == null)
        {

            Player localPlayer = GetLocalPlayer();
            if (localPlayer == null) return;
            
            float distanceToLocker = Vector3.Distance(transform.position, localPlayer.transform.position);
            if (distanceToLocker > goInRange) return;

            if (insideScript.insideLocker == false)
            {
                Debug.Log($"{localPlayer.name} clicked locker: ");
                bool wasSeenEntering = localPlayer.WasSeenRecently(0.5f);

                localPlayer.transform.position = insideLockerPosition.position;
                currentPlayerInside = localPlayer;
                insideScript.insideLocker = true;

                if (wasSeenEntering)
                {
                    Debug.Log("Enemy Saw you get in");
                    localPlayer.isHidden = false;

                    EnemyState enemy = FindAnyObjectByType<EnemyState>();
                    if (enemy != null)
                    {
                        enemy.NotifyPlayerHidInLocker(insideScript.transform.position);
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
            Player localPlayer = GetLocalPlayer();
            float distanceToLocker = Vector3.Distance(transform.position, localPlayer.transform.position);
            if (distanceToLocker > goInRange) return;

            if (localPlayer != currentPlayerInside) return;
            Debug.Log($"{currentPlayerInside.name} leaving locker");
            currentPlayerInside.transform.position = outOfLockerPosition.position;

            currentPlayerInside.isHidden = false;
            currentPlayerInside.isSeen = false;

            EnemyState enemy = FindAnyObjectByType<EnemyState>();
            if (enemy != null)
            {
                enemy.ResetLockerHunt();
            }
            insideScript.insideLocker = false;
            currentPlayerInside = null;
        }
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
