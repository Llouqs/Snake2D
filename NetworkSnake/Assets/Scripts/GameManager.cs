using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int cellCount;
    public float sizeSideGrid;
    public GameObject firstCellImage;
    public GameObject secondCellImage;
    public GameObject gameGrid;
    public GridMaker gridMaker;
    public GameObject player1;
    public 
    void Start()
    {
        gameGrid = new GameObject("GameGrid");
        gameGrid.transform.parent = transform;
        gameGrid.transform.position = transform.position;
        gridMaker = gameGrid.AddComponent<GridMaker>();
        gridMaker.cellCount = cellCount;
        gridMaker.sizeSideGrid = sizeSideGrid;
        gridMaker.firstCellImage = firstCellImage;
        gridMaker.secondCellImage = secondCellImage;
        gridMaker.SetGrid();
        player1.GetComponent<Snake>().SetSize(gridMaker.cellSize/25);
        player1 = Instantiate(player1, gridMaker.grid[cellCount / 2, cellCount / 2].transform.position + new Vector3(0,0,1), Quaternion.identity);
        player1.transform.parent = transform;
        
    }

    void Update()
    {
        
    }
}
