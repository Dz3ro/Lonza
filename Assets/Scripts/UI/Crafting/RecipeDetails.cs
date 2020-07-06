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

        if (rec.Materials.Count == 0)
            return;

        if (rec.Materials[0] != null)
        {
            var mat = rec.Materials[0];
            var cost = mat.Item2;
            var costString = cost.ToString();
            var textCost = "0/" +  costString;

            Material0.SetActive(true);
            _mat0Name.text = mat.Item1.Name;
            _mat0Img.sprite = mat.Item1.Image;
            _mat0Cost.text = textCost;
        }

        if (rec.Materials.Count == 1)
            return;

        if (rec.Materials[1] != null)
        {
            var mat = rec.Materials[1];
            var cost = mat.Item2;
            var costString = cost.ToString();
            var textCost = "0/" + costString;

            Material1.SetActive(true);
            _mat1Name.text = mat.Item1.Name;
            _mat1Img.sprite = mat.Item1.Image;
            _mat1Cost.text = textCost;
        }

        if (rec.Materials.Count == 2)
            return;

        if (rec.Materials[2] != null)
        {
            var mat = rec.Materials[2];
            var cost = mat.Item2;
            var costString = cost.ToString();
            var textCost = "0/" + costString;

            Material2.SetActive(true);
            _mat2Name.text = mat.Item1.Name;
            _mat2Img.sprite = mat.Item1.Image;
            _mat2Cost.text = textCost;
        }

    }

    





}
