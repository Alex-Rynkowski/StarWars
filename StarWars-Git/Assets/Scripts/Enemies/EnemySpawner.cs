using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnTimer = 20f;
    [SerializeField] private GameObject[] enemies;

    private float spawnCounter = 0;
    private Container container;

    [Header("Components")] private BoxCollider boxCollider;

    [SerializeField] SpaceShipController spaceShipController;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        container = FindObjectOfType<Container>();
    }

    private void Update()
    {
        if (spaceShipController == null || !container.inPlayMode) return;
        var position = spaceShipController.transform.position;
        this.transform.position = new Vector3(position.x, position.y, position.z + 40);
        spawnCounter += Time.deltaTime;

        if (spawnCounter >= spawnTimer)
        {
            var bounds = boxCollider.bounds;
            SpawnEnemy(new Vector3(Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y), Random.Range(bounds.min.z, bounds.max.z)));
            spawnCounter = 0;
        }
    }

    private void SpawnEnemy(Vector3 spawnPosition)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            Instantiate(enemies[i], spawnPosition, transform.rotation);
        }
    }
}