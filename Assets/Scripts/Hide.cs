using UnityEngine;

public class Hide : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private Transform insideLockerPosition;
    [SerializeField] private Transform outOfLockerPosition;
    [SerializeField] private InsideLocker insideScript;

    private void OnMouseDown()
    {
        if (player == null || insideScript == null) return;

        if (insideScript.insideLocker == false)
        {
            Debug.Log("Player clicked locker: Evaluating historical sightline...");

            // 1. Read the player's recent visibility history window (0.5 second grace period)
            bool wasSeenWhenEntering = player.WasSeenRecently(0.5f);

            // 2. Perform the teleportation
            player.transform.position = insideLockerPosition.position;
            insideScript.insideLocker = true;

            // 3. Command the AI based on recent history
            if (wasSeenWhenEntering)
            {
                Debug.Log("TIMING SUCCESS: Enemy saw you run here recently! Alerting enemy!");
                player.isHidden = false;

                EnemyState enemy = FindFirstObjectByType<EnemyState>();
                if (enemy != null)
                {
                    enemy.NotifyPlayerHidInLocker(insideScript.transform.position);
                }
            }
            else
            {
                Debug.Log("TIMING SUCCESS: Hidden safely out of line-of-sight.");
                player.isHidden = true;
            }
        }
        else if (insideScript.insideLocker == true)
        {
            Debug.Log("Player leaving locker.");
            player.transform.position = outOfLockerPosition.position;

            insideScript.insideLocker = false;
            player.isHidden = false;
            player.isSeen = false;

            EnemyState enemy = FindFirstObjectByType<EnemyState>();
            if (enemy != null)
            {
                enemy.ResetLockerHunt();
            }
        }
    }
}
