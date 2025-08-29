using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class InventoryDisplay : MonoBehaviour
{
    [SerializeField]
    public InventorySystem inventory;
    public GameObject prefab;
    public float starting_Point_x;
    public float starting_Point_y;
    public float spacing_x;
    public float spacing_y;
    public int columns;

    private List<Item> old_list;

    private void Start()
    {
        old_list = new List<Item>(inventory.items);
        RenderInventoryItems();
    }
    // Update is called once per frame
    void Update()
    {
        if (!old_list.SequenceEqual(inventory.items))
        {
            old_list = new List<Item>(inventory.items);
            RenderInventoryItems();
        }

    }

    public void RenderInventoryItems()
    {
        foreach (Transform child in this.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        int row = 0;
        int column = 0;
        for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject item = Instantiate(prefab, this.transform.position + new Vector3(starting_Point_x + (column * spacing_x), starting_Point_y - (row * spacing_y), 0f), Quaternion.identity, this.transform);
            item.name = i.ToString();
            item.GetComponent<Image>().sprite = inventory.items[i].sprite;
            item.GetComponentInChildren<TextMeshProUGUI>().SetText(inventory.items[i].stackSize.ToString());
            if (column < columns-1) { column++; }
            else { column = 0; row++; };
        }
    }

}
