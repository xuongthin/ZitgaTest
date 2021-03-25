using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance;

    public int MaxStages { get { return 999; } }
    public int MaxUnlockedStages { get; set; }
    public List<int> ScoreData { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // Load save file
        if (false)
        {

        }
        else
        {
            MaxUnlockedStages = 1;
            ScoreData = new List<int>();
        }
    }

    internal void ClickHandle(int index)
    {
        Debug.Log("open stage " + index);
    }
}
