using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryMouseHover : MonoBehaviour
{
    [SerializeField]
    private GameObject targetInventoryObject;
    //targetItemType;
    // Update is called once per frame
    void Update()
    {
        OnMouseClickItem();
    }

    private void OnMouseClickItem()
    {
        if (MouseOverInventory() != null && Input.GetMouseButtonDown(0))
        {
            Item item = this.gameObject.GetComponent<InventoryDisplay>().inventory.items[Convert.ToInt32(MouseOverInventory())];
            targetInventoryObject.GetComponent<InventoryObject>().AddItem(item);   
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
                    //targetItemType = raycastResultList[i].gameObject.get
                    return raycastResultList[i].gameObject.name;
                }
            }
        }

        return null;
    }
}
