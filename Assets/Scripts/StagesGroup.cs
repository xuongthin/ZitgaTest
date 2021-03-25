using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagesGroup : MonoBehaviour
{
    private int seq;
    [SerializeField] private List<Stage> stages;
    [SerializeField] private Image[] lines;

    public void Init(int seq)
    {
        lines[0].enabled = (seq % 2 == 1) && seq * 4 < LevelController.Instance.MaxStages;
        lines[1].enabled = seq % 2 == 0 && seq * 4 < LevelController.Instance.MaxStages;

        int tmp;
        for (var i = 0; i < 4; i++)
        {
            tmp = seq * 4 + i;
            // stages[i].Init(tmp, LevelController.Instance.ScoreData[tmp]);
        }

        this.seq = seq;
    }

    public void Init(StagesGroup adjacentStagesGroup, bool initAbove)
    {
        int newSeq = adjacentStagesGroup.seq + (initAbove ? 1 : -1);
        Init(newSeq);

        transform.localPosition = adjacentStagesGroup.transform.localPosition + Vector3.up * 250 * (initAbove ? 1 : -1);
        if (initAbove)
        {
            transform.SetAsLastSibling();
        }
        else
        {
            transform.SetAsFirstSibling();
        }
    }

    public bool isFirst()
    {
        return seq == 0;
    }

    public bool isLast()
    {
        bool isLastStageGroup = (seq + 1) * 4 >= LevelController.Instance.MaxStages;
        return isLastStageGroup;
    }
}
