using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagesGroup : MonoBehaviour
{
    private int id;
    [SerializeField] private List<Stage> stages;
    [SerializeField] private Image[] lines;

    public void Init(StagesGroup adjacentStagesGroup, bool initAbove)
    {
        int newSeq = adjacentStagesGroup.id + (initAbove ? 1 : -1);
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

    public void Init(int id)
    {
        lines[0].enabled = (id % 2 == 1) && id * 4 < StageManager.Ins.maxStages;
        lines[1].enabled = id % 2 == 0 && id * 4 < StageManager.Ins.maxStages;

        int tmpId;

        if (id % 2 == 0)
        {
            for (var i = 0; i < 4; i++)
            {
                tmpId = id * 4 + i;
                stages[i].Init(tmpId, StageManager.Ins.GetLevelStar(tmpId));
            }
        }
        else
        {
            for (var i = 0; i < 4; i++)
            {
                tmpId = (id + 1) * 4 - i - 1;
                stages[i].Init(tmpId, StageManager.Ins.GetLevelStar(tmpId));
            }
        }

        this.id = id;
    }

    public bool isFirst()
    {
        return id <= 0;
    }

    public bool isLast()
    {
        bool isLastStageGroup = (id + 1) * 4 >= StageManager.Ins.maxStages;
        return isLastStageGroup;
    }

    public Stage FindStage(int id)
    {
        if (id < this.id * 4 || id > this.id * 4 + 3)
        {
            return null;
        }
        else
        {
            return stages[id - this.id * 4];
        }
    }
}
