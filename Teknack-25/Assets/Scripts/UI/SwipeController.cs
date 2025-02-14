using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // Corrected "EventSystem" -> "EventSystems"

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    // Serialized fields (private variables exposed in Inspector)
    [SerializeField] int maxPage;
    int currentPage;
    
    // Represents point or vector in 3D space
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;

    // RectTransform is used to store values of position, size
    [SerializeField] RectTransform levelPagesRect;
    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    private float dragThreshold; // Fixed: Added missing declaration

    private void Awake()
    {
        currentPage = 1;
        targetPos = levelPagesRect.localPosition;
        dragThreshold = Screen.width / 15; // Fixed: "screen.width" -> "Screen.width"
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        levelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
        {
            if (eventData.position.x > eventData.pressPosition.x)
                Previous();
            else
                Next();
        }
        else
        {
            MovePage();
        }
    }
}
