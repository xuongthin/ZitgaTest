using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public static Game Ins;
    private int stageID;
    private bool onGame = false;
    [SerializeField] private Maze maze;
    [SerializeField] private Bug bug;
    [SerializeField] private Transform gate;
    private int gatePosX;
    private int gatePosY;
    [SerializeField] private GameObject prefabLine;
    [SerializeField] private Transform lines;
    private List<Cell> path;
    [SerializeField] private GameObject endgameMenu;
    [SerializeField] private Button findPath;
    [SerializeField] private Button autoMove;
    [SerializeField] private Timer timer;

    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(transform.parent.gameObject);
    }

    public void OpenGame(int id, List<bool> seed)
    {
        stageID = id;
        onGame = true;

        maze.Setup(seed);

        bug.Refresh();
        SetGatePosition();

        Cell.previousCell = bug.GetCurrentPosition();

        timer.StartLevel();
    }

    private void SetGatePosition()
    {
        gatePosX = Random.Range(4, Maze.Ins.width);
        gatePosY = Random.Range(0, Maze.Ins.height - 4);
        gate.position = Cell.GetCell(gatePosX, gatePosY).transform.position;
    }

    public void FindPath()
    {
        ClearLines();
        path = Cell.GetCell(gatePosX, gatePosY).FindPath(bug.GetCurrentPosition());
        for (int i = 0; i < path.Count - 1; i++)
        {
            DrawLine(path[i], path[i + 1]);
        }
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

    private void Update()
    {
        if (onGame)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseGame();
            }

            timer.CountDown();
        }
    }

    public void AddCell(Cell newCell)
    {
        bug.Go(newCell);
    }

    public void MoveBug()
    {
        bug.Go(path);
    }

    public void SetStatus(bool isPaused)
    {
        timer.SetStatus(isPaused);
        bug.SetStatus(isPaused);
    }

    public void CheckWin()
    {
        StageManager.Ins.UpdateStage(stageID, timer.GetStar());
        OpenEndgameMenu();
    }

    private void OpenEndgameMenu()
    {
        endgameMenu.SetActive(true);
        timer.SetStatus(true);
    }

    public void Next()
    {
        Cell.ResetAll();

        ClearLines();

        StageManager.Ins.ClickHandle(stageID + 1);

        ResetButton();
    }

    public void ResetLevel()
    {
        SetStatus(false);

        bug.Refresh();

        Cell.ResetAll();
        Cell.previousCell = bug.GetCurrentPosition();

        timer.Refresh();
        ResetButton();

        ClearLines();
    }

    public void CloseGame()
    {
        StageManager.Ins.OpenStageMenu();
        onGame = false;

        Cell.ResetAll();

        ResetButton();

        ClearLines();
    }

    private void ResetButton()
    {
        findPath.interactable = true;
        autoMove.interactable = false;
    }

    private void ClearLines()
    {
        foreach (Transform child in lines)
        {
            Destroy(child.gameObject);
        }
    }

}
