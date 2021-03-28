using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    private List<Vector3> path;
    [Range(0, 10f)]
    public float speed = 2f;
    [SerializeField] private Transform gate;

    private void Start()
    {
        path = new List<Vector3>();
        path.Add(Vector3.zero);
    }
    public void Refresh()
    {
        transform.position = Cell.GetCell(0, Maze.Ins.height - 1).transform.position;
    }

    public void Go(List<Cell> path)
    {
        foreach (Cell c in path)
        {
            this.path.Add(c.transform.position);
        }
    }

    private void Update()
    {
        if (path.Count > 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[1], speed * Time.deltaTime);
            Vector3 direction = path[1] - transform.position;
            transform.rotation = Quaternion.Euler(Vector3.forward * (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90));
            if (transform.position == path[1])
            {
                path.RemoveAt(0);
                if (transform.position == gate.position)
                {
                    Game.Ins.CheckWin();
                }
            }
        }
    }

    public void Reset()
    {
        path.Clear();
        path.Add(Vector3.zero);
    }
}
