﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInventory : MonoBehaviour
{
    public UnityEvent OnSlotClick;

    public class InventorySlot
    {
        private Item _item;
        private int _itemQuantity;

        public Item Item
        {
            get { return _item; }
            set { _item = value; }
        }

        public int ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                _itemQuantity = value;
                if (_itemQuantity == 0)
                    _item = new Item();
            }
        }

        public InventorySlot()
        {
            Item = new Item();
            ItemQuantity = 0;
        }
    }

    public InventorySlot ItemHolding
    {
        get
        {
            return _itemHolding;
        }
        set
        {
            var temp = value;
            if (temp.ItemQuantity < 1)
                temp.Item = new Item();
            _itemHolding = temp;
        }
    }

    public InventorySlot ItemEquiped 
    { 
        get { return _itemEquiped; }
        set { _itemEquiped = value; }
    }

    public List<InventorySlot> Inventory = new List<InventorySlot>();

    private int _invSlotsCount = 40;
    private int _fastInvSlotsCount = 10;
    private InventorySlot _itemHolding;
    private InventorySlot _itemEquiped;

    private void Awake()
    {
    }

    private void Start()
    {
        _itemHolding = new InventorySlot();
        _itemEquiped = new InventorySlot();
        CreateInventory();
    }

    private void Update()
    {
    }

    public bool AllSlotsInInvertoryPartAreEmpty()
    {
        for (int i = 0; i < _fastInvSlotsCount; i++)
            if (! Inventory[i].Item.ThisIsANewEmptyItem())
                return false;
        return true;
    }

    public bool HoldingHandIsEmpty()
    {
        return _itemHolding.Item.ThisIsANewEmptyItem();
    }

    private void CreateInventory()
    {
        for (int i = 0; i < _invSlotsCount; i++)
        {
            Inventory.Add(new InventorySlot());
        }
    }
}