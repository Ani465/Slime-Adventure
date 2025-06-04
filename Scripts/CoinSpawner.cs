using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 2f;
    public float heightOffset = 1f;

    private NavMeshTriangulation _triangulation;

    private void Start()
    {
        _triangulation = NavMesh.CalculateTriangulation();
        StartCoroutine(SpawnObjects());
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            SpawnObject();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnObject()
    {
        int vertexIndex = Random.Range(0, _triangulation.vertices.Length);

        NavMeshHit hit;
        if (NavMesh.SamplePosition(_triangulation.vertices[vertexIndex], out hit, 2f, NavMesh.AllAreas))
        {
            var spawnPosition = hit.position + new Vector3(0, heightOffset, 0);
            Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError($"Unable to place object on NavMesh at {_triangulation.vertices[vertexIndex]}.");
        }
    }
}