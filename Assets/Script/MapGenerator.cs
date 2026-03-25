using UnityEngine;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform player;

    public int platformsOnScreen = 6;
    public float platformLength = 20f;

    private float spawnZ = 0f;
    private List<GameObject> platforms = new List<GameObject>();

    void Start()
    {
        Debug.Log("START WORKING");

        for (int i = 0; i < platformsOnScreen; i++)
        {
            SpawnPlatform();
        }
    }

    void Update()
    {
        if (player.position.z > spawnZ - (platformsOnScreen * platformLength))
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
        Debug.Log("Spawn at Z: " + spawnZ);

        Vector3 pos = new Vector3(0, 0, spawnZ);

        GameObject newPlatform = Instantiate(platformPrefab, pos, Quaternion.identity);

        platforms.Add(newPlatform);

        spawnZ += platformLength;
    }

    void DeletePlatform()
    {
        Destroy(platforms[0]);
        platforms.RemoveAt(0);
    }
}