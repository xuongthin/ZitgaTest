using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerEnterHandler
{
    public static List<List<Cell>> cells;
    private int x;
    private int y;
    private bool visited;
    public static Cell previousCell;

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
        }

        if (cells.Count == 0)
        {
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

    public List<Cell> FindPath(Cell bug)
    {
        List<Cell> result = new List<Cell>();
        Stack<Cell> path = new Stack<Cell>();

        if (GotoNeighbor(path, bug))
        {
            while (path.Count > 0)
            {
                result.Add(path.Pop());
            }
        }
        previousCell = this;
        return result;
    }

    public bool GotoNeighbor(Stack<Cell> path, Cell bug)
    {
        // if (x == 0 && y == Maze.Ins.height - 1)
        if (bug == this)
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
                if (GetNeighbor(i).GotoNeighbor(path, bug))
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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (previousCell.IsConnectable(this))
        {
            previousCell = this;
            Game.Ins.AddCell(this);
            Debug.Log(new Vector2(x, y));
        }
    }

    private bool IsConnectable(Cell cell)
    {
        if ((cell.x == previousCell.x) && (cell.y == previousCell.y + 1))
        {
            return GetWall(0).isOpen;
        }
        if ((cell.x == previousCell.x + 1) && (cell.y == previousCell.y))
        {
            return GetWall(1).isOpen;
        }
        if ((cell.x == previousCell.x - 1) && (cell.y == previousCell.y))
        {
            return GetWall(2).isOpen;
        }
        if ((cell.x == previousCell.x) && (cell.y == previousCell.y - 1))
        {
            return GetWall(3).isOpen;
        }
        return false;
    }

#if UNITY_EDITOR
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
#endif

}
