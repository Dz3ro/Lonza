﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemsAddRemoveSearch : MonoBehaviour
{
    private ItemsCreator _allItems;
    private PlayerInventory _plrInv;

    private void Awake()
    {
        _allItems = GetComponent<ItemsCreator>();
        _plrInv = GetComponent<PlayerInventory>();
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown("o"))
            ItemAdd("Fishing Rod");

        if (Input.GetKeyDown("p"))
            ItemAdd("Pickaxe");

        if (Input.GetKeyDown("l"))
            ItemAdd("Axe");

        if (Input.GetKeyDown("k"))
            ItemAdd("Wood Floor", 64);



    }

    public void ItemAdd(Item item)
    {
        AddItemToInventory(item);
    }
    public void ItemAdd(Item item, int quantity)
    {
        for (int i = 0; i < quantity; i++)
            AddItemToInventory(item);
    }
    public void ItemAdd(string itemName)
    {
        var item = FindItemByName(itemName);
        AddItemToInventory(item);
    }
    public void ItemAdd(string itemName, int itemQuantity)
    {
        var item = FindItemByName(itemName);
        for (int i = 0; i < itemQuantity; i++)
            AddItemToInventory(item);
    }

    public int ItemQuantityInInventory(Item item)
    {
       return CheckItemQuantityInInventory(item);
    }
    public int ItemQuantityInInventory(string itemName)
    {
        var item = FindItemByName(itemName);
        return CheckItemQuantityInInventory(item);
    }

    public void ItemRemove(Item item)
    {
        RemoveItemFromInventory(item);
    }
    public void ItemRemove(Item item, int itemQuantity)
    {
        for (int i = 0; i < itemQuantity; i++)
            RemoveItemFromInventory(item);
    }
    public void ItemRemove(string itemName)
    {
        var item = FindItemByName(itemName);

        RemoveItemFromInventory(item);
    }
    public void ItemRemove(string itemName, int itemQuantity)
    {
        var item = FindItemByName(itemName);

        for (int i = 0; i < itemQuantity; i++)
            RemoveItemFromInventory(item);
    }

    public bool ItemCanBePutInHand(Item item, int itemQuantity = 1)
    {
        var hand = _plrInv.ItemHolding;
        var handItem = _plrInv.ItemHolding.Item;
        var handItemQuant = _plrInv.ItemHolding.ItemQuantity;
        var maxQuant = _plrInv.ItemHolding.Item.MaxQUantityPerStack;

        if (!handItem.ThisIsANewEmptyItem() && handItem != item)
            return false;

        if (handItem == item && handItemQuant + itemQuantity > maxQuant)
            return false;
        return true;
    }
    public bool ItemCanBePutInHand(string itemName, int itemQuantity = 1)
    {
        var item = FindItemByName(itemName);
        var hand = _plrInv.ItemHolding;
        var handItem = _plrInv.ItemHolding.Item;
        var handItemQuant = _plrInv.ItemHolding.ItemQuantity;
        var maxQuant = _plrInv.ItemHolding.Item.MaxQUantityPerStack;

        if (!handItem.ThisIsANewEmptyItem() && handItem != item)
            return false;

        if (handItem == item && handItemQuant + itemQuantity > maxQuant)
            return false;
        return true;
    }

    public void ItemPutIntoHand(Item item, int itemQuantity = 1)
    {
        if (! ItemCanBePutInHand(item))
            return;
        _plrInv.ItemHolding.Item = item;
        _plrInv.ItemHolding.ItemQuantity = itemQuantity;

    }
    public void ItemPutIntoHand(string itemName, int itemQuantity = 1)
    {
        var item = FindItemByName(itemName);

        if (!ItemCanBePutInHand(item))
            return;
        _plrInv.ItemHolding.Item = item;
        _plrInv.ItemHolding.ItemQuantity = itemQuantity;

    }



    private int CheckItemQuantityInInventory(Item item)
    {
        var number = 0;
        var playerInventory = _plrInv.Inventory;

        foreach (var inventorySlot in playerInventory)
            if (inventorySlot.Item == item)
                number += inventorySlot.ItemQuantity;

        // this part checks the hand after inventory
        if (_plrInv.ItemHolding.Item == item)
            number += _plrInv.ItemHolding.ItemQuantity;

        return number;
    }
    private void AddItemToInventory(Item item)
    {
        var playerInventory = _plrInv.Inventory;

        foreach (var inventorySlot in playerInventory)
        {
            if (inventorySlot.Item == item)
            {
                if (inventorySlot.ItemQuantity < inventorySlot.Item.MaxQUantityPerStack)
                {
                    inventorySlot.ItemQuantity++;
                    return;
                }
            }
        }
        foreach (var inventorySlot in playerInventory)
        {
            if(inventorySlot.Item.ThisIsANewEmptyItem())
            {
                inventorySlot.Item = item;
                inventorySlot.ItemQuantity++;
                return;
            }
        }

        /// this part of code fires when inventory is full
        /// tries to do the same stuff with holding slot

        if (_plrInv.ItemHolding.Item == item)
        {
            _plrInv.ItemHolding.ItemQuantity++;
            return;
        }
        if (_plrInv.ItemHolding.Item.ThisIsANewEmptyItem())
        {
            _plrInv.ItemHolding.Item = item;
            _plrInv.ItemHolding.ItemQuantity++;
            return;
        }

    }
    private void RemoveItemFromInventory(Item item)
    {
        var playerInventory = _plrInv.Inventory;

        foreach (var inventorySlot in playerInventory)
        {
            if (inventorySlot.Item == item && inventorySlot.ItemQuantity > 0)
            {
                inventorySlot.ItemQuantity--;
                return;
            }
        }
        //
        /// this part of code fires when inventory is full
        /// tries to do the same stuff with holding slot
        if (_plrInv.ItemHolding.Item == item && _plrInv.ItemHolding.ItemQuantity > 0)
        {
            _plrInv.ItemHolding.ItemQuantity--;
            return;
        }
        //
    }
    private Item FindItemByName(string name)
    {
        //code below is to avoid capitalization mistakes
        // like typing tool binding instead of Tool Binding

        //var lastCharacterWasSpace = true;
        //StringBuilder nameBuild = new StringBuilder();
        //for (int i = 0; i < name.Length; i++)
        //{
        //    if (lastCharacterWasSpace)
        //        nameBuild.Append(name[i].ToString().ToUpper());
        //    else
        //        nameBuild.Append(name[i].ToString().ToLower());

        //    lastCharacterWasSpace =  String.IsNullOrWhiteSpace(name[i].ToString());
        //}
        //var nameCorrect = nameBuild.ToString();
        

        var allItems = _allItems.ItemsList;
        var item = allItems.SingleOrDefault(i => i.Name == name);

        return item;
    }

}
