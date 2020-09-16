using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    public GameObject playerPref;
    public Vector2 applePosition;
    public Sprite appleSprite;
    public GameObject apple;
    public List<GameObject> players;
    public List<Snake> snakes;
    public List<Transform> colors;
    public int maxPlayers;

    private void Start()
    {
        cellCount = Convert.ToInt32(PlayerPrefs.GetString("gridSize"));
        CreateGrid();
        maxPlayers = Convert.ToInt32(PlayerPrefs.GetString("maxPlayersNumber"));
        for (var i = 0; i < maxPlayers; i++)
        {
            CreatePlayer();
        }
        CreateApple();
    }

    private void CreatePlayer()
    {
        var i = players.Count;
        players.Add(Instantiate(playerPref, gridMaker.grid[(int)(i*(cellCount/4) + 2), (int)cellCount / 2].transform.position + new Vector3(0,0,1), Quaternion.identity));
        snakes.Add(players[i].GetComponent<Snake>());
        snakes[i].snakeHead = colors[i * 2];
        snakes[i].snakeTail = colors[i * 2 + 1];
        snakes[i].SetHead(gridMaker.cellSize/25);
        snakes[i].SetGridBorders(sizeSideGrid);
        players[i].transform.parent = transform;
        players[i].name = "Player" + (i + 1);
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
        foreach (var snake in snakes.Where(snake => ((int) snake.positions[0].x) == (int) applePosition.x &&
                                                    ((int) snake.positions[0].y) == (int) applePosition.y))
        {
            snake.AddCircle();
            PlaceApple();
        }
    }
}
