using UnityEngine;

public class Item
{
    public string Name { get; set; }
    public string Category { get; set; }
    public string Type { get; set; }
    public Sprite Image { get; set; }
    public int MaxQUantityPerStack { get; set; }

    public Item()
    {
        Name = "Nothing";
        Category = "none";
        Type = "none";
        MaxQUantityPerStack = 99;
    }
}