using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : MonoBehaviour
{
    public InventorySystem inventory;
    public InventorySystem equipment;
    public GameObject inventoryGameObj;
    public GameObject equipmentGameObj;
    public void AddItem(Item item)
    {
        if (item.GetItemType() != ItemType.Equipment)
        {
            if (item.GetItemType() == ItemType.Consumable)
            {
                if (ItemStacking(item)) return;
            }
            if (inventory.items.Count < inventory.inventoryCapacity)
            {
                inventory.items.Add(Instantiate(item));
            }
        }
        else if (item.GetItemType() == ItemType.Equipment)
        {
            if (equipment.items.Count < equipment.inventoryCapacity)
            {
                equipment.items.Add(Instantiate(item));
            }
        }
    }

    public void RemoveItem(int index)
    {
        if (index < inventory.items.Count && index >= 0)
        {
            inventory.items.RemoveAt(index);
        }

    }

    private bool ItemStacking(Item item)
    {

        for (int i = 0; i < inventory.items.Count; i++)
        {
            Item inventoryItem = inventory.items[i];
            if (inventoryItem.GetItemType() == ItemType.Consumable && inventoryItem.name == item.name)
            {
                if (inventoryItem.stackSize + item.stackSize <= inventoryItem.maxStackSize)
                {
                    inventoryItem.stackSize += item.stackSize;
                    inventoryItem.weight = inventoryItem.stackSize * item.weight;
                    inventoryGameObj.GetComponent<InventoryDisplay>().RenderInventoryItems();
                    return true;
                }
                else if (inventoryItem.stackSize < inventoryItem.maxStackSize)
                {
                    int fillAmount = inventoryItem.maxStackSize - inventoryItem.stackSize;
                    inventoryItem.stackSize += fillAmount;
                    inventoryItem.weight = inventoryItem.stackSize * item.weight;
                    inventory.items.Add(Instantiate(item));
                    inventory.items[inventory.items.Count-1].stackSize = item.stackSize - fillAmount;
                    return true;
                }

            }
        }
        return false;
    }

    private void OnApplicationQuit()
    {
        inventory.items.Clear();
        equipment.items.Clear();
    }
}
