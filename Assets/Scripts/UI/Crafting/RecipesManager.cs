using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipesManager : MonoBehaviour
{
    public GameObject Recipe0;
    public GameObject Recipe1;
    public GameObject Recipe2;

    private TextMeshProUGUI _rec0Txt;
    private Image _rec0Img;

    private TextMeshProUGUI _rec1Txt;
    private Image _rec1Img;

    private TextMeshProUGUI _rec2Txt;
    private Image _rec2Img;

    private RecipesAndCategoriesCreator _craftMng;

    private RecipeDetails _recDtls;

    public int RecipesToShow
    {
        get { return _recipesToShow; }
        set
        {
            _recipesToShow = value;
            if (_recipesToShow < 0)
                _recipesToShow = 0;
            else if (_recipesToShow + 3 > GetRecipesOfSelectedCategory().Count)
                _recipesToShow = value - 1; //GetRecipesOfSelectedCategory().Count;
            SetRecipeVisuals();
        }
    }


    private int _recipesToShow = 0;

    private void Awake()
    {
        _rec0Txt = Recipe0.GetComponentInChildren<TextMeshProUGUI>();
        _rec0Img = Recipe0.GetComponentInChildren<Image>();
        _rec1Txt = Recipe1.GetComponentInChildren<TextMeshProUGUI>();
        _rec1Img = Recipe1.GetComponentInChildren<Image>();
        _rec2Txt = Recipe2.GetComponentInChildren<TextMeshProUGUI>();
        _rec2Img = Recipe2.GetComponentInChildren<Image>();

        _craftMng = GetComponent<RecipesAndCategoriesCreator>();

        _recDtls = GetComponent<RecipeDetails>();
    }

    private void Start()
    {
        _rec0Img.preserveAspect = true;
        _rec1Img.preserveAspect = true;
        _rec2Img.preserveAspect = true;

        LateVisualsSet(0.01f);
    }

    private void Update()
    {

    }

    private void MoveCategories()
    {
        var categories = _craftMng.CraftCategories;

        foreach (var cat in categories)
                cat.CurrentPosition++;
           

        foreach (var cat in _craftMng.CraftCategoriesObjects)
            cat.GetComponent<CraftCategoryVisuals>().ChangeCraftImage();

        SetRecipeVisuals();
        _recDtls.SetSelectedRecipe(null);
        //_recDtls.SetVisuals();
    }

    private void LateVisualsSet(float delay)
    {
        IEnumerator Wait()
        {
            yield return new WaitForSecondsRealtime(delay);
            SetRecipeVisuals();
        }
        StartCoroutine(Wait());
    }

    

    public List<Recipe>
        GetRecipesOfSelectedCategory()
    {
        var selectedCategory = _craftMng.CraftCategories
            .SingleOrDefault(x => x.IsSelected == true);

        var recipes = selectedCategory.Recipes;

        return recipes;
    }

    //public void ScrollThroughRecipes(bool moveForward)
    //{
    //    if (moveForward == true)
    //        RecipesToShow++;
    //    else
    //        RecipesToShow--;
    //    SetRecipeVisuals();
    //}

    public void SetRecipeVisuals()
    {
        var recipes = GetRecipesOfSelectedCategory();


        Recipe0.SetActive(false);
        Recipe1.SetActive(false);
        Recipe2.SetActive(false);

        if (recipes.Count == 0)
            return;

        var recipeFirst = recipes[0 + _recipesToShow];
        var recipeSecond = recipes[1 + _recipesToShow];
        var recipeThird = recipes[2 + _recipesToShow];


        if (recipeFirst != null)
        {
            Recipe0.SetActive(true);
            _rec0Txt.text = recipeFirst.Name;
            _rec0Img.sprite = recipeFirst.Product.Item1.Image;
        }
        if (recipeSecond != null)
        {
            Recipe1.SetActive(true);
            _rec1Txt.text = recipeSecond.Name;
            _rec1Img.sprite = recipeSecond.Product.Item1.Image;
        }
        if (recipeThird != null)
        {
            Recipe2.SetActive(true);
            _rec2Txt.text = recipeThird.Name;
            _rec2Img.sprite = recipeThird.Product.Item1.Image;
        }




        //_rec1Txt.text = recipes[1 + _recipesToShow].Name;
        //_rec1Img.sprite = recipes[1 + _recipesToShow].Product.Item1.Image;

        //_rec2Txt.text = recipes[2 + _recipesToShow].Name;
        //_rec2Img.sprite = recipes[2 + _recipesToShow].Product.Item1.Image;
    }

    private Recipe _selectedRecipe;

    public Recipe SelectRecipe(int selectedRecipe)
    {
        var recipes = GetRecipesOfSelectedCategory();

        var selectedRecipeFinal = recipes[selectedRecipe + _recipesToShow];

        return selectedRecipeFinal;
    }

}