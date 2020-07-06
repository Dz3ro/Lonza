using UnityEngine;

public class Pickable : ObjectInteractable
{
    public override void WhenPlayerInteracts()
    {
        var z = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsAddRemoveSearch>();
        var name = ReadGameObjectName();
        z.ItemAdd(name);
        GameObject.FindGameObjectWithTag("Player").GetComponent<PickItem>().PickUp(gameObject);

    }

    
}