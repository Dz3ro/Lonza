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
    private readonly int SlotsInQuickBar = 10;
    private RectTransform _slotEquipedPos;
    private Image _slotImage;
    private RectTransform _currentSlotPos;
    private Controls _ctrls;
    private Movement _plrMov;

    private void Awake()
    {
        _slotImage = GetComponent<Image>();
        _slotEquipedPos = GetComponent<RectTransform>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>();

        var player = GameObject.FindGameObjectWithTag("Player");

        _ctrls = player.GetComponent<Controls>();
        _plrMov = player.GetComponent<Movement>();
    }

    

    private void Update()
    {
        SlotEquipedBehaviour();
        //SetEquipedSlotInInventoryScript();
    }


    public Item GetEquipedItem()
    {
        var item = _plrInv.Inventory[_currentInvSlot].Item;

        return item;
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
        _plrInv.ItemEquiped = _plrInv.Inventory[_currentInvSlot];
    }
    private void MoveSlotEquipedByPlayer()
    {
        if (_plrMov.TakingAction)
            return;

        if (_ctrls.ScrollUp)
            SlotEquipedSetupLogic(1);
        else if (_ctrls.ScrollDown)
            SlotEquipedSetupLogic(-1);
    }
    private void MoveSlotEquipedAuto()
    {
        if (! _plrInv.Inventory[_currentInvSlot].Item.ThisIsANewEmptyItem())
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
            while (_plrInv.Inventory[_currentInvSlot].Item.ThisIsANewEmptyItem());

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
            while (_plrInv.Inventory[_currentInvSlot].Item.ThisIsANewEmptyItem());

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