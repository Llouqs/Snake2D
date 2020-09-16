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
    private const float FrameRateTime = 0.25f;
    private float _previousTime;
    private float _gridBorder;
 
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

    public void SetGridBorders(float size)
    {
        _gridBorder = size;
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
        _inputDirection = Direction.Right;
        AddCircle();
        AddCircle();
    }

    private void Update()
    {
        if (IsGameOver())
        {
            SnakeRespawn();
        }
        Rotation();
        if (Time.time - _previousTime > FrameRateTime)
        {
            var previousPosition = positions[0];
            MovePlayer();
            snakeCircles[0].position = positions[0];

            for (var i = 1; i < snakeCircles.Count; i++)
            {
                var tmp = positions[i];
                positions[i] = previousPosition;
                previousPosition = tmp;
                snakeCircles[i].position = positions[i];
            }

            _previousTime = Time.time;
        }
    }

    bool IsGameOver()
    {
        if (SnakeBiteItself()||SnakeBiteBorders())
        {
            return true;
        }
        return false;
    }

    bool SnakeBiteBorders()
    {
        if (positions[0].x > _gridBorder/2 || positions[0].y > _gridBorder/2 || positions[0].x < -_gridBorder/2 || positions[0].y < -_gridBorder/2)
        {
            return true;
        }
        return false;
    }

    bool SnakeBiteItself()
    {
        for (var i = 1; i < positions.Count; i++)
        {
            if (positions[0].Equals(positions[i]))
                return true;
        }
        return false;
    }
    void SnakeRespawn()
    {
        for (var i = 1; i<transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        snakeCircles.Clear();
        positions.Clear();
        snakeHead.position = transform.position;
        snakeCircles.Add(snakeHead);
        positions.Add(snakeHead.position);
        _inputDirection = Direction.Left;
        AddCircle();
        AddCircle();
    }

    private void MovePlayer()
    {
        switch (_inputDirection)
            {
                case Direction.Up:
                    positions[0] += new Vector2(0, sizeSnake);
                    break;
                case Direction.Left:
                    positions[0] -= new Vector2(sizeSnake, 0);
                    break;
                case Direction.Right:
                    positions[0] += new Vector2(sizeSnake, 0);
                    break;
                case Direction.Down:
                    positions[0] -= new Vector2(0, sizeSnake);
                    break;
                default:
                    break;
            }

        _currentDirection = _inputDirection;
    }
    
    private void Rotation() {
        switch (_inputDirection) {
                case Direction.Left:
                case Direction.Right:
                    if (Input.GetKey(KeyCode.DownArrow)&&_currentDirection!=Direction.Up)
                        _inputDirection = Direction.Down;
                    else if (Input.GetKey(KeyCode.UpArrow)&&_currentDirection!=Direction.Down)
                        _inputDirection = Direction.Up;
                    break;
                case Direction.Up:
                case Direction.Down:
                    if (Input.GetKey(KeyCode.LeftArrow)&&_currentDirection!=Direction.Right)
                        _inputDirection = Direction.Left;
                    else if (Input.GetKey(KeyCode.RightArrow)&&_currentDirection!=Direction.Left)
                        _inputDirection = Direction.Right;
                    break;
                default:
                    break;
            }
    }
}
