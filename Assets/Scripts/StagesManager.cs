using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StagesManager : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private Vector2 lastDragPointer;
    private bool isScrollingUp;
    [SerializeField] private StagesGroup[] stagesGroups;
    private int highestStageGroupId;
    private int lowestStageGroupId;
    [SerializeField] private Transform highThreshold;
    [SerializeField] private Transform lowThreshold;

    private void Start()
    {
        highestStageGroupId = stagesGroups.Length - 1;
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
            if (stagesGroups[highestStageGroupId].transform.position.y >= highThreshold.position.y)
            {
                stagesGroups[highestStageGroupId].Init(stagesGroups[lowestStageGroupId], false);
                lowestStageGroupId = highestStageGroupId;
                highestStageGroupId = (highestStageGroupId == 0) ? (stagesGroups.Length - 1) : (highestStageGroupId - 1);
            }
        }
        else
        {
            if (stagesGroups[lowestStageGroupId].transform.position.y <= lowThreshold.position.y)
            {
                stagesGroups[lowestStageGroupId].Init(stagesGroups[highestStageGroupId], true);
                highestStageGroupId = lowestStageGroupId;
                lowestStageGroupId = (lowestStageGroupId == (stagesGroups.Length - 1)) ? 0 : (lowestStageGroupId + 1);
            }
        }
    }
}
