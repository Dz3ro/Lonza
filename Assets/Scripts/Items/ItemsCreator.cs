using System.Collections.Generic;
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

    public Item Rock = new Item();
    public Item Stick = new Item();
    public Item Bush = new Item();
    public Item ToolHandle = new Item();
    public Item ToolBinding = new Item();
    public Item Pickaxe = new Item();
    public Item Axe = new Item();
    public Item Hoe = new Item();
    public Item FishingRod = new Item();



    private void Awake()
    {
        CreateAllItems();
    }

   

    private void CreateAllItems()
    {
        CreateItem(Rock, "Rock", "Material", "Stone", 16, SpriteRock);
        CreateItem(Stick, "Stick", "Material", "Wood", 16, SpriteStick);
        CreateItem(Bush, "Bush", "Material", "Grass", 16, SpriteBush);
        CreateItem(ToolHandle, "Tool Handle", "Material", "Wood", 16, SpriteToolHandle);
        CreateItem(ToolBinding, "Tool Binding", "Material", "Wood", 16, SpriteToolBinding);
        CreateItem(Pickaxe, "Pickaxe", "Tool", "Stone", 1, SpritePickaxe);
        CreateItem(Axe, "Axe", "Tool", "Stone", 1, SpriteAxe);
        CreateItem(Hoe, "Hoe", "Tool", "Stone", 1, SpriteHoe);
        CreateItem(FishingRod, "Fishing Rod", "Tool", "Wood", 1, SpriteFishingRod);

    }

    private void CreateItem(Item Item, string Name, string Category, string Type,
        int MaxQuant, Sprite Image)
    {
        Item.Name = Name;
        Item.Category = Category;
        Item.Type = Type;
        Item.MaxQUantityPerStack = MaxQuant;
        Item.Image = Image;
        ItemsList.Add(Item);
    }
}