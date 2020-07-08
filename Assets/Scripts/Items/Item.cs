using UnityEngine;

public class Item 
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
    public Sprite Image { get; set; }
    public int MaxQUantityPerStack { get; set; }
    public GameObject VrsCollectable { get; set; }
    public GameObject VrsPickable { get; set; }

    public Item()
    {
        Name = "Nothing";
        Category = "none";
        Type = "none";
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