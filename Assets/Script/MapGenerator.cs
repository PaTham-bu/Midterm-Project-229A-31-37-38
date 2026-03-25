using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;

    public int platformsOnScreen = 12;
    public float platformLength = 20f;

    public float minSlope = -12f;
    public float maxSlope = -6f;

    private List<GameObject> platforms = new List<GameObject>();

    private Vector3 nextSpawnPosition = Vector3.zero;
    private Quaternion nextRotation = Quaternion.identity;

    void Start()
    {
        for (int i = 0; i < platformsOnScreen; i++)
        {
            SpawnPlatform();
        }

        // Place player safely
        player.position = nextSpawnPosition + Vector3.up * 2f;
    }

    void Update()
    {
        if (player.position.z > nextSpawnPosition.z - (platformsOnScreen * platformLength))
        {
            SpawnPlatform();
        }

        if (platforms.Count > 0)
        {
            if (player.position.z - platforms[0].transform.position.z > platformLength * 2)
            {
                DeletePlatform();
            }
        }
    }

    void SpawnPlatform()
    {
        // Random slope
        float slope = Random.Range(minSlope, maxSlope);

        Quaternion rotation = nextRotation * Quaternion.Euler(slope, 0, 0);

        GameObject newPlatform = Instantiate(platformPrefab, nextSpawnPosition, rotation);
        platforms.Add(newPlatform);

        // Move spawn point to END of this platform (IMPORTANT)
        nextSpawnPosition += newPlatform.transform.forward * platformLength;

        // Update rotation for next platform
        nextRotation = rotation;
    }

    void DeletePlatform()
    {
        Destroy(platforms[0]);
        platforms.RemoveAt(0);
    }
}