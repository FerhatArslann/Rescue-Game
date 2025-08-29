using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Item/Consumable")]
public class Consumable : Item
{

    [SerializeField]public ItemAbility itemAbility;
    ItemAbilities itemAbilities = new ItemAbilities();
    public Consumable()
    {
        itemType = ItemType.Consumable;
    }

    public override void UseItem()
    {
        itemAbilities.UseAbility(itemAbility);
        stackSize--;
    }
}
