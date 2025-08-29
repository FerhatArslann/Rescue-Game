using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTooltip : MonoBehaviour
{
    void Update()
    {
        if (MouseOverInventory() != null)
        {
            string[] parentChildName = MouseOverInventory();
            InventorySystem inventory = GameObject.Find(parentChildName[0]).GetComponent<InventoryDisplay>().inventory;
            if (Convert.ToInt32(parentChildName[1]) < inventory.items.Count)
            {
                Item item = inventory.items[Convert.ToInt32(parentChildName[1])];
                if( item.GetItemType() != ItemType.Equipment)
                {
                    System.Func<string> getTooltipTextFunc = () => {
                        return $"<color=#00ff00>{item.name}</color>\nWeight: {item.weight}\n{item.description}\n";
                    };
                    TooltipScreenSpaceUI.ShowTooltip_Static(getTooltipTextFunc);
                }
                else
                {
                    System.Func<string> getTooltipTextFunc = () => {
                        return $"<color=#00ff00>{item.name}</color>\nWeight: {item.weight}\n<color=#ffff00></color>{item.description}\n";
                    };
                    TooltipScreenSpaceUI.ShowTooltip_Static(getTooltipTextFunc);
                }

            }
        }
        else
        {
            TooltipScreenSpaceUI.HideTooltip_Static();
        }
    }

    private string[] MouseOverInventory()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);

        for (int i = 0; i < raycastResultList.Count; i++)
        {
            if (raycastResultList[i].gameObject.GetComponent<DraggableWindow>() && raycastResultList[i].gameObject.transform.parent != null)
            {
                return new string[] { raycastResultList[i].gameObject.transform.parent.name, raycastResultList[i].gameObject.name };
            }
        }
        return null;
    }
}
