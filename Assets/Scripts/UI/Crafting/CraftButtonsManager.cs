using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButtonsManager : MonoBehaviour
{

    private RecipesAndCategoriesCreator _catMng;
    private RecipesManager _recMng;
    private RecipeDetails _recDtls;


    private void Awake()
    {
        _catMng = GetComponent<RecipesAndCategoriesCreator>();
        _recMng = GetComponent<RecipesManager>();
        _recDtls = GetComponent<RecipeDetails>();
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

    private void RecipeButton(int number)
    {
        var recipeSelected = _recMng.SelectRecipe(number);
        _recDtls.SetSelectedRecipe(recipeSelected);
        _recDtls.SetVisuals();
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
        _recDtls.SetVisuals();
    }


}
