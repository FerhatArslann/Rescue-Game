using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Equipment,
    Consumable,
    Usable
}

public abstract class Item : ScriptableObject
{
    protected ItemType itemType;
    public ItemType GetItemType() { return itemType; }
    public int stackSize;
    public int maxStackSize;
    public new string name;
    [TextArea(15,20)]
    public string description;
    public Sprite sprite;
    public float weight;
    public virtual void UseItem() { Debug.Log("Abstract"); }

}
