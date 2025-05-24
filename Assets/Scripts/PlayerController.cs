using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    private float movementSpeed = 10f;
    private float turnSpeed = 180f;

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
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(transform.up);
    }
}
