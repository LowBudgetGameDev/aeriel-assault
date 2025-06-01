using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private Transform playerTransform;

    [Header("Enemy Info")]
    [SerializeField] private Transform[] enemyPrefabs; // 0 - Normal, 1 - Fast, 2 - Strong
    [SerializeField] private float[] initSpawnChances;

    [Header("Spawning Info")]
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private Vector2 halfSpawnSize;

    [Header("Wave Info")]
    [SerializeField] private float chanceUpdateTime = 15f;
    [SerializeField] private float equalizingTime = 180f;

    [Header("Powerup Info")]
    [SerializeField] private Transform[] powerupPrefabs;
    [SerializeField] private float powerupSpawnTime = 5f;

    private float[] spawnChances;
    private float spawnTimer;

    private float waveTimer;
    private int wave;

    private float powerupTimer;

    private Vector2 halfCameraSize;

    private void Awake()
    {
        waveTimer = chanceUpdateTime;
        powerupTimer = powerupSpawnTime;

        Camera mainCamera = UtilsClass.GetMainCamera();
        halfCameraSize = new Vector2(
            mainCamera.orthographicSize * mainCamera.aspect,
            mainCamera.orthographicSize);

        spawnChances = initSpawnChances;
    }

    private void Update()
    {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer < 0f)
        {
            SpawnEnemy();
            spawnTimer = spawnTime;
        }

        waveTimer -= Time.deltaTime;

        if (waveTimer < 0f)
        {
            wave++;
            UpdateSpawnChances();
            waveTimer = chanceUpdateTime;
        }

        powerupTimer -= Time.deltaTime;

        if (powerupTimer < 0f)
        {
            SpawnPowerup();
            powerupTimer = powerupSpawnTime;
        }
    }

    private void SpawnEnemy()
    {
        float randomValue = Random.Range(0f, 1f);

        Transform enemyPrefab;

        if (randomValue < spawnChances[0])
        {
            enemyPrefab = enemyPrefabs[0];
        }
        else if (randomValue < spawnChances[0] + spawnChances[1])
        {
            enemyPrefab = enemyPrefabs[1];
        }
        else
        {
            enemyPrefab = enemyPrefabs[2];
        }

        Vector3 spawnPosition = new Vector3(Random.Range(-halfSpawnSize.x, halfSpawnSize.x), Random.Range(-halfSpawnSize.y, halfSpawnSize.y));

        if (Mathf.Clamp(spawnPosition.x, playerTransform.position.x - halfCameraSize.x, playerTransform.position.x + halfCameraSize.x) == spawnPosition.x &&
            Mathf.Clamp(spawnPosition.y, playerTransform.position.y - halfCameraSize.y, playerTransform.position.y + halfCameraSize.y) == spawnPosition.y)
        {
            float buffer = 1f;

            spawnPosition.x = spawnPosition.x - playerTransform.position.x < 0f ? playerTransform.position.x - halfCameraSize.x - buffer : playerTransform.position.x + halfCameraSize.x + buffer;
            spawnPosition.y = spawnPosition.y - playerTransform.position.y < 0f ? playerTransform.position.y - halfCameraSize.y - buffer : playerTransform.position.y + halfCameraSize.y + buffer;
        }

        Transform enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);

        enemy.GetComponent<EnemyController>().Setup(playerTransform);
    }

    private void SpawnPowerup()
    {
        int randomIndex = Random.Range(0, powerupPrefabs.Length);

        Transform powerupPrefab = powerupPrefabs[randomIndex];

        Vector3 spawnPosition = new Vector3(Random.Range(-halfSpawnSize.x, halfSpawnSize.x), Random.Range(-halfSpawnSize.y, halfSpawnSize.y));

        Transform powerup = Instantiate(powerupPrefab, spawnPosition, Quaternion.identity);
    }

    private void UpdateSpawnChances()
    {
        float t = wave * chanceUpdateTime / equalizingTime;

        t = Mathf.Clamp01(t);

        for (int i = 0; i < spawnChances.Length; i++)
        {
            spawnChances[i] = GetUpdatedChance(initSpawnChances[i], t);
        }
    }

    private float GetUpdatedChance(float initChance, float t)
    {
        return initChance * (1 - t) + t / 3;
    }

}
