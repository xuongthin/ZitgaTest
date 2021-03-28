using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public static Menu Ins;
    [SerializeField] private MazeData mazeData;

    public void Play()
    {
        // gameObject.SetActive(false);
        // StageManager.Ins.OpenStageMenu();
        SceneManager.LoadScene(1);
    }

    // public void OpenMenu()
    // {
    //     gameObject.SetActive(true);
    // }

    public void RandomGameData()
    {
        int randomMax = Random.Range(1, mazeData.data.Count);
        for (int i = 0; i < randomMax; i++)
        {
            mazeData.data[i].star = Random.Range(1, 4);
        }
        mazeData.data[randomMax].star = 0;
    }

    public void ResetGameData()
    {
        mazeData.data[0].star = 0;
        for (int i = 1; i < mazeData.data.Count; i++)
        {
            mazeData.data[i].star = -1;
        }
    }
}
