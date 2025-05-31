using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private float speed = 20f;
    private int damageAmount;

    private bool isBomb;
    private float explosionRadius = 5f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    public void Setup(Vector2 dir, int damageAmount, bool isBomb = false)
    {
        this.isBomb = isBomb;
        this.damageAmount = damageAmount;
        transform.up = dir;

        rigidbody2D.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out HealthSystem healthSystem))
        {
            healthSystem.Damage(damageAmount);
        }

        if (isBomb)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);

            foreach (Collider2D collider in colliders)
            {
                if (collider == collision) continue;

                if (collider.gameObject.TryGetComponent(out healthSystem))
                {
                    healthSystem.Damage(damageAmount);
                }
            }
        }

        Destroy(gameObject);
    }
}
