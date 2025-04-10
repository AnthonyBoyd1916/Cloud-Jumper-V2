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
        float xRotRand = UnityEngine.Random.Range(0f,360f);
        //float yRotRandB = UnityEngine.Random.rotation.y;

        //int xRotRandA = Convert.ToInt32((float)xRotRandB);
        //int yRotRandA = Convert.ToInt32((float)yRotRandB);
        if (SpawnChance == 1)
        {
            GameObject cloud1Rot = Instantiate(cloud1BeingSpawned, newCloudSpawnLocation, Quaternion.identity);
            //cloud1Rot.transform.rotation = Quaternion.Euler(xRotRand, 0, 0);
        }
        else if (SpawnChance == 2)
        {
            GameObject cloud2Rot = Instantiate(cloud2BeingSpawned, newCloudSpawnLocation, Quaternion.identity);
            //cloud2Rot.transform.rotation = Quaternion.Euler(xRotRand, 0, 0);
        }
        else if (SpawnChance == 3)
        {
            GameObject cloud3Rot = Instantiate(cloud3BeingSpawned, newCloudSpawnLocation, Quaternion.identity);
            //cloud3Rot.transform.rotation = Quaternion.Euler(xRotRand, 0, 0);
        }
    }
}
