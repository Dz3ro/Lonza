using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableRock : Pickable
{
    public override void WhenPlayerInteracts()
    {
        var item = _allItems.Rock;
        PickItem(item);
    }
}
