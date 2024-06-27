using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab;  // Zombie prefab to spawn
    public Transform player;         // Reference to the player's transform
    public float initialSpawnRate = 40f;   // Initial time interval between spawns
    public float spawnRateDecrease = 5f; // Amount to decrease spawn rate each interval
    public float minSpawnDistance = 10f;   // Minimum distance zombies should spawn from the player
    public float maxSpawnDistance = 50f;   // Maximum distance zombies can spawn from the player
    public float maxSpawnRate = 200f;       // Minimum time interval between spawns

    private float currentSpawnRate;
    private float nextSpawnTime;

    void Start()
    {
        currentSpawnRate = initialSpawnRate;
        nextSpawnTime = Time.time + currentSpawnRate;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnZombie();
            nextSpawnTime = Time.time + currentSpawnRate;

            // Decrease spawn rate to increase the spawn frequency over time
            currentSpawnRate = Mathf.Min(maxSpawnRate, currentSpawnRate + spawnRateDecrease);
        }
    }

    void SpawnZombie()
    {
        Vector3 spawnPosition = GetRandomSpawnPosition();
        Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
    }

    Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxSpawnDistance;
        randomDirection += player.position;
        UnityEngine.AI.NavMeshHit navHit;
        UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, maxSpawnDistance, -1);

        while (Vector3.Distance(navHit.position, player.position) < minSpawnDistance)
        {
            randomDirection = Random.insideUnitSphere * maxSpawnDistance;
            randomDirection += player.position;
            UnityEngine.AI.NavMesh.SamplePosition(randomDirection, out navHit, maxSpawnDistance, -1);
        }

        return navHit.position;
    }
}
