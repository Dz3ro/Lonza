using System.Collections.Generic;
using UnityEngine;

public class ItemsCreator : MonoBehaviour
{
    public Sprite SpriteRock;
    public Sprite SpriteStick;

    public List<Item> ItemsList = new List<Item>();

    public Item Rock = new Item();
    public Item Stick = new Item();

    private void Awake()
    {
        CreateAllItems();
    }

    private void Start()
    {

    }

    private void CreateAllItems()
    {
        CreateItem(Rock, "Rock", "Material", "Stone", 16, SpriteRock);
        CreateItem(Stick, "Stick", "Material", "Wood", 16, SpriteStick);
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