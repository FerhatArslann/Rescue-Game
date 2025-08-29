using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundItem : MonoBehaviour
{
    [SerializeField]
    public bool useObjectIcon;
    public Item item;
    void Start()
    {
        if (!useObjectIcon) this.GetComponent<SpriteRenderer>().sprite = item.sprite;
    }

    void Update()
    {
        
    }
    //when player pick up item add item to player inventory if inventory is not full
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            var InventoryObject = collision.GetComponent<InventoryObject>();

            if (InventoryObject.inventory.items.Count < InventoryObject.inventory.inventoryCapacity)
            {
                collision.GetComponent<InventoryObject>().AddItem(item);
                Destroy(gameObject);
            }

        }
    }
}
