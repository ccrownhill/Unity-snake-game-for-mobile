using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int len = 1;

    public float moveSpeed = 5f;
    public float maxSpeed = 9f;

    public float turnThreshold = 0.1f;

    public Grid grid;

    private Transform head;

    Vector3 movement;
    private Vector3Int prevGrid = new Vector3Int(0,0,0);

    private Queue moveQueue = new Queue();
    private Vector3 prevPos = new Vector3();
    public GameObject bodyPrefab;

    private SceneLoader loader;

    public bool gameover = false;

    private AudioSource audio;
    public AudioClip deathSound;
    public AudioClip clickSound;

    public float waitTime = 0.1f;
    private List<Transform> body = new List<Transform>();

    void Start()
    {
        audio = GetComponent<AudioSource>();
        loader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        moveSpeed *= grid.cellSize.x;
        head = GetComponentsInChildren<Transform>()[1];
        head.position = grid.GetCellCenterLocal(grid.LocalToCell(head.position));
        prevGrid = grid.LocalToCell(head.position);
        StartCoroutine(Mover());
    }

    private IEnumerator Mover()
    {
        while (!gameover)
        {
            prevGrid = grid.LocalToCell(head.position);
            handleInput();
            head.Translate(movement * grid.cellSize.x);
            handleSnake();
            handleCollisionWithTail();
            destroyOld();
            detectOutOfBounds();
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void handleInput()
    {
        if (moveQueue.Count > 0)
        {
            char dir = (char)moveQueue.Dequeue();
            if (dir == 'u')
            {
                movement.x = 0;
                movement.y = 1;
            }
            if (dir == 'd')
            {
                movement.x = 0;
                movement.y = -1;
            }
            if (dir == 'l')
            {
                movement.x = -1;
                movement.y = 0;
            }
            if (dir == 'r')
            {
                movement.x = 1;
                movement.y = 0;
            }
        }
    }

    public void MoveUp()
    {
        if (movement.y == 1) { return;  }
        moveQueue.Enqueue('u');
        audio.PlayOneShot(clickSound);
    }

    public void MoveDown()
    {
        if (movement.y == -1) { return; }
        moveQueue.Enqueue('d');
        audio.PlayOneShot(clickSound);
    }

    public void MoveLeft()
    {
        if (movement.x == -1) { return; }
        moveQueue.Enqueue('l');
        audio.PlayOneShot(clickSound);
    }

    public void MoveRight()
    {
        if (movement.x == 1) { return; }
        moveQueue.Enqueue('r');
        audio.PlayOneShot(clickSound);
    }

    private void handleSnake()
    {
        if (grid.LocalToCell(head.position) == prevGrid) { return;  }
        prevPos = grid.GetCellCenterLocal(prevGrid);
        body.Add(Instantiate(bodyPrefab, prevPos, Quaternion.identity, transform).transform);
    }

    private void destroyOld()
    {
        if (body.Count > len - 1)
        {
            Destroy(body[0].gameObject);
            body.RemoveAt(0);
        }
    }

    private void handleCollisionWithTail()
    {
        for (int i = 0; i < body.Count; i++)
        {
            if (head.position == body[i].position)
            {
                audio.PlayOneShot(deathSound);
                loader.gameOver();
            }
        }
    }

    private void detectOutOfBounds()
    {
        Vector3Int gridPos = grid.LocalToCell(head.position);
        if (gridPos.x < -21 || gridPos.x > 3 || gridPos.y < -12 || gridPos.y > 11)
        {
            audio.PlayOneShot(deathSound);
            loader.gameOver();
        }
    }
}
