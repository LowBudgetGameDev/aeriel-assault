using UnityEngine;

public class PowerupHeal : MonoBehaviour, IPowerup
{
    public void Apply(Transform playerTransform)
    {
        playerTransform.GetComponent<HealthSystem>().Heal(10000);
    }
}
