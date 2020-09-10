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
    public Snake snake1;
    public Vector2 applePosition;
    public Sprite appleSprite;
    public GameObject apple;

    private void Start()
    {
        CreateGrid();
        player1 = Instantiate(player1, gridMaker.grid[cellCount / 2, cellCount / 2].transform.position + new Vector3(0,0,1), Quaternion.identity);
        snake1 = player1.GetComponent<Snake>();
        snake1.SetHead(gridMaker.cellSize/25);
        snake1.SetGridBorders(sizeSideGrid);
        player1.transform.parent = transform;
        player1.name = "Player 1";
        CreateApple();
    }

    private void CreateGrid()
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

    private void CreateApple()
    {
        apple = new GameObject("Apple");
        var appleRenderer = apple.AddComponent<SpriteRenderer>();
        appleRenderer.sprite = appleSprite;
        appleRenderer.sortingOrder = 1;
        apple.transform.localScale = new Vector3(gridMaker.cellSize/25, gridMaker.cellSize/25, 1);
        apple.transform.parent = transform;
        PlaceApple();
    }

    private void PlaceApple()
    {
        var randomPosX = Random.Range(0, cellCount);
        var randomPosY = Random.Range(0, cellCount);
        applePosition = gridMaker.grid[randomPosX, randomPosY].nodePosition;
        apple.transform.position = applePosition;
    }

    private void Update()
    {
        if (((int)snake1.positions[0].x) == (int)applePosition.x && ((int)snake1.positions[0].y) == (int)applePosition.y)
        {
            snake1.AddCircle();
            PlaceApple();
        }
        /*foreach (var tmp in snake1.positions)
        {
            
        }
        */
    }
}
