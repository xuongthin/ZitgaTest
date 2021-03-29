using System;
using System.Text;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Ins;

    public MazeData mazeData;

    private void Awake()
    {
        Ins = this;
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        StringBuilder dataBuilder = new StringBuilder("", mazeData.data.Count);
        foreach (LevelData level in mazeData.data)
        {
            if (level.star == -1)
            {
                dataBuilder.Append('-');
            }
            else
            {
                dataBuilder.Append(level.star);
            }
        }
        PlayerPrefs.SetString("maze_data", dataBuilder.ToString());
    }

    public void Load()
    {
        if (!PlayerPrefs.HasKey("maze_data"))
        {
            return;
        }
        string data = PlayerPrefs.GetString("maze_data");
        for (int i = 0; i < data.Length && i < mazeData.data.Count; i++)
        {
            mazeData.data[i].star = (int)Char.GetNumericValue(data[i]);
        }
    }
}
