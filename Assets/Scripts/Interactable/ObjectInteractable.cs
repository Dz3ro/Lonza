using UnityEngine;

public class ObjectInteractable : MonoBehaviour
{
    public virtual void WhenPlayerInteracts()
    {
        print("base interaction");
    }

    // all my gameojects are named like Rock0 Stick1 Rock3(Clone)
    // this emthod is supposed to read jsut the first word

    protected void AddItemToInventory()
    {
        // this method validates that in itemslist exist item with name like gameobject
        // after that calls 2nd method
        var inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsCreator>();
        var name = ReadGameObjectName();

        var items = inventoryManager.ItemsList;

        foreach (var item in items)
        {
            if (name == item.Name)
            {
                AddItemToInventory2(item);
                return;
            }
        }
    }

    protected void AddItemToInventory(int ItemQuantity)
    {
        // this method validates that in itemslist exist item with name like gameobject
        // after that calls 2nd method
        var inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsCreator>();
        var name = ReadGameObjectName();

        var items = inventoryManager.ItemsList;

        foreach (var item in items)
        {
            if (name == item.Name)
            {
                for (int i = 0; i < ItemQuantity; i++)
                    AddItemToInventory2(item);
                return;
            }
        }
    }

    private void AddItemToInventory2(Item item)
    {
        // this method adds the item to your inventory into correct slot

        var playerInventory = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>().Inventory;

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
            if (inventorySlot.Item.Name == "Nothing")
            {
                inventorySlot.Item = item;
                inventorySlot.ItemQuantity++;
                return;
            }
        }
    }

    private string ReadGameObjectName()
    {
        var name = gameObject.name;
        var hasCloneInName = false;
        string finalName;

        if (name.Length > 10)
        {
            var partName = name.Substring(name.Length - 7);

            if (partName == "(Clone)")
                hasCloneInName = true;
        }
        if (!hasCloneInName)
            finalName = gameObject.name.Substring(0, gameObject.name.Length - 1);
        else
            finalName = gameObject.name.Substring(0, gameObject.name.Length - 8);

        return finalName;
    }
}