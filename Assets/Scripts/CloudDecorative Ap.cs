using System;
using Unity.VisualScripting;
using UnityEngine;

public class CloudDecorativeAp : MonoBehaviour
{
    public GameObject cloudline, cloud1BeingSpawned, cloud2BeingSpawned, cloud3BeingSpawned;
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
        for (int i = 0; i < cloudCount; i++)
        {
            GenerateRandomSpawnLocation();
        }

    }
    public void GenerateRandomSpawnLocation()
    {
        int SpawnChance = Convert.ToInt32(UnityEngine.Random.Range(1f, 3f));
        if (SpawnChance > 2)
        {
            randomPlaceInBounds = new Vector3(UnityEngine.Random.Range(minBoundingArea.x, maxBoundingArea.x), UnityEngine.Random.Range(minBoundingArea.y, maxBoundingArea.y), UnityEngine.Random.Range(minBoundingArea.z, maxBoundingArea.z));
            CreateOneCloud(randomPlaceInBounds);
        }

    }

    public void CreateOneCloud(Vector3 newCloudSpawnLocation)
    {
        int SpawnChance = Convert.ToInt32(UnityEngine.Random.Range(1f, 3f));

        Vector3 rotRand = new Vector3(UnityEngine.Random.rotation.x, UnityEngine.Random.rotation.x, 180);
        //float xRotRandB = UnityEngine.Random.rotation.x;
        //float yRotRandB = UnityEngine.Random.rotation.y;

        //int xRotRandA = Convert.ToInt32((float)xRotRandB);
        //int yRotRandA = Convert.ToInt32((float)yRotRandB);
        if (SpawnChance == 1)
        {
            Instantiate(cloud1BeingSpawned, newCloudSpawnLocation, Quaternion.Euler(rotRand));
        }
        else if (SpawnChance == 2)
        {
            Instantiate(cloud1BeingSpawned, newCloudSpawnLocation, Quaternion.Euler(rotRand));
        }
        else if (SpawnChance == 3)
        {
            Instantiate(cloud1BeingSpawned, newCloudSpawnLocation, Quaternion.Euler(rotRand));
        }
    }
}
