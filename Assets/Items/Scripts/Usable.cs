using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "Item/Usable")]

public class Usable : Item
{
    [SerializeField] public ItemAbility itemAbility;
    ItemAbilities itemAbilities = new ItemAbilities();
    public Usable()
    {
        itemType = ItemType.Usable;
    }

    public override void UseItem()
    {
        itemAbilities.UseAbility(itemAbility);
    }
}
