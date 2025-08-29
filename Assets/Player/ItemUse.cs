using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public GameObject displayedInventory;
    private KeyCode[] useItemHotkeys = {
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
    }; 
    private InventorySystem inventory;

    private void Start()
    {
        inventory = this.GetComponent<InventoryObject>().inventory;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            for (int i = 0; i < useItemHotkeys.Length; i++)
            {
                if (Input.GetKeyDown(useItemHotkeys[i]) && i < inventory.items.Count)
                {
                    inventory.items[i].UseItem();
                    if (inventory.items[i].stackSize <= 0)
                    {
                        inventory.items.Remove(inventory.items[i]);
                    }
                    else if(inventory.items[i].GetItemType() != ItemType.Equipment)
                    {
                        displayedInventory.GetComponent<InventoryDisplay>().RenderInventoryItems();
                    }
                }
            }
        }
    }
}
