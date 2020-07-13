using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DirtToTill : ObjectInteractiveWithTool
{
    private ItemsCreator _allItems;

    private Tilemap _tMap;
    private GameObject _plrFac;

    [SerializeField] private TileBase DirtTiled;


    private void Awake()
    {
        _plrFac = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<PlayerFacing>().gameObject;
        _allItems = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsCreator>();
        _tMap = GetComponent<Tilemap>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public override void OnToolUse(Item item)
    {
        if (item != _allItems.Hoe)
            return;
        print("ahi clicked with hoe");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        

        


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!Input.GetKeyDown("t"))
            return;

        if (collision.gameObject != _plrFac)
            return;

        var posX = Mathf.RoundToInt(_plrFac.transform.position.x);
        var posY = Mathf.CeilToInt(_plrFac.transform.position.y);

        var vector = new Vector3Int(posX, posY, 0);

        _tMap.SetTile(vector, DirtTiled);
        print(_plrFac.transform.position.x + " " + _plrFac.transform.position.y);
        print("put a tile in a " + posX + " " + posY);

    }
}
