using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item/Equipment")]

public class Equipment : Item
{
    public float heatResistance;
    public float oxygen;

    public Equipment()
    {
        itemType = ItemType.Equipment;
    }

    public Dictionary<string, float> GetItemStats()
    {
        Dictionary<string, float> itemStats = new Dictionary<string, float>()
        { 
            {"heatResistance", heatResistance },
            {"oxygen", oxygen }
        };
        return itemStats;
    }
}
