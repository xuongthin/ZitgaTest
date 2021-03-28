using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Ins;
    public int maxStages { get { return 999; } }
    public int maxUnlockedStages { get; set; }
    [SerializeField] private MazeData mazeData;
    [SerializeField] private StageUIManager UI;

    private void Awake()
    {
        Ins = this;
    }

    public void OpenStageMenu()
    {
        gameObject.SetActive(true);
    }

    public void ClickHandle(int id)
    {
        Game.Ins.OpenGame(id, mazeData.data[id].seed);
        gameObject.SetActive(false);
    }

    public int GetLevelStar(int id)
    {
        return mazeData.data[id].star;
    }

    public void UpdateStage(int id, int star)
    {
        mazeData.data[id].star = star;
        UI.UpdateStage(id);

        if (mazeData.data[id + 1].star == -1)
        {
            mazeData.data[id + 1].star = 0;
            UI.UpdateStage(id + 1);
        }
    }

    public void OpenMenu()
    {
        // gameObject.SetActive(true);
        // Menu.Ins.OpenMenu();
        SceneManager.LoadScene(0);
    }
}
