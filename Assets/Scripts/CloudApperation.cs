using System;
using UnityEngine;

public class CloudApperation : MonoBehaviour
{
    public GameObject cloudline, cloudBeingSpawned;
    public int cloudCount;
    public Vector3 minBoundingArea, maxBoundingArea, randomPlaceInBounds;

    void Start()
    {
        cloudline = this.gameObject;
        minBoundingArea = (cloudline.GetComponent<BoxCollider>().bounds.min);
        maxBoundingArea = (cloudline.GetComponent<BoxCollider>().bounds.max);
        CloudGenerator();
    }
    public void CloudGenerator()
    {
        for(int i = 0; i < cloudCount; i++)
        {
            GenerateRandomSpawnLocation();
        }

    }
    public void GenerateRandomSpawnLocation()
    {
        randomPlaceInBounds = new Vector3(UnityEngine.Random.Range(minBoundingArea.x, maxBoundingArea.x), UnityEngine.Random.Range(minBoundingArea.y, maxBoundingArea.y), UnityEngine.Random.Range(minBoundingArea.z, maxBoundingArea.z));
        CreateOneCloud(randomPlaceInBounds);
    }

    public void CreateOneCloud(Vector3 newCloudSpawnLocation)
    {
        Instantiate(cloudBeingSpawned, newCloudSpawnLocation, Quaternion.Euler(UnityEngine.Random.value, UnityEngine.Random.value, 180f));
    }


    void Update()
    {
        
    }
}
