using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// script lets inventory managing by left and right clicking slots

public class SlotClick : MonoBehaviour,
    IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private PlayerInventory _plrInv;
    private int _invtrNmbr;
    private Image _image;
    private TextMeshProUGUI _itemCount;
    private RectTransform _rTm;

    private void Awake()
    {
        _rTm = GetComponent<RectTransform>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
        _image = GetComponent<Image>();
        _itemCount = gameObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _invtrNmbr = Convert.ToInt16(transform.name.ToString().Substring(4));
    }

    private void Update()
    {
        ShowItem();
    }

    private void OnEnable()
    {
        if (_rTm == null)
            _rTm = GetComponent<RectTransform>();

        _rTm.localScale = Vector3.one;
    }

    private void ShowItem()
    {
        var inventoryItem = _plrInv.Inventory[_invtrNmbr];

        if (inventoryItem.Item.Name == "Nothing")
        {
            _image.color = Color.clear;
            _itemCount.color = Color.clear;
        }
        else if (inventoryItem.Item.Name != "Nothing")
        {
            _image.color = Color.white;
            _image.sprite = inventoryItem.Item.Image;
            _itemCount.color = Color.white;
            _itemCount.text = inventoryItem.ItemQuantity.ToString();
        }
    }

    private void ClickSlotLeft()
    {
        if (_plrInv.ItemHolding.Item.Name == "Nothing")
        {
            _plrInv.ItemHolding.Item = _plrInv.Inventory[_invtrNmbr].Item;
            _plrInv.ItemHolding.ItemQuantity = _plrInv.Inventory[_invtrNmbr].ItemQuantity;
            _plrInv.Inventory[_invtrNmbr].ItemQuantity = 0;
        }
        else if (_plrInv.ItemHolding.Item.Name == _plrInv.Inventory[_invtrNmbr].Item.Name)
        {
            var quantitySlot = _plrInv.Inventory[_invtrNmbr].ItemQuantity;
            var quantityHold = _plrInv.ItemHolding.ItemQuantity;
            var quantityMax = _plrInv.ItemHolding.Item.MaxQUantityPerStack;

            while (quantitySlot < quantityMax && quantityHold > 0)
            {
                quantitySlot++;
                quantityHold--;
            }
            _plrInv.Inventory[_invtrNmbr].ItemQuantity = quantitySlot;
            _plrInv.ItemHolding.ItemQuantity = quantityHold;
        }
        else if (_plrInv.ItemHolding.Item.Name != _plrInv.Inventory[_invtrNmbr].Item.Name)
        {
            var temp = _plrInv.ItemHolding;
            _plrInv.ItemHolding = _plrInv.Inventory[_invtrNmbr];
            _plrInv.Inventory[_invtrNmbr] = temp;
        }
        _plrInv.OnSlotClick.Invoke();
    }

    private void ClickSlotRight()
    {
        if (_plrInv.ItemHolding.Item.Name == "Nothing")
        {
            _plrInv.ItemHolding.Item = _plrInv.Inventory[_invtrNmbr].Item;
            _plrInv.ItemHolding.ItemQuantity++;
            _plrInv.Inventory[_invtrNmbr].ItemQuantity--;
        }
        else if (_plrInv.ItemHolding.Item.Name == _plrInv.Inventory[_invtrNmbr].Item.Name)
        {
            var quantitySlot = _plrInv.Inventory[_invtrNmbr].ItemQuantity;
            var quantityHold = _plrInv.ItemHolding.ItemQuantity;
            var quantityMax = _plrInv.ItemHolding.Item.MaxQUantityPerStack;

            quantitySlot--;
            quantityHold++;

            _plrInv.Inventory[_invtrNmbr].ItemQuantity = quantitySlot;
            _plrInv.ItemHolding.ItemQuantity = quantityHold;
        }
        else if (_plrInv.ItemHolding.Item.Name != _plrInv.Inventory[_invtrNmbr].Item.Name)
        {
            return;
        }
        _plrInv.OnSlotClick.Invoke();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            ClickSlotLeft();
        else if (eventData.button == PointerEventData.InputButton.Right)
            ClickSlotRight();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _rTm.localScale = new Vector3(1.2f, 1.2f, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _rTm.localScale = Vector3.one;
    }

}