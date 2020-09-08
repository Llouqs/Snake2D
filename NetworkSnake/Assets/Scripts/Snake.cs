using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Snake : MonoBehaviour
{
    public float sizeSnake;
    private bool up, left, right, down;
    private Direction _inputDirection;
    private Direction _currentDirection;
    public Transform snakeHead;
    public Transform snakeTail;
    public List<Transform> snakeCircles = new List<Transform>();
    public List<Vector2> positions = new List<Vector2>();
    public float speed;
    private const float FrameRateTime = 0.15f;
    private float _previousTime;

    private enum Direction
    {
        Up, Left, Right, Down
    }
    public void AddCircle()
    {
        var circle = Instantiate(snakeTail, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeCircles.Add(circle);
        positions.Add(circle.position);
        circle.name = "SnakeTail " + snakeCircles.Count;
    }
    public void SetHead(float size)
    {
        sizeSnake = size;
        snakeHead.localScale = new Vector3(sizeSnake, sizeSnake, 1);
        snakeHead = Instantiate(snakeHead, transform.position, Quaternion.identity, transform);
        snakeHead.name = "SnakeHead 1";
        snakeCircles.Add(snakeHead);
        positions.Add(snakeHead.position);
        snakeTail.localScale = new Vector3(sizeSnake, sizeSnake, 1);
        AddCircle();
        AddCircle();
    }

    private void Update()
    {
        GetInput();
        SetPlayerDirection();
        if (Time.time - _previousTime > FrameRateTime)
        {
            var previousPosition = positions[0];
            MovePlayer();
            snakeCircles[0].position = positions[0];
            _previousTime = Time.time;
            for (var i = 1; i < snakeCircles.Count; i++)
            {
                var tmp = positions[i];
                positions[i] = previousPosition;
                previousPosition = tmp;
                snakeCircles[i].position = positions[i];
            }
        }
    }

    private void SetPlayerDirection()
    {
        if (up)
        {
            _inputDirection = Direction.Up;
        }
        else if(left)
        {
            _inputDirection = Direction.Left;
        }
        else if(right)
        {
            _inputDirection = Direction.Right;
        }
        else if(down)
        {
            _inputDirection = Direction.Down;
        }
    }

    private void MovePlayer()
    {
        switch (_inputDirection)
        {
            case Direction.Up:
                positions[0] += new Vector2(0, sizeSnake);
                _currentDirection = Direction.Up;
                break;
            case Direction.Left:
                positions[0] -= new Vector2(sizeSnake, 0);
                _currentDirection = Direction.Left;
                break;
            case Direction.Right:
                positions[0] += new Vector2(sizeSnake, 0);
                _currentDirection = Direction.Right;
                break;
            case Direction.Down:
                positions[0] -= new Vector2(0, sizeSnake);
                _currentDirection = Direction.Down;
                break;
            default:
                break;
        }
    }

    private void GetInput()
    { 
        if(_currentDirection!=Direction.Down)
            up = Input.GetKeyDown(KeyCode.UpArrow);
        if(_currentDirection!=Direction.Right)
            left = Input.GetKeyDown(KeyCode.LeftArrow);
        if(_currentDirection!=Direction.Left)
            right = Input.GetKeyDown(KeyCode.RightArrow);
        if(_currentDirection!=Direction.Up)
            down = Input.GetKeyDown(KeyCode.DownArrow);
    }
}
