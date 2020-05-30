using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllItems : MonoBehaviour
{
    [SerializeField]
    private Sprite SpriteRock;

    public Item Rock = new Item();
    void Start()
    {
        CreateAllItems();
    }

    private void CreateAllItems()
    {
        CreateRock();
    }

    private void CreateRock()
    {
        Rock.Name = "Rock";
        Rock.Category = "Material";
        Rock.Type = "Stone";
        Rock.MaxQUantityPerStack = 16;
        Rock.Image = SpriteRock;

    }
}
