using UnityEngine;

public class InsideLocker : MonoBehaviour
{
    public Player player;
    public bool insideLocker;

    private void OnTriggerEnter(Collider other)
    {
        Player detectedPlayer = other.GetComponentInParent<Player>();
        if (detectedPlayer != null)
        {
            player = detectedPlayer;
            insideLocker = true;
        }
    }
}
