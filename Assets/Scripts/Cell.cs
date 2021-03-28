using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public static List<List<Cell>> cells;
    private int x;
    private int y;
    private bool visited;

    public static void ResetAll()
    {
        for (int i = 0; i < cells.Count; i++)
        {
            for (int j = 0; j < cells[i].Count; j++)
            {
                cells[i][j].visited = false;
            }
        }
    }

    private void Awake()
    {
        visited = false;

        if (cells == null)
        {
            cells = new List<List<Cell>>();
            cells.Add(new List<Cell>());
        }

        y = cells.Count - 1;
        if (cells[y].Count > Maze.Ins.width - 1)
        {
            cells.Add(new List<Cell>());
            y++;
        }
        x = cells[y].Count;

        cells[y].Add(this);

    }

    public void BreakWall()
    {
        visited = true;

        int[] direction = { 0, 1, 2, 3 };
        direction.Shuffle();

        Cell tmp;
        for (int i = 0; i < 4; i++)
        {
            tmp = GetNeighbor(direction[i]);
            if (tmp != null && !tmp.visited)
            {
                tmp.BreakWall();
                GetWall(direction[i]).Set(true);
            }
        }
    }

    public List<Cell> FindPath()
    {
        List<Cell> result = new List<Cell>();
        Stack<Cell> path = new Stack<Cell>();

        if (GotoNeighbor(path))
        {
            while (path.Count > 0)
            {
                result.Add(path.Pop());
            }
        }

        return result;
    }

    public bool GotoNeighbor(Stack<Cell> path)
    {
        if (x == 0 && y == Maze.Ins.height - 1)
        {
            path.Push(this);
            return true;
        }

        path.Push(this);
        visited = true;

        for (int i = 0; i < 4; i++)
        {
            if (GetWall(i).GetStatus() && !GetNeighbor(i).visited)
            {
                if (GetNeighbor(i).GotoNeighbor(path))
                {
                    return true;
                }
            }
        }
        path.Pop();
        return false;
    }

    public static Cell GetCell(int x, int y)
    {
        if (x < 0 || x > Maze.Ins.width - 1 || y < 0 || y > Maze.Ins.height - 1)
        {
            return null;
        }
        return cells[y][x];
    }

    public Cell GetNeighbor(int direction)
    {
        switch (direction)
        {
            case 0:
                return GetCell(x, y + 1);
            case 1:
                return GetCell(x + 1, y);
            case 2:
                return GetCell(x - 1, y);
            case 3:
                return GetCell(x, y - 1);
            default:
                Debug.Log("invalid direction");
                return null;
        }
    }

    public Wall GetWall(int direction)
    {
        switch (direction)
        {
            case 0:
                return Maze.Ins.GetWall(x, 2 * (y + 1));
            case 1:
                return Maze.Ins.GetWall(x + 1, 2 * y + 1);
            case 2:
                return Maze.Ins.GetWall(x, 2 * y + 1);
            case 3:
                return Maze.Ins.GetWall(x, 2 * y);
            default:
                Debug.Log("invalid direction");
                return null;
        }
    }

    public void ShowPosition()
    {
        Debug.Log(new Vector2(x, y));
    }

}
