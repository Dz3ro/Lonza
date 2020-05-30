using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private int _invSlotsCount = 40;
    private int _fastInvSlotsCount = 10;

    public class InventorySlot
    {
        public Item Item;
        public int ItemQuantity;

        public InventorySlot()
        {
            Item = new Item();
            ItemQuantity = 0;
        }
    }

    public int haha { get; set; }

    public List<InventorySlot> Inventory = new List<InventorySlot>();

    private void Start()
    {
        CreateInventory();
    }

    private void CreateInventory()
    {
        for (int i = 0; i < _invSlotsCount; i++)
        {
            Inventory.Add(new InventorySlot());
        }
    }

    private void Update()
    {
        Testing();
    }

    public bool InvFastBarIsEmpty()
    {
        for (int i = 0; i < _fastInvSlotsCount; i++)
            if (Inventory[i].Item.Name != "Nothing")
                return false;

        return true;
    }

    private void Testing()
    {
        foreach (var slot in Inventory)
        {
            if (slot.ItemQuantity == 0 && slot.Item.Name != "Nothing")
                slot.Item = new Item();
        }
    }
}