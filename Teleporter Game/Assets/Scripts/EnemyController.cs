using System.Collections;
using System.Collections.Generic;
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
    private float minSpawnDistance = 2f;
    private float minDistanceFromEdge = 0.05f;
    private float minX, maxX, minY, maxY;

    private GameObject player;
    private Rigidbody2D rigidBody;
    private Vector2 currentDirection;
    private Camera mainCamera;

    public Transform firePoint;
    public GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(padding, padding, 0f));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1f - padding, 1f - padding, 0f));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        minY = lowerLeft.y;
        maxY = upperRight.y;
        currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        teleportInterval = Random.Range(minTeleportInterval, maxTeleportInterval);
        fireRate = Random.Range(minFireInterval, maxFireInterval);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = (Vector2)transform.position + currentDirection * moveSpeed * Time.deltaTime;
        
        Border();
        float newX = Mathf.Clamp(newPosition.x, minX, maxX);
        float newY = Mathf.Clamp(newPosition.y, minY, maxY);
        transform.position = new Vector3(newX, newY, transform.position.z);

        // Rotate the enemy towards the player
        if (player != null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Rotate(), rotateSpeed * Time.deltaTime);
        }

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
    private void Border()
    {
        float distanceFromLeftEdge = transform.position.x - minX;
        float distanceFromRightEdge = maxX - transform.position.x;
        float distanceFromBottomEdge = transform.position.y - minY;
        float distanceFromTopEdge = maxY - transform.position.y;

        // Check if the enemy is too close to the edge of the screen
        if (distanceFromLeftEdge < minDistanceFromEdge)
        {
            // Adjust the x direction to move away from the left edge
            currentDirection.x = Mathf.Abs(currentDirection.x);
        }
        else if (distanceFromRightEdge < minDistanceFromEdge)
        {
            // Adjust the x direction to move away from the right edge
            currentDirection.x = -Mathf.Abs(currentDirection.x);
        }

        if (distanceFromBottomEdge < minDistanceFromEdge)
        {
            // Adjust the y direction to move away from the bottom edge
            currentDirection.y = Mathf.Abs(currentDirection.y);
        }
        else if (distanceFromTopEdge < minDistanceFromEdge)
        {
            // Adjust the y direction to move away from the top edge
            currentDirection.y = -Mathf.Abs(currentDirection.y);
        }

        if (transform.position.x < minX || transform.position.x > maxX)
        {
            // Reverse the x direction
            currentDirection.x *= -1;
        }

        if (transform.position.y < minY || transform.position.y > maxY)
        {
            // Reverse the y direction
            currentDirection.y *= -1;
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
        if(player != null)
        {
            currentDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            Vector2 destination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            while (Vector2.Distance(destination, player.transform.position) < minSpawnDistance)
            {
                destination = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }
            transform.position = destination;

            transform.rotation = Rotate();
        }
    }
    private void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        lastFired = 0f;
        fireRate = Random.Range(minFireInterval, maxFireInterval);
    }
}
