using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField] private Transform bulletPrefab;

    [Header("Basic Info")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float turnSpeed = 90f;
    [SerializeField] private float shootTime = 0.5f;
    [SerializeField] private int damage = 200;

    [Header("Avoidance Info")]
    [SerializeField] private float avoidanceRadius = 2f;
    [SerializeField] private float avoidanceStrength = 5f;

    [Header("Target Info")]
    [SerializeField] private float targetTime = 5f;
    [SerializeField] private float targetRange = 15f;

    private Transform playerTransform;

    private float shootTimer;
    private float targetTimer;

    private Vector3 flyAwayDir;

    public void Setup(Transform playerTransform)
    {
        this.playerTransform = playerTransform;
    }

    private void Awake()
    {
        flyAwayDir = UtilsClass.RandomUnitVector();
    }

    private void Update()
    {
        if (playerTransform == null) return;

        if (Vector3.Distance(transform.position, playerTransform.position) > targetRange)
        {
            MoveToPlayer();
            return;
        }

        if (targetTimer > targetTime)
        {
            FlyAway();
            return;
        }

        targetTimer += Time.deltaTime;

        MoveToPlayer();

        shootTimer -= Time.deltaTime;

        if (shootTimer < 0f)
        {
            Shoot();
            shootTimer += shootTime;
        }
    }

    private void MoveToPlayer()
    {
        Vector3 avoidance = new Vector3();
        int neighborCount = 0;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform != transform && hit.transform.TryGetComponent<EnemyController>(out EnemyController e))
            {
                Vector3 difference = hit.transform.position - transform.position;

                avoidance += difference.normalized / (difference.sqrMagnitude);
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            avoidance /= neighborCount;
        }

        Vector3 toPlayer = (playerTransform.position - transform.position).normalized;

        Vector3 moveDirection = (toPlayer - avoidance * avoidanceStrength).normalized;

        float cross = UtilsClass.CrossProduct(transform.up, moveDirection);

        int rotateDir = cross > 0 ? -1 : 1;

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotateDir * -turnSpeed * Time.deltaTime);
    }

    private void Shoot()
    {
        Transform bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

        bullet.GetComponent<Bullet>().Setup(transform.up, damage);
    }

    private void FlyAway()
    {
        Vector3 avoidance = new Vector3();
        int neighborCount = 0;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, avoidanceRadius);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform != transform && hit.transform.TryGetComponent<EnemyController>(out EnemyController e))
            {
                Vector3 difference = hit.transform.position - transform.position;

                avoidance += difference.normalized / (difference.sqrMagnitude);
                neighborCount++;
            }
        }

        if (neighborCount > 0)
        {
            avoidance /= neighborCount;
        }

        Vector3 defaultDir = flyAwayDir;

        Vector3 moveDirection = (defaultDir - avoidance * avoidanceStrength).normalized;

        float cross = UtilsClass.CrossProduct(transform.up, moveDirection);

        int rotateDir = cross > 0 ? -1 : 1;

        transform.position += transform.up * movementSpeed * Time.deltaTime;

        transform.Rotate(0f, 0f, rotateDir * -turnSpeed * Time.deltaTime);
    }
}
