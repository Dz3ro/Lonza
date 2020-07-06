using System;
using UnityEngine;
using UnityEngine.UI;

// this script is for the 5 gameobjects showing craftcategories on list
public class CraftCategoryVisuals : MonoBehaviour
{
    private RecipesAndCategoriesCreator _catMng;
    private int _craftPosNumber;
    private Image _myImg;

    private void Awake()
    {
        _catMng = transform.parent.gameObject
            .GetComponent<RecipesAndCategoriesCreator>();
        _myImg = GetComponent<Image>();
    }

    private void Start()
    {
        _craftPosNumber = Convert.ToInt16
            (gameObject.name.Substring(gameObject.name.Length - 1));
        ChangeCraftImage();
    }

    private void Update()
    {
    }

    public void ChangeCraftImage()
    {
        var craftCategories = _catMng.CraftCategories;
        foreach (var cat in craftCategories)
        {
            if (cat.CurrentPosition == _craftPosNumber)
            {
                _myImg.sprite = cat.Image;
                return;
            }
        }
        
    }
}