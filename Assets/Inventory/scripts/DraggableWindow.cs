using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Input;
using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableWindow : MonoBehaviour, IDragHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private Vector2 originPosition;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GameObject.Find("UI").GetComponent<Canvas>();
        originPosition = rectTransform.anchoredPosition;
    }
    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            rectTransform.anchoredPosition = originPosition;
        }
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}
