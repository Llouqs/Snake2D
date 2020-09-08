using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Snake : MonoBehaviour
{
    public float sizeSnake;
    private bool up, left, right, down;
    private Direction currentDirection;
    public Transform snakeHead;
    public Transform snakeTail;
    public List<Transform> snakeCircles = new List<Transform>();
    public List<Vector2> positions = new List<Vector2>();
    //private Rigidbody2D componentRigidbody;
    public float speed;
    private readonly float _frameRateTime = 0.5f;
    private float _previousTime;

    public enum Direction
    {
        up, left, right, down
    }
    public void AddCircle()
    {
        var circle = Instantiate(snakeTail, positions[positions.Count - 1], Quaternion.identity, transform);
        snakeCircles.Add(circle);
        positions.Add(circle.position);
    }
    public void SetHead(float size)
    {
        //componentRigidbody = GetComponent<Rigidbody2D>();
        sizeSnake = size;
        snakeHead = Instantiate(snakeHead, transform.position, Quaternion.identity, transform);
        snakeHead.localScale = new Vector3(sizeSnake, sizeSnake, 1);
        snakeCircles.Add(snakeHead);
        positions.Add(snakeHead.position);
        
        snakeTail = Instantiate(snakeTail, transform.position, Quaternion.identity, transform);
        snakeTail.localScale = new Vector3(sizeSnake, sizeSnake, 1);
        snakeCircles.Add(snakeTail);
        positions.Add(snakeTail.position);
        
        AddCircle();
        AddCircle();
        AddCircle();
        AddCircle();
        AddCircle();
    }
    void Update()
    {
        // var distance = ((Vector2) snakeHead.position - positions[0]).magnitude;
        // if (distance > sizeSnake)
        // {
        //     var direction = ((Vector2) snakeHead.position - positions[0]).normalized;
        //     positions.Insert(0, positions[0] + direction * sizeSnake);
        //     positions.RemoveAt(positions.Count-1);
        //     distance -= sizeSnake;
        // }


        GetInput();
        SetPlayerDirection();
        if (Time.time - _previousTime > _frameRateTime)
        {
            var previousPosition = positions[0];
            SetPlayerDirection();
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


    private bool IsGameOver()
    {
        throw new NotImplementedException();
    }

    void SetPlayerDirection()
    {
        if (up && currentDirection!=Direction.down)
        {
            currentDirection = Direction.up;
        }
        else if(left && currentDirection!=Direction.right)
        {
            currentDirection = Direction.left;
        }
        else if(right && currentDirection!=Direction.left)
        {
            currentDirection = Direction.right;
        }
        else if(down && currentDirection!=Direction.up)
        {
            currentDirection = Direction.down;
        }
    }
    
    void MovePlayer()
    {
        switch (currentDirection)
        {
            /*case Direction.up:
                componentRigidbody.velocity = new Vector3(0, sizeSnake, 0)*speed;
                break;
            case Direction.left:
                componentRigidbody.velocity = new Vector3(-sizeSnake, 0, 0)*speed;
                break;
            case Direction.right:
                componentRigidbody.velocity = new Vector3(sizeSnake, 0, 0)*speed;
                break;
            case Direction.down:
                componentRigidbody.velocity = new Vector3(0, -sizeSnake, 0)*speed;
                break;
            default:
                break;
            case Direction.up:
                snakeHead.position += new Vector3(0, sizeSnake, 0);
                break;
            case Direction.left:
                snakeHead.position -= new Vector3(sizeSnake, 0, 0);
                break;
            case Direction.right:
                snakeHead.position += new Vector3(sizeSnake, 0, 0);
                break;
            case Direction.down:
                snakeHead.position -= new Vector3(0, sizeSnake, 0);
                break;
            default:
                break;*/
            case Direction.up:
                positions[0] += new Vector2(0, sizeSnake);
                break;
            case Direction.left:
                positions[0] -= new Vector2(sizeSnake, 0);
                break;
            case Direction.right:
                positions[0] += new Vector2(sizeSnake, 0);
                break;
            case Direction.down:
                positions[0] -= new Vector2(0, sizeSnake);
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
