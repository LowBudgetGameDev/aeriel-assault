using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform bulletPrefab;

    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float turnSpeed = 180f;
    [SerializeField] private float shootTime = 0.5f;

    [SerializeField] private Transform playerTransform;


    //private void Update()
    //{


    //    transform.position += transform.up * movementSpeed * Time.deltaTime;

    //    transform.Rotate(0f, 0f, movementInput * -turnSpeed * Time.deltaTime);

    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        Shoot();
    //    }
    //}

    //private void Shoot()
    //{
    //    Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

    //    bullet.GetComponent<Bullet>().Setup(transform.up);
    //}
}
