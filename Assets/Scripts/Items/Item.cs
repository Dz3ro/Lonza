using UnityEngine;

public enum ItemType
{
    Axe, Pickaxe, Hoe, None,  
}
public enum ItemCategory
{
    Tool, CraftingMaterialOnly, Collectable,Weapon, None
}

public class Item 
{
    public string Name { get; set; }
    public ItemCategory Category { get; set; }
    public ItemType Type { get; set; }
    public int Damage { get; set; }
    public int Level { get; set; }
    public Sprite Image { get; set; }
    public int MaxQUantityPerStack { get; set; }
    public GameObject VrsCollectable { get; set; }
    public GameObject VrsPickable { get; set; }

    public Item()
    {
        Name = "Nothing";
        Category = ItemCategory.None;
        Type = ItemType.None;
        Damage = 0;
        Level = 0;
        MaxQUantityPerStack = 99;
        VrsCollectable = null;
        VrsPickable = null;
    }

    public bool ThisIsANewEmptyItem()
    {
        var x = new Item();
        if (this.Name != x.Name || this.Category != x.Category ||
            this.Type != x.Type || 
            this.MaxQUantityPerStack != x.MaxQUantityPerStack)
            return false;
        return true;
    }

    
}