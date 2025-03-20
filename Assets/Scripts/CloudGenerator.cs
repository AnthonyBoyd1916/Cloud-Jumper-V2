using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CloudGenerator : MonoBehaviour
{
    public enum Grid
    {
        CLOUD,
        EMPTY
    }

    public Grid[,] cloudGrid;
    public List<CloudWalker> cloudWalkers;
    public GameObject cloudPlane;
    public GameObject generatedCloud;
    //public Transform cloudStorage;

    public int planeWidthX;
    public int planeDepthZ;

    public int numWalkers;
    public int gridCount = default;
    public float percentageClouds;
    public float generationTime;

    private void Start()
    {
        planeWidthX = Convert.ToInt32(cloudPlane.transform.localScale.x);
        planeDepthZ = Convert.ToInt32(cloudPlane.transform.localScale.z);

        InitializeGrid();
    }

    void InitializeGrid()
    {
        cloudGrid = new Grid[planeDepthZ, planeWidthX];

        for(int x = 0; x < cloudGrid.GetLength(0); x++)
        {
            for (int z = 0; z < cloudGrid.GetLength(1); z++)
            {
                cloudGrid[x,z] = Grid.EMPTY;
            }
        }

        cloudWalkers = new List<CloudWalker>();

        Vector3Int planeCentre = new Vector3Int(cloudGrid.GetLength(0)/2, 0, cloudGrid.GetLength(1)/2);
        CloudWalker cloudWalker = new CloudWalker(new Vector3(planeCentre.x, 0, planeCentre.z), GetDirection(),0.5f);

        cloudGrid[planeCentre.x, planeCentre.z] = Grid.CLOUD;

        cloudWalkers.Add(cloudWalker);

        gridCount++;
        Debug.Log("Grid squares counted " + gridCount);
        StartCoroutine(CreateClouds());
    }

    Vector3 GetDirection()
    {
        int choice = Mathf.FloorToInt(UnityEngine.Random.value * 3.99f);

        switch (choice) 
        {
            case 0: return Vector3.right;
            case 1: return Vector3.forward;
            case 2: return Vector3.back;
            case 3: return Vector3.left;
            default: return Vector3.zero;
        }

    }

    IEnumerator CreateClouds(/*CloudWalker cloudWalker*/)
    {
        float currentCloudCoverage = (float)gridCount / (float)cloudGrid.Length;
        Debug.Log("Cloud Coverage " + currentCloudCoverage);
        
        while (currentCloudCoverage < percentageClouds)
        {
            Debug.Log("Within percentage ");
            bool cloudCreated = false;
            foreach (CloudWalker cloudWalker in cloudWalkers)
            {
                Vector3Int curPos = new Vector3Int((int)cloudWalker.position.x,0,(int)cloudWalker.position.z);
                Debug.Log("Walker position found");
                if (cloudGrid[curPos.x,curPos.z] != Grid.CLOUD)
                {
                    Instantiate(generatedCloud,curPos,Quaternion.identity);
                    Debug.Log("Cloud generated");
                    gridCount++;
                    cloudGrid[curPos.x,curPos.z] = Grid.CLOUD;
                    Debug.Log("Grid square set to cloud");
                    cloudCreated = true;
                    currentCloudCoverage = (float)gridCount / (float)cloudGrid.Length;
                    Debug.Log("Cloud Coverage Updated " + currentCloudCoverage);
                }
            }

            ChanceToStopWalker();
            ChanceToRedirectWalker();
            ChanceToCreateWalker();
            UpdateWalkerPosition();

            if (cloudCreated)
            {
                Debug.Log("Clouds generation finished");
                yield return new WaitForSeconds(generationTime);
            }
        }
    }

    void ChanceToStopWalker()
    {
        int updateWalkerCount = cloudWalkers.Count;
        for (int i = 0; i < updateWalkerCount; i++)
        {
            if (UnityEngine.Random.value < cloudWalkers[i].directionChangeChance && cloudWalkers.Count > 1)
            {             
                cloudWalkers.RemoveAt(i);
                Debug.Log("Walker stopped");
                break;
            }
        }
    }

    void ChanceToRedirectWalker()
    {
        for (int i = 0; i < cloudWalkers.Count; i++)
        {
            if (UnityEngine.Random.value < cloudWalkers[i].directionChangeChance)
            {
                
                CloudWalker curWalker = cloudWalkers[i];
                curWalker.direction = GetDirection();
                cloudWalkers[i] = curWalker;
                Debug.Log("Walker changed direction");
            }
        }
    }

    void ChanceToCreateWalker()
    {
        int updateWalkerCount = cloudWalkers.Count;
        for (int i = 0; i < updateWalkerCount; i++)
        {
            if (UnityEngine.Random.value < cloudWalkers[i].directionChangeChance && cloudWalkers.Count < numWalkers)
            {
                Vector3 newDirection = GetDirection();
                Vector3 newPosition = cloudWalkers[i].position;
                
                CloudWalker newWalker = new CloudWalker(newDirection, newPosition, 0.5f);
                cloudWalkers.Add(newWalker);
                Debug.Log("Walker created");
            }
        }
    }

    void UpdateWalkerPosition()
    {
        for(int i = 0;i < cloudWalkers.Count; i++)
        {
            CloudWalker foundWalker = cloudWalkers[i];
            foundWalker.position = foundWalker.direction;
            foundWalker.position.x = Mathf.Clamp(foundWalker.position.x, 1, cloudGrid.GetLength(0) - 2);
            foundWalker.position.z = Mathf.Clamp(foundWalker.position.z, 1, cloudGrid.GetLength(1) - 2);
            cloudWalkers[i] = foundWalker;
            Debug.Log("Walker position updated");
        }
    }
}
