using System;
using UnityEngine;
using UnityEngine.UI;

public class ItemInUse : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _invSlots = new GameObject[10];

    private PlayerInventory _plrInv;
    private bool _plrHasActiveInvSlot = true;
    private int _currentInvSlot = 0;
    private int SlotsInQuickBar = 10;
    private RectTransform _currentItemIconTransform;
    private Image _iconImage;

    private void Start()
    {
        _iconImage = GetComponent<Image>();
        _currentItemIconTransform = GetComponent<RectTransform>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
    }

    private void Update()
    {
        Testing();
        ShowIconOnlyOnSlotsWithItems();
    }

    private void UpdateCurrentInvSlot()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0)
            return;

        if (scroll > 0)
            do
            {
                _currentInvSlot++;
                if (_currentInvSlot > SlotsInQuickBar - 1)
                    _currentInvSlot = 0;
                else if (_currentInvSlot < 0)
                    _currentInvSlot = 9;
            }
            while (_plrInv.Inventory[_currentInvSlot].Item.Name == "Nothing");

        else if (scroll < 0)
            do
            {
                _currentInvSlot--;
                if (_currentInvSlot > SlotsInQuickBar - 1)
                    _currentInvSlot = 0;
                else if (_currentInvSlot < 0)
                    _currentInvSlot = 9;
            }
            while (_plrInv.Inventory[_currentInvSlot].Item.Name == "Nothing");

        UpdateCurrentInvSlotImgPos();
    }

    private void UpdateCurrentInvSlotImgPos()
    {
        var transformToChange = TransformOfInvSlotWithNumber(_currentInvSlot);
        _currentItemIconTransform.anchoredPosition = transformToChange.anchoredPosition;
    }

    private RectTransform _currentSlotPos;

    private RectTransform TransformOfInvSlotWithNumber(int InventoryNumber)
    {
        foreach (var invSlot in _invSlots)
        {
            var invSlotNumber = Convert.ToInt16(invSlot.name.Substring(4));

            if (invSlotNumber == InventoryNumber)
            {
                _currentSlotPos = invSlot.GetComponent<RectTransform>();
                break;
            }
        }
        return _currentSlotPos;
    }

    private void ShowIconOnlyOnSlotsWithItems()
    {
        if (_plrInv.InvFastBarIsEmpty())
            _plrHasActiveInvSlot = false;
        else
            _plrHasActiveInvSlot = true;

        if (!_plrHasActiveInvSlot)
        {
            _iconImage.color = new Color(0, 0, 0, 0);
            return;
        }
        _iconImage.color = new Color(255, 255, 255, 255);
        UpdateCurrentInvSlot();
    }

    private void Testing()
    {
       
    }
}