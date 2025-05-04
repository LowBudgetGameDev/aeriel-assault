using UnityEngine;

public class Bullet : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private float speed = 20f;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

        Destroy(gameObject, 5f);
    }

    public void Setup(Vector2 dir)
    {
        transform.up = dir;

        rigidbody2D.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
