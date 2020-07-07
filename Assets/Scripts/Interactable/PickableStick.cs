using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableStick : Pickable
{
    public override void WhenPlayerInteracts()
    {
        var stick = _allItems.Stick;
        PickItem(stick);
    }
}
