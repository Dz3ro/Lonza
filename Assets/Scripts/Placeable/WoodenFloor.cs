using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenFloor : Pickable
{
    void Start()
    {
        
    }

    void Update()
    {
    }

    public override void WhenPlayerInteracts()
    {
        var floorWooden = _allItems.FloorWooden;
        //var floorWooden = GameObject.FindGameObjectWithTag("Inventory")
        //    .GetComponent<ItemsCreator>().FloorWooden;
        PickItem(floorWooden);
    }
}
