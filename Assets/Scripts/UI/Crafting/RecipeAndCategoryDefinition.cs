using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script is a class definition holder

public class Recipe
{
    public string Name { get; set; }
    public Tuple<Item, int> Product { get; set; }
    public List<Tuple<Item, int>> Materials { get; set; }

    public Recipe(string recipeName, Item craftProduct,
        int productAmount, CraftCategory craftCategory, params Tuple<Item, int>[] materials)
    {
        if (String.IsNullOrEmpty(recipeName))
            this.Name = craftProduct.Name;
        else
            this.Name = recipeName;

        this.Product = Tuple.Create(craftProduct, productAmount);

        this.Materials = new List<Tuple<Item, int>>();

        foreach (var material in materials)
            this.Materials.Add(material);

        craftCategory.Recipes.Add(this);

    }
}
public class CraftCategory
{
    public Sprite Image { get; set; }
    public string Name { get; set; }

    public int CurrentPosition
    {
        get { return _currentPos; }
        set
        {
            _currentPos = value;
            if (_currentPos > RecipesAndCategoriesCreator._catCounter - 1)
                _currentPos = 0;
            else if (_currentPos < 0)
                _currentPos = RecipesAndCategoriesCreator._catCounter - 1;

            if (_currentPos == 0)
                IsSelected = true;
            else
                IsSelected = false;
        }
    }

    public bool IsSelected { get; private set; }
    public List<Recipe> Recipes { get; set; }

    private int _currentPos;

    public CraftCategory(string CategoryName, Sprite CategoryImage)
    {
        RecipesAndCategoriesCreator._catCounter++;
        CurrentPosition = RecipesAndCategoriesCreator._catCounter - 1;
        Recipes = new List<Recipe>();
        Name = CategoryName;
        Image = CategoryImage;
    }
}

public class RecipeAndCategoryDefinition : MonoBehaviour
{
        
}
