﻿using System.Collections.Generic;
using UnityEngine;

public class ItemsCreator : MonoBehaviour
{
    public Sprite SpriteRock;
    public Sprite SpriteStick;
    public Sprite SpriteBush;
    public Sprite SpriteToolHandle;
    public Sprite SpriteToolBinding;
    public Sprite SpritePickaxe;
    public Sprite SpriteAxe;
    public Sprite SpriteHoe;
    public Sprite SpriteFishingRod;


    public List<Item> ItemsList = new List<Item>();

    /// <summary>
    /// //list of all items 
    /// </summary>
    public Item Rock = new Item();
    public Item Stick = new Item();
    public Item Bush = new Item();
    public Item ToolHandle = new Item();
    public Item ToolBinding = new Item();
    public Item Pickaxe = new Item();
    public Item Axe = new Item();
    public Item Hoe = new Item();
    public Item FishingRod = new Item();
    ///////


    /// <summary>
    /// all items in version pickable
    /// </summary>
    [SerializeField] private GameObject PickableRock = null;
    [SerializeField] private GameObject PickableStick = null;
    [SerializeField] private GameObject PickableBush = null;
    [SerializeField] private GameObject PickableToolHandle = null;
    [SerializeField] private GameObject PickableToolBinding = null;
    [SerializeField] private GameObject PickablePickaxe = null;
    [SerializeField] private GameObject PickableAxe = null;
    [SerializeField] private GameObject PickableHoe = null;
    [SerializeField] private GameObject PickableFishingRod = null;
    //////

    /// <summary>
    /// all items in version collectable 
    /// </summary>
    [SerializeField] private GameObject CollectableRock = null;
    [SerializeField] private GameObject CollectableStick = null;
    [SerializeField] private GameObject CollectableBush = null;
    [SerializeField] private GameObject CollectableToolHandle = null;
    [SerializeField] private GameObject CollectableToolBinding = null;
    [SerializeField] private GameObject CollectablePickaxe = null;
    [SerializeField] private GameObject CollectableAxe = null;
    [SerializeField] private GameObject CollectableHoe = null;
    [SerializeField] private GameObject CollectableFishingRod = null;
    /////



    private void Awake()
    {
        CreateAllItems();
    }

   

    private void CreateAllItems()
    {
        CreateItem(Rock, "Rock", "Material", "Stone", 16, SpriteRock, CollectableRock, PickableRock);
        CreateItem(Stick, "Stick", "Material", "Wood", 16, SpriteStick, CollectableStick, PickableStick);
        CreateItem(Bush, "Bush", "Material", "Grass", 16, SpriteBush, CollectableBush, PickableBush);
        CreateItem(ToolHandle, "Tool Handle", "Material", "Wood", 16, SpriteToolHandle);
        CreateItem(ToolBinding, "Tool Binding", "Material", "Wood", 16, SpriteToolBinding);
        CreateItem(Pickaxe, "Pickaxe", "Tool", "Stone", 1, SpritePickaxe);
        CreateItem(Axe, "Axe", "Tool", "Stone", 1, SpriteAxe);
        CreateItem(Hoe, "Hoe", "Tool", "Stone", 1, SpriteHoe);
        CreateItem(FishingRod, "Fishing Rod", "Tool", "Wood", 1, SpriteFishingRod);

    }

    private void CreateItem(Item item, string name, string category, string type,
        int maxQuant, Sprite image)
    {
        item.Name = name;
        item.Category = category;
        item.Type = type;
        item.MaxQUantityPerStack = maxQuant;
        item.Image = image;
        ItemsList.Add(item);
    }

    private void CreateItem(Item item, string name, string category, string type,
        int maxQuant, Sprite image, GameObject vrsCollectable,
        GameObject vrsPickable)
    {
        item.Name = name;
        item.Category = category;
        item.Type = type;
        item.MaxQUantityPerStack = maxQuant;
        item.Image = image;
        item.VrsCollectable = vrsCollectable;
        item.VrsPickable = vrsPickable;
        ItemsList.Add(item);
       
    }


}