using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    public MazeData mazeData;

    public void GenerateAllLevel()
    {
        for (int i = 0; i < 999; i++)
        {
            GenerateMaze();
            SaveMaze();
            ResetMaze();
        }
    }

    public void GenerateMaze()
    {
        Cell.GetCell(0, 0).BreakWall();
    }

    public void ResetMaze()
    {
        Maze.Ins.ResetMaze();
        Cell.ResetAll();
    }

    public void SaveMaze()
    {
        int levelSeq = mazeData.data.Count;
        LevelData levelData = new LevelData { id = levelSeq, star = -1, seed = Maze.Ins.GetData() };
        mazeData.data.Add(levelData);
    }
#endif
}
