using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class RemoveInventoryItem : MonoBehaviour
{
    [SerializeField]
    private InventorySystem inventory;

    // Update is called once per frame
    void Update()
    {
        OnMouseClickItem();
    }

    private void OnMouseClickItem()
    {
        if (MouseOverInventory() != null && Input.GetMouseButtonDown(1))
        {
            inventory.items.RemoveAt(Convert.ToInt32(MouseOverInventory()));
        }
    }

    private string MouseOverInventory()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<DraggableWindow>() != null)
            {
                if (raycastResultList[i].gameObject.transform.IsChildOf(this.gameObject.transform))
                {
                    return raycastResultList[i].gameObject.name;
                }
            }
        }

        return null;
    }
}
