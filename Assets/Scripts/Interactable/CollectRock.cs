using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectRock : InteractionPlayer
{
    public override void WhenPlayerInteracts()
    {
        var inventoryManager = GameObject.FindGameObjectWithTag("Inventory").GetComponent<AllItems>();

        AddItemToInventory(inventoryManager.Rock);
      
    }

    

    
}
