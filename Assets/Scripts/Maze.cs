using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
// using UnityEngine.SceneManagement;

public class Maze : MonoBehaviour
{
    public static Maze Ins;

    private void Awake()
    {
        Ins = this;
        walls = new List<List<Wall>>();
        // walls.Clear();
        Init();
        // SceneManager.sceneLoaded += InitMaze;
    }

    public int width { get { return 10; } }
    public int height { get { return 13; } }
    [SerializeField] private GameObject _prefabCell;
    [SerializeField] private GameObject _prefabWall;
    [SerializeField] private Transform cellGrid;
    [SerializeField] private Transform horizontalWallGrid;
    [SerializeField] private Transform verticalWallGrid;
    private List<List<Wall>> walls;
    private bool isDisableLayout = false;

    // private void InitMaze(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.buildIndex == 1)
    //     {
    //         walls.Clear();
    //         Init();
    //     }
    // }

    private void Init()
    {
        GameObject tmp;
        List<Wall> horizontalLst;
        List<Wall> verticalLst;
        for (int i = 0; i < height; i++)
        {
            horizontalLst = new List<Wall>();
            verticalLst = new List<Wall>();

            for (int j = 0; j < width; j++)
            {
                tmp = Instantiate(_prefabCell, cellGrid);

                tmp = Instantiate(_prefabWall, verticalWallGrid);
                verticalLst.Add(tmp.GetComponent<Wall>());

                tmp = Instantiate(_prefabWall, horizontalWallGrid);
                horizontalLst.Add(tmp.GetComponent<Wall>());

            }

            tmp = Instantiate(_prefabWall, verticalWallGrid);
            verticalLst.Add(tmp.GetComponent<Wall>());

            walls.Add(horizontalLst);
            walls.Add(verticalLst);
        }

        horizontalLst = new List<Wall>();
        for (int k = 0; k < 10; k++)
        {
            tmp = Instantiate(_prefabWall, horizontalWallGrid);
            horizontalLst.Add(tmp.GetComponent<Wall>());
        }
        walls.Add(horizontalLst);
    }

    public void Setup(List<bool> seed)
    {
        if (!isDisableLayout)
        {
            DisableGridLayoutGroups();
        }

        int d = 0;
        for (int i = 0; i < walls.Count; i++)
        {
            for (int j = 0; j < walls[i].Count; j++)
            {
                walls[i][j].GetComponent<Wall>().Set(seed[d]);
                d++;
            }
        }
    }

    private void DisableGridLayoutGroups()
    {
        GridLayoutGroup tmp = horizontalWallGrid.GetComponent<GridLayoutGroup>();
        tmp.enabled = false;
        tmp = verticalWallGrid.GetComponent<GridLayoutGroup>();
        tmp.enabled = false;
    }

    public Wall GetWall(int x, int y)
    {
        if (x < 0 || x > width || y < 0 || y > 2 * height)
        {
            return null;
        }
        Wall wall = walls[y][x].GetComponent<Wall>();
        return wall;
    }

    // public void Clear()
    // {
    //     for (int i = 0; i < walls.Count; i++)
    //     {
    //         for (int j = 0; j < walls[i].Count; j++)
    //         {
    //             Destroy(walls[i][j].gameObject);
    //         }
    //     }
    //     walls.Clear();
    //     Debug.Log(walls.Count);
    // }

#if UNITY_EDITOR
    public List<bool> GetData()
    {
        List<bool> result = new List<bool>();
        for (int i = 0; i < walls.Count; i++)
        {
            for (int j = 0; j < walls[i].Count; j++)
            {
                result.Add(walls[i][j].GetStatus());
            }
        }
        return result;
    }

    public void ResetMaze()
    {
        for (int i = 0; i < walls.Count; i++)
        {
            for (int j = 0; j < walls[i].Count; j++)
            {
                walls[i][j].Set(false);
            }
        }
    }
#endif

}
