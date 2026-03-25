using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] platformPrefabs;
    public Transform player;

    public int platformsOnScreen = 6;

    private List<GameObject> platforms = new List<GameObject>();
    private Vector3 nextSpawnPosition = Vector3.zero;

    void Start()
    {
        for (int i = 0; i < platformsOnScreen; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player.position.z > nextSpawnPosition.z - 50f)
        {
            SpawnPlatform();
        }

        if (platforms.Count > platformsOnScreen)
        {
            Destroy(platforms[0]);
            platforms.RemoveAt(0);
        }
    }

    void SpawnPlatform()
    {
        if (platformPrefabs.Length == 0)
        {
            Debug.LogError("No platform prefabs!");
            return;
        }

        GameObject prefab = platformPrefabs[Random.Range(0, platformPrefabs.Length)];

        GameObject newPlatform = Instantiate(prefab, nextSpawnPosition, Quaternion.identity);

        platforms.Add(newPlatform);

        // 🔥 Move spawn position forward using REAL size
        float length = GetPlatformLength(newPlatform);
        nextSpawnPosition += Vector3.forward * length;
    }

    float GetPlatformLength(GameObject obj)
    {
        Collider col = obj.GetComponent<Collider>();

        if (col == null)
        {
            Debug.LogError("Platform has no collider!");
            return 20f;
        }

        return col.bounds.size.z;
    }
}