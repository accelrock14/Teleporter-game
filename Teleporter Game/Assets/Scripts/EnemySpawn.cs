using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemyPrefab;
    private GameObject player;
    private Camera mainCamera;

    public float minSpawnIntervel;
    public float maxSpawnIntervel;

    private float timeSinceLastSpawn = 0f;
    private float spawnIntervel;
    private float minX, maxX, minY, maxY;
    private float padding = 0f;
    private float minSpawnDistance = 2f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        spawnIntervel = Random.Range(minSpawnIntervel, maxSpawnIntervel);
        mainCamera = Camera.main;
        Vector3 lowerLeft = mainCamera.ViewportToWorldPoint(new Vector3(padding, padding, 0f));
        Vector3 upperRight = mainCamera.ViewportToWorldPoint(new Vector3(1f - padding, 1f - padding, 0f));
        minX = lowerLeft.x;
        maxX = upperRight.x;
        minY = lowerLeft.y;
        maxY = upperRight.y;
        Spawn();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;
        if(timeSinceLastSpawn >= spawnIntervel)
        {
            Spawn();
        }
    }
    public void Spawn()
    {
        if (player != null)
        {
            Vector2 spawnLocation = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            while (Vector2.Distance(spawnLocation, player.transform.position) < minSpawnDistance)
            {
                spawnLocation = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            }

            Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

            // Rotate the enemy towards the player
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;
            Quaternion spawnRotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);

            Instantiate(enemyPrefab, spawnLocation, spawnRotation);
            timeSinceLastSpawn = 0;
        }
    }
}
