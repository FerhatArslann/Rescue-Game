using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStats : MonoBehaviour
{
    //player inventory and equipment
    public InventorySystem inventory;
    public InventorySystem equipment;
    public float weight = 0;

    private List<Item> currentItems = new List<Item>();
    private Equipment currentEquipment;
    private Dictionary<string, float> equipmentStats;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var i in inventory.items)
        {
            currentItems.Add(Instantiate(i));
        }
        if (equipment.items.Count > 0) currentEquipment = (Equipment)Instantiate(equipment.items[0]);
    }

    // Update is called once per frame
    void Update()
    {
        if (CompareItemLists())
        {
            SetItemsWeight();
            currentItems.Clear();
            foreach (var i in inventory.items)
            {
                currentItems.Add(Instantiate(i));
            }
        }
           
        if (equipment.items.Count > 0 && currentEquipment == null)
        {
            currentEquipment = (Equipment)Instantiate(equipment.items[0]);
            equipmentStats = currentEquipment.GetItemStats();
            SetItemsWeight();
        }
        else if (equipment.items.Count <= 0 && currentEquipment != null)
        {
            currentEquipment = null;
            equipmentStats = new Dictionary<string, float>();
            SetItemsWeight();
        }
    }

    private bool CompareItemLists()
    {
        if (currentItems.Count != inventory.items.Count) return true;
        for(int i=0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].name != currentItems[i].name || inventory.items[i].stackSize != currentItems[i].stackSize) return true;
        }
        return false;
    }

    //Calculate & set player items + equipment load
    private void SetItemsWeight()
    {
        weight = 0;
        foreach (var i in inventory.items)
        {
            weight += i.weight;
        }
        if (equipment.items.Count > 0)
        {
            weight += currentEquipment.weight;
        }
        if (this.gameObject.GetComponent<PlayerController>() != null)
        {
            this.gameObject.GetComponent<PlayerController>().weight = weight;
        }
    }
}
