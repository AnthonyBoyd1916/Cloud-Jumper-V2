using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridPointGenerator : MonoBehaviour
{
    public GameObject cube;
    public Grid grid;
    public int gridWidth, gridWidthGaps;
    public int gridDepth, gridDepthGaps;
    public int gridHeight, gridHeightGaps;
    public int desiredCells;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int numberOfCells = gridWidth * gridDepth;

        if (desiredCells > 0)
        {
            for (int i = 1; i < gridWidth; i++)
            {
                for (int j = 1; j < gridDepth; j++)
                {
                    Vector3 currentCellPosition = grid.cellSize.normalized;
                    Vector3 newCellPosition;
                    RandomizedJumpLocation(currentCellPosition);
                    newCellPosition = currentCellPosition;
                    Instantiate(cube, newCellPosition, Quaternion.identity);
                    desiredCells--;
                }
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 RandomizedJumpLocation(Vector3 lastJumpSpot)
    {
        Vector3Int positiveXJump = new Vector3Int(gridWidth, 0, 0);
        Vector3Int negativeXJump = new Vector3Int(-gridWidth, 0, 0);
        Vector3Int positiveZJump = new Vector3Int(0, 0, gridDepth);
        Vector3Int negativeZJump = new Vector3Int(0, 0, -gridDepth);
        Vector3Int positiveYJump = new Vector3Int(0, gridHeight, 0);
        Vector3Int negativeYJump = new Vector3Int(0, -gridHeight, 0);

        int randomNextJump = Mathf.FloorToInt(UnityEngine.Random.value * 5.99f);

        switch (randomNextJump)
        {
            case 0: return lastJumpSpot + positiveXJump;
            case 1: return lastJumpSpot + negativeXJump;
            case 2: return lastJumpSpot + positiveYJump;
            case 3: return lastJumpSpot + negativeYJump;
            case 4: return lastJumpSpot + positiveZJump;
            case 5: return lastJumpSpot + negativeZJump;
            default: return Vector3.zero;
        }
    }
}
