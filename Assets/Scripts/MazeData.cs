using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "MazeData", menuName = "ScriptableObjects/MazeData", order = 1)]
public class MazeData : ScriptableObject
{
    public List<LevelData> data;
}

[System.Serializable]
public class LevelData
{
    public int id;
    // Status of level: (star = -1) -> level is locked
    //                  (star >= 0) -> level is unlocked
    //                                 score = star
    public int star = -1;
    public List<bool> seed;
}
