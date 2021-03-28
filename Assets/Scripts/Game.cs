using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Ins;
    [SerializeField] private Maze maze;
    [SerializeField] private Bug bug;
    [SerializeField] private Transform gate;
    private int gatePosX;
    private int gatePosY;
    [SerializeField] private GameObject prefabLine;
    [SerializeField] private Transform lines;
    private int stageID;
    private List<Cell> path;

    private void Awake()
    {
        Ins = this;
    }

    public void OpenGame(int id, List<bool> seed)
    {
        stageID = id;
        maze.Setup(seed);
        SetGatePosition();
        bug.Refresh();
    }

    public void CloseGame()
    {
        ClearLines();
        Cell.ResetAll();
        StageManager.Ins.OpenStageMenu();
    }

    public void FindPath()
    {
        path = Cell.GetCell(gatePosX, gatePosY).FindPath();
        for (int i = 0; i < path.Count - 1; i++)
        {
            DrawLine(path[i], path[i + 1]);
        }
    }

    public void MoveBug()
    {
        bug.Go(path);
    }

    public void CheckWin()
    {
        StartCoroutine(Win());
        StageManager.Ins.UpdateStage(stageID, 2);
    }

    private void SetGatePosition()
    {
        gatePosX = Random.Range(4, Maze.Ins.width);
        gatePosY = Random.Range(0, Maze.Ins.height - 4);
        gate.position = Cell.GetCell(gatePosX, gatePosY).transform.position;
    }

    private void DrawLine(Cell start, Cell end)
    {
        Transform line = Instantiate(prefabLine, lines).transform;
        line.position = (start.transform.position + end.transform.position) / 2;

        if (start.transform.localPosition.x != end.transform.localPosition.x)
        {
            line.rotation = Quaternion.Euler(Vector3.forward * 90f);
        }
    }

    IEnumerator Win()
    {
        yield return new WaitForSeconds(1.5f);
        Debug.Log("win");
        CloseGame();
    }

    private void ClearLines()
    {
        foreach (Transform child in lines)
        {
            Destroy(child.gameObject);
        }
    }

}
