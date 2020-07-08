using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButtonsManager : MonoBehaviour
{

    private RecipesAndCategoriesCreator _catMng;
    private RecipesManager _recMng;
    private RecipeDetails _recDtls;
    private ItemsAddRemoveSearch _itmGod;
    private PlayerInventory _plrInv;

    private void Awake()
    {
        _catMng = GetComponent<RecipesAndCategoriesCreator>();
        _recMng = GetComponent<RecipesManager>();
        _recDtls = GetComponent<RecipeDetails>();
        _itmGod = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsAddRemoveSearch>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>();
    }
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void CategorySwitchButtonRight()
    {
        CategorySwitchButton(true);
    }
    public void CategorySwitchButtonLeft()
    {
        CategorySwitchButton(false);
    }
    public void ScrollThroughRecipesUp()
    {
        _recMng.RecipesToShow--;
    }
    public void ScrollThroughRecipesDown()
    {
        _recMng.RecipesToShow++;
    }
    public void RecipeButtonUpper()
    {
        RecipeButton(0);
    }
    public void RecipeButtonMiddle()
    {
        RecipeButton(1);
    }
    public void RecipeButtonLower()
    {
        RecipeButton(2);
    }    
    public void CreateButton()
    {
        var recipe = _recDtls.SelectedRecipeShow();
        var product = recipe.Product.Item1;
        var productQuant = recipe.Product.Item2;

        if( ! _itmGod.ItemCanBePutInHand(product, productQuant))
            return;
        if (!HaveAllMaterialsToCraft(recipe))
            return;
        RemoveMaterialsFromInventory(recipe);
        _itmGod.ItemPutIntoHand(product, productQuant);
    }

    private bool HaveAllMaterialsToCraft(Recipe recipe)
    {
        foreach (var item in recipe.Materials)
        {
            var inBag = _itmGod.ItemQuantityInInventory(item.Item1);
            if (inBag < item.Item2)
                return false;
        }
        return true;
    }
    private void RemoveMaterialsFromInventory(Recipe recipe)
    {
        foreach (var item in recipe.Materials)
            _itmGod.ItemRemove(item.Item1, item.Item2);
    }


    private void RecipeButton(int number)
    {
        var recipeSelected = _recMng.SelectRecipe(number);
        _recDtls.SetSelectedRecipe(recipeSelected);

    }
    private void CategorySwitchButton(bool goRight)
    {
        var categories = _catMng.CraftCategories;

        foreach (var cat in categories)
            if (goRight)
                cat.CurrentPosition++;
            else
                cat.CurrentPosition--;

        foreach (var cat in _catMng.CraftCategoriesObjects)
            cat.GetComponent<CraftCategoryVisuals>().ChangeCraftImage();

        _recMng.SetRecipeVisuals();
        _recDtls.SetSelectedRecipe(null);
        //_recDtls.SetVisuals();
    }


}
