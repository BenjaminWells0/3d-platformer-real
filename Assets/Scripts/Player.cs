using UnityEngine;

public class Player : MonoBehaviour
{
    public bool isSeen;
    public bool isHidden;
    public bool isCrouched;

    private float lastSeenTime;

    void Update()
    {
        if (isSeen)
        {
            lastSeenTime = Time.time;
        }
    }

    public bool WasSeenRecently(float gracePeriod = 0.5f)
    {
        return isSeen || (Time.time - lastSeenTime <= gracePeriod);
    }
}
