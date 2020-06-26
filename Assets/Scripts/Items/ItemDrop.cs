using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;


public class ItemDrop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler

{

    private bool outsideUI = false;

    private List<GameObject> _pickable = new List<GameObject>();
    private List<GameObject> _collectable = new List<GameObject>();

    private void Start()
    {

        _pickable = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<FullList>().Pickable;
        _collectable = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<FullList>().Collectable;
        

    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            DropItem();
    }





    private void DropItem()
    {
        if (!outsideUI)
            return;

        var slotHolding = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>().ItemHolding;

        var slotHoldingName = slotHolding.Item.Name;
        var collectableToDrop = _collectable.FirstOrDefault(gObj => gObj.name.Remove(gObj.name.Length - 1) == slotHoldingName);

        
        var dropped = Instantiate(collectableToDrop);
        dropped.GetComponent<Collectable>().ItemQuantity = slotHolding.ItemQuantity;
        dropped.transform.position = new Vector3(3f,3f,0f);
        slotHolding.ItemQuantity = 0;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        outsideUI = false;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outsideUI = true;
    }


}

   

