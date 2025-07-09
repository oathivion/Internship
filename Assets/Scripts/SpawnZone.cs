using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public Vector2 zoneSize = new Vector2(10f, 10f);
    public GameObject[] enemyPrefabs;
    public int enemyCount = 5;

    void Start()
    {
        GenerateEnemies();
    }

    void GenerateEnemies()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Vector2 spawnPos = GetRandomPointInZone();
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }

    Vector2 GetRandomPointInZone()
    {
        Vector2 center = transform.position;
        float x = Random.Range(center.x - zoneSize.x / 2f, center.x + zoneSize.x / 2f);
        float y = Random.Range(center.y - zoneSize.y / 2f, center.y + zoneSize.y / 2f);
        return new Vector2(x, y);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, zoneSize);
    }
}
