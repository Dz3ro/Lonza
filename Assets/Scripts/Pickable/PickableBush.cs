using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableBush : Pickable
{
    public override void WhenPlayerInteracts()
    {
        var bush = _allItems.Bush;
        PickItem(bush);
    }
}
