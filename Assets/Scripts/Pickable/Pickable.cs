using UnityEngine;

public class Pickable : ObjectInteractable
{

    protected ItemsCreator _allItems;

    protected void Awake()
    {
        _allItems = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsCreator>();
    }

    protected void Start()
    {

    }
    public override void WhenPlayerInteracts()
    {
        print("picking item");
    }

    protected void PickItem(Item item)
    {
        var z = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsAddRemoveSearch>();
        z.ItemAdd(item);
        GameObject.FindGameObjectWithTag("Player")
            .GetComponent<PlayerAnimationEvents>().PickUp(gameObject);
    }

    
}