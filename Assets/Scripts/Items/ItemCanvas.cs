using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemCanvas : MonoBehaviour, IDropHandler
{
    private CanvasGroup _canvasGroup;
    private PlayerInventory _plrInv;

    private void Start()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (eventData == null)
            return;

        var stringSlotFrom = eventData.pointerDrag.transform.parent.name.Substring(4);
        var stringSlotInto = transform.name.Substring(4);
        var slotFrom = Convert.ToInt16(stringSlotFrom);
        var slotInto = Convert.ToInt16(stringSlotInto);

        var itemNameFrom = _plrInv.Inventory[slotFrom].Item.Name;
        var itemNameInto = _plrInv.Inventory[slotInto].Item.Name;

        if (itemNameFrom != itemNameInto)
            DropWhenItemsAreDifferent(eventData);
        else
            DropWhenItemsAreSame(eventData);
    }

    private void DropWhenItemsAreDifferent(PointerEventData eventData)
    {
        var itemInto = transform.GetChild(0);
        var itemFrom = eventData.pointerDrag.GetComponent<RectTransform>();
        itemInto.SetParent(itemFrom.parent.transform);
        itemInto.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        itemFrom.SetParent(gameObject.transform);
        itemFrom.anchoredPosition = Vector3.zero;

        var slotDraggedFrom = Convert.ToInt16(itemInto.parent.name.Substring(4));
        var slotDraggedInto = Convert.ToInt16(itemFrom.parent.name.Substring(4));

        var tempItem = _plrInv.Inventory[slotDraggedFrom];
        _plrInv.Inventory[slotDraggedFrom] = _plrInv.Inventory[slotDraggedInto];
        _plrInv.Inventory[slotDraggedInto] = tempItem;
    }

    private void DropWhenItemsAreSame(PointerEventData eventData)
    {
        var itemInto = transform.GetChild(0);
        var itemFrom = eventData.pointerDrag.GetComponent<RectTransform>();

        var slotDraggedInto = Convert.ToInt16(itemInto.parent.name.Substring(4));
        var slotDraggedFrom = Convert.ToInt16(itemFrom.parent.name.Substring(4));

        var quantityIn = _plrInv.Inventory[slotDraggedInto].ItemQuantity;
        var quantityFrom = _plrInv.Inventory[slotDraggedFrom].ItemQuantity;
        var maxItemQuantity = _plrInv.Inventory[slotDraggedFrom].Item.MaxQUantityPerStack;

        while (quantityFrom > 0 && quantityIn < maxItemQuantity)
        {
            quantityIn++;
            quantityFrom--;
        }
        _plrInv.Inventory[slotDraggedInto].ItemQuantity = quantityIn;
        _plrInv.Inventory[slotDraggedFrom].ItemQuantity = quantityFrom;
        itemInto.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(quantityIn);
        itemFrom.GetComponentInChildren<TextMeshProUGUI>().text = Convert.ToString(quantityFrom);
    }
}