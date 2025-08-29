using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory", menuName = "Inventory")]
public class InventorySystem : ScriptableObject
{
    public int inventoryCapacity;
    public List<Item> items;
}
