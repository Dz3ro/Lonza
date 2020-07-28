using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLake : Water
{
    
    private new void Start()
    {
        base.Start();
        base.AddFishToWater(base._allItems.GrayFish, 40);
    }

    void Update()
    {

    }

    public override void GetFish()
    {
        var fish = GetRandomFish();
        var itemGod = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsAddRemoveSearch>();

        itemGod.ItemAdd(fish);
    }
}
