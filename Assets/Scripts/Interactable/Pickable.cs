using UnityEngine;

public class Pickable : ObjectInteractable
{
    public override void WhenPlayerInteracts()
    {
        AddItemToInventory();
        GameObject.FindGameObjectWithTag("Player").GetComponent<PickItem>().PickUp(gameObject);

    }

    
}