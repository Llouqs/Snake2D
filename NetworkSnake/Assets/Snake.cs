using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public Sprite headSprite;
    public Sprite bendSprite;
    public Sprite bodySprite;
    public Sprite tailSprite;
    public float sizeSnake;
    private bool up, left, right, down;
    private Direction currentDirection;
    private float _frameRateTime = 0.7f;
    private float _previousTime;
    public GameObject head;
    public List<GameObject> allBody;

    public enum Direction
    {
        up, left, right, down
    }

    public void SetHead(float size)
    {
        sizeSnake = size;
        head = new GameObject("SnakeHead");
        var headRenderer = head.AddComponent<SpriteRenderer>();
        headRenderer.sprite = headSprite;
        headRenderer.sortingOrder = 2;
        head.transform.localScale = new Vector3(sizeSnake, sizeSnake, 1);
        allBody.Add(head);
        head.transform.position = transform.position;
        head.transform.parent = transform;      
    }
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
            case Direction.up:
                transform.position += new Vector3(0, sizeSnake, 0);
                head.transform.localRotation = Quaternion.Euler(0,0,90);
                break;
            case Direction.left:
                transform.position -= new Vector3(sizeSnake, 0, 0);
                head.transform.localRotation = Quaternion.Euler(0,0,180);
                break;
            case Direction.right:
                transform.position += new Vector3(sizeSnake, 0, 0);
                head.transform.localRotation = Quaternion.Euler(0,0,0);
                break;
            case Direction.down:
                transform.position -= new Vector3(0, sizeSnake, 0);
                head.transform.localRotation = Quaternion.Euler(0,0,-90);
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
