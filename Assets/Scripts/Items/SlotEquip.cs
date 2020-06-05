using System;
using UnityEngine;
using UnityEngine.UI;

// This script takes care for the equiped inventory slot behaviour

public class SlotEquip : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _invPartSlots = new GameObject[10];

    private PlayerInventory _plrInv;
    private bool _plrHasActiveInvSlot = true;
    private int _currentInvSlot = 0;
    private int SlotsInQuickBar = 10;
    private RectTransform _slotEquipedPos;
    private Image _slotImage;
    private RectTransform _currentSlotPos;

    private void Awake()
    {
        _slotImage = GetComponent<Image>();
        _slotEquipedPos = GetComponent<RectTransform>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        SlotEquipedBehaviour();
    }

    private void SlotEquipedBehaviour()
    {
        if (_plrInv.AllSlotsInInvertoryPartAreEmpty())
            _plrHasActiveInvSlot = false;
        else
            _plrHasActiveInvSlot = true;

        if (!_plrHasActiveInvSlot)
        {
            _slotImage.color = Color.clear;
            return;
        }
        _slotImage.color = Color.white;
        MoveSlotEquipedByPlayer();
        MoveSlotEquipedAuto();
    }

    private void MoveSlotEquipedByPlayer()
    {
        var scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll == 0)
            return;
        if (scroll > 0)
            SlotEquipedSetupLogic(1);
        else if (scroll < 0)
            SlotEquipedSetupLogic(-1);
    }

    private void MoveSlotEquipedAuto()
    {
        if (_plrInv.Inventory[_currentInvSlot].Item.Name != "Nothing")
            return;
        SlotEquipedSetupLogic(1);
    }

    private void SlotEquipedSetupLogic(int direction)
    {
        if (_plrInv.AllSlotsInInvertoryPartAreEmpty())
            return;

        if (direction > 0)
        {
            do
            {
                _currentInvSlot++;
                if (_currentInvSlot > SlotsInQuickBar - 1)
                    _currentInvSlot = 0;
                else if (_currentInvSlot < 0)
                    _currentInvSlot = 9;
            }
            while (_plrInv.Inventory[_currentInvSlot].Item.Name == "Nothing");
        }
        else if (direction < 0)
        {
            do
            {
                _currentInvSlot--;
                if (_currentInvSlot > SlotsInQuickBar - 1)
                    _currentInvSlot = 0;
                else if (_currentInvSlot < 0)
                    _currentInvSlot = 9;
            }
            while (_plrInv.Inventory[_currentInvSlot].Item.Name == "Nothing");
        }

        MoveImageToPosition();
    }

    private void MoveImageToPosition()
    {
        var transformToChange = FindPositionOfSlot(_currentInvSlot);
        _slotEquipedPos.anchoredPosition = transformToChange.anchoredPosition;
    }

    private RectTransform FindPositionOfSlot(int SlotNumber)
    {
        foreach (var invSlot in _invPartSlots)
        {
            var invSlotNumber = Convert.ToInt16(invSlot.name.Substring(4));

            if (invSlotNumber == SlotNumber)
            {
                _currentSlotPos = invSlot.GetComponent<RectTransform>();
                break;
            }
        }
        return _currentSlotPos;
    }
}