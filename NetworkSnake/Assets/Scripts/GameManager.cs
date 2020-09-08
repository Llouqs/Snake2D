using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public int cellCount;
    public float sizeSideGrid;
    public GameObject firstCellImage;
    public GameObject secondCellImage;
    public GameObject gameGrid;
    public GridMaker gridMaker;
    public GameObject player1;
    public Sprite appleSprite;
    public GameObject apple;
    
    void Start()
    {
        CreateGrid();
        player1 = Instantiate(player1, gridMaker.grid[cellCount / 2, cellCount / 2].transform.position + new Vector3(0,0,1), Quaternion.identity);
        player1.GetComponent<Snake>().SetHead(gridMaker.cellSize/25);
        player1.transform.parent = transform;
        CreateApple();
    }
    void CreateGrid()
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
    }
    
    void CreateApple()
    {
        apple = new GameObject("Apple");
        var appleRenderer = apple.AddComponent<SpriteRenderer>();
        appleRenderer.sprite = appleSprite;
        appleRenderer.sortingOrder = 1;
        apple.transform.localScale = new Vector3(gridMaker.cellSize/25, gridMaker.cellSize/25, 1);
        apple.transform.parent = transform;
        PlaceApple();
    }

    void PlaceApple()
    {
        var randomPosX = Random.Range(0, cellCount);
        var randomPosY = Random.Range(0, cellCount);
        apple.transform.position = gridMaker.grid[randomPosX, randomPosY].nodePosition;
    }
    void Update()
    {
        
    }
}
