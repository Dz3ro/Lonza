using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionPlayer : MonoBehaviour
{

    public virtual void WhenPlayerInteracts()
    {
        print("base interaction");
    }

    public void AddItemToInventory(Item item)
    {
        var playerInventory = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>().Inventory;

        foreach(var inventorySlot in playerInventory)
        {
            if (inventorySlot.Item == item)
            {
                if (inventorySlot.ItemQuantity < inventorySlot.Item.MaxQUantityPerStack)
                {
                    inventorySlot.ItemQuantity++;
                    return;
                }
            }
            if (inventorySlot.Item.Name == "Nothing")
            {
                inventorySlot.Item = item;
                inventorySlot.ItemQuantity++;
                return;
            }
        }
    }
   
   
}
