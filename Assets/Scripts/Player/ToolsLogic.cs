using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsLogic : MonoBehaviour
{
    [SerializeField] private GameObject DirtTilled;

    private PlayerInventory _plrInv;
    private PlayerFacing _plrFac;
    private Movement _plrMov;
    private GameObject _plr;
    private Item _equip;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _plrInv = GameObject.FindGameObjectWithTag("Inventory")
           .GetComponent<PlayerInventory>();
        _plrFac = _plr.GetComponentInChildren<PlayerFacing>();
        _plrMov = _plr.GetComponent<Movement>();

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _equip = _plrInv.ItemEquiped.Item;
    }

    public void UseTool(Item item)
    {
        if (item.Category != "Tool")
            return;
        if (item.Name == "Hoe")
            UseHoe();
        else 
            UseAxeAndPickaxe();
    }
    private void UseHoe()
    {
        if (! _plrFac.FacingEmptyTileAbleToTill())
            return;
        Instantiate(DirtTilled, 
            _plrFac.GetPlayerFacingPosition(),
            Quaternion.identity);

    }
    private void UseAxeAndPickaxe()
    {
        _plrFac.UseTool(_equip);
    }

}
