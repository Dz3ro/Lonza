﻿using UnityEngine;

public class Pickable : ObjectInteractable
{
    public override void WhenPlayerInteracts()
    {
        AddItemToInventory();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PickItem>().PickUp(gameObject);

    }

    //private void AddItemToInventory2(Item item)
    //{
    //    // this method adds the item to your inventory into correct slot

    //    var playerInventory = GameObject.FindGameObjectWithTag("Inventory")
    //        .GetComponent<PlayerInventory>().Inventory;

    //    foreach (var inventorySlot in playerInventory)
    //    {
    //        if (inventorySlot.Item == item)
    //        {
    //            if (inventorySlot.ItemQuantity < inventorySlot.Item.MaxQUantityPerStack)
    //            {
    //                inventorySlot.ItemQuantity++;
    //                return;
    //            }
    //        }
    //        if (inventorySlot.Item.Name == "Nothing")
    //        {
    //            inventorySlot.Item = item;
    //            inventorySlot.ItemQuantity++;
    //            return;
    //        }
    //    }
    //}

    //    private void AddItemToInventory()
    //    {
    //        // this method validates that in itemslist exist item with name like gameobject
    //        // after that calls 2nd method
    //        var inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsCreator>();
    //        var name = gameObject.name.Substring(0, gameObject.name.Length - 1);
    //        var items = inventoryManager.ItemsList;

    //        foreach (var item in items)
    //        {
    //            if (name == item.Name)
    //            {
    //                AddItemToInventory2(item);
    //                /////
    //                //////
    //                /////
    //                ///

    //                GameObject.FindGameObjectWithTag("Player").GetComponent<PickItem>().PickUp(gameObject);
    //                return;
    //            }
    //        }
    //    }
}