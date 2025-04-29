using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public string color;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    [SerializeField] private RectTransform[] dropTargets;

    public bool canDrug;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(canDrug)
            canvasGroup.blocksRaycasts = false; 
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrug)
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (canDrug)
        {
            canvasGroup.blocksRaycasts = true;
            for (int i = 0; i < dropTargets.Length; i++)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(dropTargets[i], Input.mousePosition, canvas.worldCamera) && !dropTargets[i].GetComponent<RoadManager>().isBusy)
                {
                    canDrug = false;
                    Game.Instance.OnPlaneClaimed(color, dropTargets[i].gameObject);
                    gameObject.transform.position = dropTargets[i].transform.position;
                }
            }
        }
    }
}
