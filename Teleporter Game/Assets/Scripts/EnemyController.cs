using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Movement variables")]
    public float moveSpeed = 2f;
    public float padding = 0f;
    public float rotateSpeed = 1000f;
    [Header("Teleport variables")]
    public float minTeleportInterval = 2f;
    public float maxTeleportInterval = 4f;
    [Header("Fire variables")]
    public float minFireInterval = 2f;
    public float maxFireInterval = 4f;

    private float fireRate;
    private float lastFired = 0f;
    private float teleportInterval;
    private float timeSinceLastSpawn = 0f;
    private float minSpawnDistance = 5f;
    private float minX, maxX, minY, maxY;

    private GameObject player;
    private Rigidbody2D rb;
    private Camera mainCamera;
    private GameManager gameState;

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        gameState = FindObjectOfType<GameManager>();

        teleportInterval = Random.Range(minTeleportInterval, maxTeleportInterval);
        fireRate = Random.Range(minFireInterval, maxFireInterval);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Rotate the enemy towards the player
        if (gameState.state != GameState.LOST)
        {
            Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(padding, padding, 0f));
            Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1f - padding, 1f - padding, 0f));
            minX = lowerLeft.x;
            maxX = upperRight.x;
            minY = lowerLeft.y;
            maxY = upperRight.y;

            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
            directionToPlayer.Normalize();
            rb.velocity = transform.up * moveSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotate(), rotateSpeed * Time.deltaTime);

            timeSinceLastSpawn += Time.deltaTime;

            if (timeSinceLastSpawn >= teleportInterval)
            {
                Teleport();
                timeSinceLastSpawn = 0f;
                teleportInterval = Random.Range(minTeleportInterval, maxTeleportInterval);
            }

            lastFired += Time.deltaTime;
            if (lastFired >= fireRate)
            {
                Shoot();
            }
        }
    }
    private Quaternion Rotate()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        // Rotate the enemy towards the player
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
        return targetRotation;
    }
    private void Teleport()
    {
        Vector2 currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        Vector2 destination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        while (Vector2.Distance(destination, player.transform.position) < minSpawnDistance)
        {
            destination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        }
        transform.position = destination;

        transform.rotation = Rotate();
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        lastFired = 0f;
        fireRate = Random.Range(minFireInterval, maxFireInterval);
    }
}
