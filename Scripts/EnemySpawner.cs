using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public int numberOfEnemiesToSpawn = 5;
    public float spawnDelay = 1f;
    public GameObject[] enemyPrefabs;

    private NavMeshTriangulation _triangulation;

    public static int activeEnemies = 0; // Track active enemies globally

    private void Start()
    {
        _triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnEnemies());
    }

    private IEnumerator SpawnEnemies()
    {
        var wait = new WaitForSeconds(spawnDelay);

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            SpawnEnemy();
            yield return wait;
        }
    }

    private void SpawnEnemy()
    {
        int vertexIndex = Random.Range(0, _triangulation.vertices.Length);
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(_triangulation.vertices[vertexIndex], out hit, 2f, -1))
        {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex], hit.position, Quaternion.identity);

            var agent = enemy.GetComponent<NavMeshAgent>();
            if (agent != null)
            {
                agent.Warp(hit.position);
                agent.enabled = true;
            }

            activeEnemies++; // Increment the active enemy count

            enemy.GetComponent<Enemy>().OnEnemyDestroyed += HandleEnemyDestroyed; // Subscribe to the destroy event
        }
        else
        {
            Debug.LogError($"Unable to place NavMeshAgent on NavMesh at {_triangulation.vertices[vertexIndex]}.");
        }
    }

    private void HandleEnemyDestroyed()
    {
        activeEnemies--; // Decrement the active enemy count
    }
}