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
                   // print("added succesfully, there was already that item and had place");
                    return;
                }
            }
            if (inventorySlot.Item.Name == "Nothing")
            {
                inventorySlot.Item = item;
                inventorySlot.ItemQuantity++;
               // print("added succesfully to new slot");
                return;
            }
        }
        //print("adding failed, inventory full");
    }
   
   
}
