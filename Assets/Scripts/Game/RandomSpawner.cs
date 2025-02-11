using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn; 
    public Vector2 spawnAreaSize = new Vector2(10f, 10f); 
    public int spawnCount = 10;
    public LayerMask collisionMask; 
    public float objectRadius = 0.5f;

    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnObject();
        }
    }
    [ContextMenu("Spawn Object")]
    void SpawnObject()
    {
        Vector2 spawnPosition;
        int attempts = 0;
        do
        {
            float randomX = Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2);
            float randomY = Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2);
            spawnPosition = new Vector2(randomX, randomY);
            attempts++;
        }
        while (Physics2D.OverlapCircle(spawnPosition, objectRadius, collisionMask) && attempts < 10);
        spawnPosition += new Vector2(transform.position.x, transform.position.y);
        int index = Random.Range(0, objectsToSpawn.Count);
        Instantiate(objectsToSpawn[index], spawnPosition, Quaternion.identity,transform);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnAreaSize.x, spawnAreaSize.y, 0));
    }
}
