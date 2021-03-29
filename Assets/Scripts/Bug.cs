using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private List<Cell> path;
    [Range(100, 1000f)]
    public float speed = 200f;
    [SerializeField] private Transform gate;
    private bool isPaused = false;

    private void Start()
    {
        path = new List<Cell>();
        path.Add(Cell.GetCell(0, Maze.Ins.height - 1));
    }

    public void SetStatus(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public Cell GetCurrentPosition()
    {
        Stop();
        if (path.Count > 1)
        {
            return path[1];
        }
        else
        {
            return path[0];
        }
    }

    private void Stop()
    {
        for (int i = 2; i < path.Count; i++)
        {
            path.RemoveAt(i);
        }
    }

    public void Go(Cell cell)
    {
        path.Add(cell);
    }

    public void Go(List<Cell> path)
    {
        this.path = path;
    }

    private void Update()
    {
        if (isPaused)
        {
            return;
        }

        if (path.Count > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[1].transform.position, speed * Time.deltaTime);
            Vector3 direction = path[1].transform.position - transform.position;
            transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90));
            if (direction.sqrMagnitude <= 1f)
            {
                path.RemoveAt(0);
                if (Vector3.Distance(transform.position, gate.position) <= 1f)
                {
                    path.Clear();
                    Game.Ins.CheckWin();
                }
            }
        }
    }

    public void Refresh()
    {
        path.Clear();
        path.Add(Cell.GetCell(0, Maze.Ins.height - 1));
        isPaused = false;
        transform.position = path[0].transform.position;
    }
}
