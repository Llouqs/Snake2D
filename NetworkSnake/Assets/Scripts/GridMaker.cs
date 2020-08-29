using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GridMaker : MonoBehaviour
{
    public int cellCount;
    public float sizeSideGrid;
    public GameObject firstCellImage;
    public GameObject secondCellImage;
    public float cellSize;
    [SerializeField] private int cellSquare;
    public Vector2 firstCellPosition;
    public GridNode[,] grid;

    public void SetGrid()
    {
        grid = new GridNode[cellCount, cellCount];
        cellSquare = cellCount * cellCount;
        firstCellPosition = new Vector2(sizeSideGrid / (cellCount * 2), sizeSideGrid / (cellCount * 2));
        cellSize = (sizeSideGrid / cellCount) * 25;
        firstCellImage.transform.localScale = new Vector3(cellSize, cellSize, 1);
        secondCellImage.transform.localScale = new Vector3(cellSize, cellSize, 1);
        var tmpPosition = new Vector2(transform.position.x, transform.position.y);
        var flag = true;
        for (var i = 0; i < cellCount; i++)
        {
            for (var j = 0; j < cellCount; j++)
            {
                GameObject cell;
                var tmpVector = tmpPosition + firstCellPosition + new Vector2(firstCellPosition.x * 2 * j, firstCellPosition.y * 2 * i);
                if (flag)
                {
                    
                    cell = Instantiate(firstCellImage, tmpVector, Quaternion.identity);
                    flag = false;
                }
                else
                {
                    cell = Instantiate(secondCellImage, tmpVector, Quaternion.identity);
                    flag = true;
                }
                cell.transform.parent = transform;
                cell.name = "( Cell: " + i + ", " + j + " )";
                var n = cell.AddComponent<GridNode>();
                n.nodePosition = tmpVector;
                grid[i, j] = n;
            }
            if (cellCount % 2 == 0)
                flag = !flag;
        }
    }
}
