using UnityEngine;

public class PowerupDefense : MonoBehaviour, IPowerup
{
    public void Apply(Transform playerTransform)
    {
        playerTransform.GetComponent<HealthSystem>().MakeInvinsibleForTime(5f);
    }
}
