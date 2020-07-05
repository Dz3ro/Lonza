using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// in this script u create all the recipes and categories

public class RecipesAndCategoriesCreator : MonoBehaviour
{
    // Sprites for categories
    public Sprite CategoryImagePickaxe;
    public Sprite CategoryImagePotion;
    public Sprite CategoryImageFurnace;
    public Sprite CategoryImagePan;
    public Sprite CategoryImageScroll;
    //
    public List<CraftCategory> CraftCategories { get; private set; }
    public List<GameObject> CraftCategoriesObjects;

    // this ints are for auto use whrn categories are created
    // never meant to to set or get
    public static int _catCounter = 0;
    public static int AmountOfCategoriesCreated = 0;
    //

    private ItemsCreator _allItems;

    private string _catNameEquip = "Equipment";
    private string _catNameAlch = "Alchemy";
    private string _catNameStruct = "Structures";
    private string _catNameCook = "Cooking";
    private string _catNameEnchant = "Enchanting";

    private void Awake()
    {
        _allItems = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsCreator>();

        CreateCategories();
    }

    private void Start()
    {
        CreateRecipes();
    }

    private void Update()
    {
    }

    private void CreateCategories()
    {
        void AddCategory(string CategoryName, Sprite CategoryPicture)
        {
            var category = new CraftCategory(CategoryName, CategoryPicture);
            CraftCategories.Add(category);
        }

        CraftCategories = new List<CraftCategory>();

        AddCategory(_catNameEquip, CategoryImagePickaxe);
        AddCategory(_catNameAlch, CategoryImagePotion);
        AddCategory(_catNameStruct, CategoryImageFurnace);
        AddCategory(_catNameCook, CategoryImagePan);
        AddCategory(_catNameEnchant, CategoryImageScroll);
    }

    private void CreateRecipes()
    {
        CreateRecipe(_allItems.ToolBinding, 1, _catNameEquip,
            Tuple.Create(_allItems.Bush, 4));

        CreateRecipe(_allItems.ToolHandle, 1, _catNameEquip,
            Tuple.Create(_allItems.Stick, 3));

        CreateRecipe(_allItems.Pickaxe, 1, _catNameEquip,
            Tuple.Create(_allItems.ToolBinding, 1),
            Tuple.Create(_allItems.ToolHandle, 1),
            Tuple.Create(_allItems.Rock, 5));

        CreateRecipe(_allItems.Axe, 1, _catNameEquip,
            Tuple.Create(_allItems.ToolBinding, 1),
            Tuple.Create(_allItems.ToolHandle, 1),
            Tuple.Create(_allItems.Rock, 4));

        CreateRecipe(_allItems.Hoe, 1, _catNameEquip,
            Tuple.Create(_allItems.ToolBinding, 1),
            Tuple.Create(_allItems.ToolHandle, 1),
            Tuple.Create(_allItems.Rock, 2));
    }

    private void CreateRecipe(string recipeName, Item product,
        int productAmount, string categoryName,
        params Tuple<Item, int>[] materials)
    {
        new Recipe(recipeName, product, productAmount,
           CraftCategories.FirstOrDefault(x => x.Name == categoryName),
           materials);
    }

    private void CreateRecipe(Item product,
        int productAmount, string categoryName,
        params Tuple<Item, int>[] materials)
    {
        new Recipe(product.Name, product, productAmount,
           CraftCategories.FirstOrDefault(x => x.Name == categoryName),
           materials);
    }
}