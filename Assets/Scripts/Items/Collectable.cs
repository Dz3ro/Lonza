using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : ObjectInteractable
{
    public int ItemQuantity 
    { 
        get { return _itemQuantity; } 
        set 
        {
            if (value >= 0)
                _itemQuantity = value;
            else
                _itemQuantity = 0;
        } }
    private int _itemQuantity = 1;
    public void WhenPlayerTouches()
    {
       // AddItemToInventory();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        AddItemToInventory(_itemQuantity);
        Destroy(gameObject);
    }
}
