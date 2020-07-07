using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDetails : MonoBehaviour
{
    public GameObject RecipeDetailsWindow;

    public GameObject Product;
    public GameObject Material0;
    public GameObject Material1;
    public GameObject Material2;


    private TextMeshProUGUI _prodName;
    private Image _prodImg;

    private TextMeshProUGUI _mat0Name;
    private Image _mat0Img;
    private TextMeshProUGUI _mat0Cost;

    private TextMeshProUGUI _mat1Name;
    private Image _mat1Img;
    private TextMeshProUGUI _mat1Cost;

    private TextMeshProUGUI _mat2Name;
    private Image _mat2Img;
    private TextMeshProUGUI _mat2Cost;

    private ItemsAddRemoveSearch _itmGod;

    private string _gObjNameOfName = "Name";
    private string _gObjNameOfCost = "Cost";

    private Recipe _recipeSelected;



    private void Awake()
    {
        _prodName = Product.transform.Find(_gObjNameOfName)
            .GetComponent<TextMeshProUGUI>();
        _prodImg = Product.GetComponentInChildren<Image>();

        _mat0Name = Material0.transform.Find(_gObjNameOfName)
            .GetComponent<TextMeshProUGUI>();
        _mat0Cost = Material0.transform.Find(_gObjNameOfCost)
            .GetComponent<TextMeshProUGUI>();
        _mat0Img = Material0.GetComponentInChildren<Image>();

        _mat1Name = Material1.transform.Find(_gObjNameOfName)
            .GetComponent<TextMeshProUGUI>();
        _mat1Cost = Material1.transform.Find(_gObjNameOfCost)
            .GetComponent<TextMeshProUGUI>();
        _mat1Img = Material1.GetComponentInChildren<Image>();

        _mat2Name = Material2.transform.Find(_gObjNameOfName)
            .GetComponent<TextMeshProUGUI>();
        _mat2Cost = Material2.transform.Find(_gObjNameOfCost)
            .GetComponent<TextMeshProUGUI>();
        _mat2Img = Material2.GetComponentInChildren<Image>();

        _itmGod = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsAddRemoveSearch>();

    }

    public void Start()
    {
        _prodImg.preserveAspect = true;
        _mat0Img.preserveAspect = true;
        _mat1Img.preserveAspect = true;
        _mat2Img.preserveAspect = true;
        SetVisuals();
    }

    void Update()
    {
        
    }

    public void SetSelectedRecipe(Recipe recipe)
    {
        _recipeSelected = recipe;
    }

    public Recipe SelectedRecipeShow()
    {
        return _recipeSelected;
    }

    public void SetVisuals()
    {
        var rec = _recipeSelected;

        Material0.SetActive(false);
        Material1.SetActive(false);
        Material2.SetActive(false);
        RecipeDetailsWindow.SetActive(false);

        if (rec == null)
            return;

        RecipeDetailsWindow.SetActive(true);

        _prodName.text = rec.Product.Item1.Name;
        _prodImg.sprite = rec.Product.Item1.Image;

        SetVisualsMaterial(0, Material0, _mat0Name, _mat0Cost, _mat0Img);
        SetVisualsMaterial(1, Material1, _mat1Name, _mat1Cost, _mat1Img);
        SetVisualsMaterial(2, Material2, _mat2Name, _mat2Cost, _mat2Img);
    }

    private void SetVisualsMaterial(int matNumber, GameObject matWindow,
        TextMeshProUGUI matName, TextMeshProUGUI matCost,
        Image matImg)
    {

        var rec = _recipeSelected;

        if (rec.Materials.Count <= matNumber)
            return;

        if (rec.Materials[matNumber] != null)
        {
            var mat = rec.Materials[matNumber];
            var cost = mat.Item2;
            var costString = cost.ToString();
            var materialsHave = CheckMaterials(matNumber);
            var textCost = materialsHave + "/" + costString;

            matWindow.SetActive(true);
            matImg.sprite = mat.Item1.Image;


            matName.text = mat.Item1.Name;
            matCost.text = textCost;

            var haveMaterials = materialsHave >= cost;
            var colorWhenHaveItems = Color.green;
            var colorWhenMissingItems = new Color32(120,120,120, 50);

            if (haveMaterials)
            {
                matName.color = colorWhenHaveItems;
                matCost.color = colorWhenHaveItems;
            }
            else
            {
                print("Ahi");
                matName.color = colorWhenMissingItems;
                matCost.color = colorWhenMissingItems;
            }
        }
    }

    private int CheckMaterials(int materialsNumber)
    {
        var mat = _recipeSelected.Materials[materialsNumber].Item1;
        var number = _itmGod.ItemQuantityInInventory(mat);

        return number;
    }







}
