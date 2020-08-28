using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Sprite head;
    public Sprite bend;
    public Sprite body;
    public Sprite tail;
    public List<Sprite> allBody;
    public float sizeSnake;
    private bool up, left, right, down;
    private Direction currentDirection;
    private float _frameRateTime = 0.9f;
    private float _previousTime;

    public enum Direction
    {
        up, left, right, down
    }
    
    void Start()
    {
        allBody.Add(head);
        allBody.Add(body);
        allBody.Add(tail);
    }

    public void SetSize(float size)
    {
        sizeSnake = size;
        transform.localScale = new Vector3(sizeSnake, sizeSnake, 1);
    }
    // Update is called once per frame
    void Update()
    {
        GetInput();
        SetPlayerDirection();
        if (Time.time - _previousTime > _frameRateTime)
        {
 
            MovePlayer();
                //if (IsGameOver())
                //GameOver();
                //else
                //SpawnShape();
            _previousTime = Time.time;
        }
        
    }

    private bool IsGameOver()
    {
        throw new NotImplementedException();
    }

    void SetPlayerDirection()
    {
        if (up)
        {
            currentDirection = Direction.up;
        }
        else if(left)
        {
            currentDirection = Direction.left;
        }
        else if(right)
        {
            currentDirection = Direction.right;
        }
        else if(down)
        {
            currentDirection = Direction.down;
        }
    }

    void MovePlayer()
    {
        switch (currentDirection)
        {
            case Direction.up:
                transform.position += new Vector3(0, sizeSnake, 0);
                break;
            case Direction.left:
                transform.position -= new Vector3(sizeSnake, 0, 0);
                break;
            case Direction.right:
                transform.position += new Vector3(sizeSnake, 0, 0);
                break;
            case Direction.down:
                transform.position -= new Vector3(0, sizeSnake, 0);
                break;
            default:
                break;
        }
    }
    void GetInput()
    {
        up = Input.GetKeyDown(KeyCode.UpArrow);
        left = Input.GetKeyDown(KeyCode.LeftArrow);
        right = Input.GetKeyDown(KeyCode.RightArrow);
        down = Input.GetKeyDown(KeyCode.DownArrow);
    }
}
