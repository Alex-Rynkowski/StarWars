using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] SpaceShipController spaceShipController;
    [SerializeField] GameObject[] enemies;
    [SerializeField] float minSpawnTimer = .5f;
    [SerializeField] float maxSpawnTimer = .5f;

    [Tooltip("set a time if you want to deactivate spawner")] [SerializeField]
    private float deactivateSpawner = 0;

    [SerializeField] private float zPosition;

    List<BoxCollider> spawners = new List<BoxCollider>();

    float deactivateTimer;
    float spawnTimer;
    float timer = 0;
    Container container;

    private void Start()
    {
        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);

        container = FindObjectOfType<Container>();

        foreach (BoxCollider spawner in GetComponentsInChildren<BoxCollider>())
        {
            spawners.Add(spawner);
        }
    }

    private void Update()
    {
        if (this.deactivateSpawner > 0) DeactivateSpawner();
        if (!this.container.inPlayMode) return;

        this.timer += Time.deltaTime;
        if (this.timer < this.spawnTimer) return;

        spawnTimer = Random.Range(minSpawnTimer, maxSpawnTimer);
        this.timer = 0;
        for (int i = 0; i < this.spawners.Count; i++)
        {
            SpawnEnemy(new Vector3(Random.Range(spawners[i].bounds.min.x, spawners[i].bounds.max.x),
                Random.Range(spawners[i].bounds.min.y, spawners[i].bounds.max.y),
                Random.Range(spawners[i].bounds.min.z, spawners[i].bounds.max.z)));
        }
    }

    private void LateUpdate()
    {
        UpdatePosition(zPosition);
    }

    private Vector3 UpdatePosition(float zAxisPosition)
    {
        var position = spaceShipController.transform.position;
        return this.transform.position = new Vector3(position.x, position.y,
            position.z + zAxisPosition);
    }

    private void SpawnEnemy(Vector3 position)
    {
        Vector3 spawnPosition = position;
        for (int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i], spawnPosition, Quaternion.identity);
        }
    }

    void DeactivateSpawner()
    {
        deactivateTimer += Time.deltaTime;
        if (deactivateTimer >= deactivateSpawner)
        {
            Destroy(this.gameObject);
        }
    }
}