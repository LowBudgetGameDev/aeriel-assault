using UnityEngine;

public class Powerup : MonoBehaviour
{
    private IPowerup powerup;

    private void Awake()
    {
        powerup = GetComponent<IPowerup>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        powerup.Apply(collision.transform);

        Destroy(gameObject);
    }
}
