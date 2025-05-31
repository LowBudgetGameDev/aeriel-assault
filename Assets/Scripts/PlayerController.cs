using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;
    [SerializeField] private Transform bombPrefab;

    private Transform projectilePrefab;

    private float movementSpeed = 10f;
    private float turnSpeed = 180f;
    private int damage = 250;

    private void Awake()
    {
        projectilePrefab = bombPrefab;
    }

    private void Update()
    {
        float movementInput = Input.GetAxisRaw("Horizontal");

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, movementInput * -turnSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        projectile.GetComponent<Bullet>().Setup(transform.up, damage, projectilePrefab == bombPrefab);
    }

    public void ToggleBombsForTime(float time)
    {
        projectilePrefab = bombPrefab;
        FunctionTimer.Create(() =>
        {
            projectilePrefab = bulletPrefab;
        }, time);
    }
}
