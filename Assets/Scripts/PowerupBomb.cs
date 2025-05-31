using UnityEngine;

public class PowerupBomb : MonoBehaviour, IPowerup
{
    public void Apply(Transform playerTransform)
    {
        playerTransform.GetComponent<PlayerController>().ToggleBombsForTime(10f);
    }
}
