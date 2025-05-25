using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private float shootTime = 0.5f;

    [SerializeField] private Transform playerTransform;

    private float shootTimer;

    private void Update()
    {
        float cross = UtilsClass.CrossProduct(transform.up, playerTransform.position - transform.position);

        int rotateDir = cross > 0 ? -1 : 1;

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotateDir * -turnSpeed * Time.deltaTime);

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            Shoot();
            shootTimer += shootTime;
        }
    }

    private void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(transform.up);
    }
}
