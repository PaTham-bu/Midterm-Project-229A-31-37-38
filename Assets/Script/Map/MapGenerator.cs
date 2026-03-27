using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    public GameObject[] trackPrefabs;

    [Header("Settings")]
    public int segmentsOnScreen = 5;
    public float safeDistance = 30f;

    private List<GameObject> activeSegments = new List<GameObject>();
    private Transform lastEndPoint;

    void Start()
    {
        // Spawn FIRST segment at origin (fixed start)
        GameObject first = Instantiate(trackPrefabs[0], Vector3.zero, Quaternion.identity);

        lastEndPoint = first.transform.Find("EndPoint");
        activeSegments.Add(first);

        // Spawn next segments
        for (int i = 1; i < segmentsOnScreen; i++)
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        HandleSpawning();
        HandleDeleting();
    }

    void HandleSpawning()
    {
        // Spawn when player is near the end
        if (player.position.z > lastEndPoint.position.z - 100f)
        {
            SpawnSegment();
        }
    }

    void SpawnSegment()
    {
        GameObject prefab = trackPrefabs[Random.Range(0, trackPrefabs.Length)];
        GameObject newSegment = Instantiate(prefab);

        Transform startPoint = newSegment.transform.Find("StartPoint");
        Transform endPoint = newSegment.transform.Find("EndPoint");

        // Align perfectly
        Vector3 offset = newSegment.transform.position - startPoint.position;
        newSegment.transform.position = lastEndPoint.position + offset;

        // Match rotation
        newSegment.transform.rotation = lastEndPoint.rotation;

        lastEndPoint = endPoint;

        activeSegments.Add(newSegment);
    }

    void HandleDeleting()
    {
        if (activeSegments.Count == 0) return;

        GameObject first = activeSegments[0];
        Transform endPoint = first.transform.Find("EndPoint");

        if (player.position.z > endPoint.position.z + safeDistance)
        {
            Destroy(first);
            activeSegments.RemoveAt(0);
        }
    }
}