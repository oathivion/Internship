using UnityEngine;
using System.Collections.Generic;

public class WorldGenerator : MonoBehaviour
{
    [Header("Tile Prefabs")]
    public GameObject[] tilePrefabs;

    [Header("Generation Settings")]
    public int platformCount = 30;
    public Vector2Int areaSize = new Vector2Int(30, 10);
    public float tileSpacing = 1;

    private HashSet<Vector2> occupiedPositions = new HashSet<Vector2>();

    void Start()
    {
        GeneratePlatforms();
    }

    void GeneratePlatforms()
    {
        int tries = 0;
        int maxTries = platformCount * 10;

        while (platformCount > 0 && tries < maxTries)
        {
            tries++;

            int platformSize = Random.Range(1, 4); // 1, 2, or 3 tiles wide
            float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f - platformSize + 1);
            float y = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);

            Vector2 basePos = new Vector2(
                Mathf.Round(x / tileSpacing) * tileSpacing,
                Mathf.Round(y / tileSpacing) * tileSpacing
            );

            // Check if space is free
            bool canPlace = true;
            for (int i = 0; i < platformSize; i++)
            {
                Vector2 pos = basePos + new Vector2(i * tileSpacing, 0);
                if (occupiedPositions.Contains(pos))
                {
                    canPlace = false;
                    break;
                }
            }

            if (!canPlace)
                continue;

            // Place platform tiles
            for (int i = 0; i < platformSize; i++)
            {
                Vector2 pos = basePos + new Vector2(i * tileSpacing, 0);
                GameObject prefab = tilePrefabs[Random.Range(0, tilePrefabs.Length)];
                Instantiate(prefab, pos, Quaternion.identity, transform);
                occupiedPositions.Add(pos);
            }

            platformCount--;
        }
    }
}
