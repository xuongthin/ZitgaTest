using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StageUIManager : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    [SerializeField] private GameObject prefabStagesGroup;
    private Vector2 lastDragPointer;
    private bool isScrollingUp;
    private List<StagesGroup> stagesGroups;
    [SerializeField] private Transform stagesParent;
    private int highestStageGroupId;
    private int lowestStageGroupId;
    [SerializeField] private Transform initPoisition;
    [SerializeField] private Transform highestPosition;
    [SerializeField] private Transform topThreshold;
    [SerializeField] private Transform bottomThreshold;
    private ScrollRect scroll;

    private void Awake()
    {
        scroll = GetComponent<ScrollRect>();
    }

    private void Start()
    {
        stagesGroups = new List<StagesGroup>();

        for (int i = 0; i < 6; i++)
        {
            StagesGroup group = Instantiate(prefabStagesGroup, stagesParent).GetComponent<StagesGroup>();
            if (i == 0)
            {
                group.Init(0);
                group.transform.position = initPoisition.position;
            }
            else
            {
                group.Init(stagesGroups[i - 1], true);
            }
            stagesGroups.Add(group);
        }

        highestStageGroupId = stagesGroups.Count - 1;
        lowestStageGroupId = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastDragPointer = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        isScrollingUp = eventData.position.y > lastDragPointer.y;
        lastDragPointer = eventData.position;
    }

    public void OnViewScroll()
    {
        if (isScrollingUp)
        {
            if (stagesGroups[0].isFirst())
            {
                float offer = stagesGroups[0].transform.position.y - initPoisition.position.y;

                if (offer >= 0)
                {
                    scroll.StopMovement();
                    scroll.velocity = Vector2.up * (-offer * 1000);
                }
                return;
            }

            if (stagesGroups[highestStageGroupId].transform.position.y >= topThreshold.position.y)
            {
                stagesGroups[highestStageGroupId].Init(stagesGroups[lowestStageGroupId], false);
                lowestStageGroupId = highestStageGroupId;
                highestStageGroupId = (highestStageGroupId == 0) ? (stagesGroups.Count - 1) : (highestStageGroupId - 1);
            }
        }
        else
        {
            if (stagesGroups[3].isLast())
            {
                float offer = stagesGroups[3].transform.position.y - highestPosition.position.y;
                if (offer <= 0)
                {
                    scroll.StopMovement();
                    scroll.velocity = Vector2.up * (-offer * 1000);
                }
                return;
            }

            if (stagesGroups[lowestStageGroupId].transform.position.y <= bottomThreshold.position.y)
            {
                stagesGroups[lowestStageGroupId].Init(stagesGroups[highestStageGroupId], true);
                highestStageGroupId = lowestStageGroupId;
                lowestStageGroupId = (lowestStageGroupId == (stagesGroups.Count - 1)) ? 0 : (lowestStageGroupId + 1);
            }
        }
    }

    public void UpdateStage(int id)
    {
        Stage stage = FindStageOnView(id);
        if (stage != null)
        {
            stage.Init(id, StageManager.Ins.GetLevelStar(id));
        }

    }

    private Stage FindStageOnView(int id)
    {
        Stage result = null;
        foreach (StagesGroup group in stagesGroups)
        {
            result = group.FindStage(id);
            if (result != null)
            {
                return result;
            }
        }
        return result;
    }
}
