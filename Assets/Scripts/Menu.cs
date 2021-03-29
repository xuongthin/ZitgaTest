using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private MazeData mazeData;

    private void Awake()
    {
        SaveSystem.Ins.Load();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void RandomGameData()
    {
        int randomMax = Random.Range(1, mazeData.data.Count);
        for (int i = 0; i < randomMax; i++)
        {
            mazeData.data[i].star = Random.Range(1, 4);
        }
        mazeData.data[randomMax].star = 0;
        SaveSystem.Ins.Save();
    }

    public void ResetGameData()
    {
        mazeData.data[0].star = 0;
        for (int i = 1; i < mazeData.data.Count; i++)
        {
            mazeData.data[i].star = -1;
        }
        SaveSystem.Ins.Save();
    }
}
