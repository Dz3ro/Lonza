using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemOnBar : MonoBehaviour,
    IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas _canvas;

    private PlayerInventory _plrInv;
    private int _invtrNmbr;
    private Image _image;
    private TextMeshProUGUI _itemCount;
    private RectTransform _rectTransform;
    private CanvasGroup _canvasGroup;

    private void Start()
    {
        _invtrNmbr = Convert.ToInt16(transform.parent.name.ToString().Substring(4));
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
        _itemCount = gameObject.GetComponentInChildren<TextMeshProUGUI>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
        _canvas = GameObject.FindGameObjectWithTag("Canvas").GetComponent<Canvas>();
        _canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();
    }

    private void Update()
    {
        ShowItem();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup = gameObject.GetComponentInParent<CanvasGroup>();

        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = 0.5f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;
        _rectTransform.anchoredPosition = Vector3.zero;
    }

    private void ShowItem()
    {
        _invtrNmbr = Convert.ToInt16(transform.parent.name.ToString().Substring(4));

        var inventoryItem = _plrInv.Inventory[_invtrNmbr];

        if (inventoryItem.Item.Name == "Nothing")
        {
            _image.color = Color.clear;
            _itemCount.color = Color.clear;
        }
        else if (inventoryItem.Item.Name != "Nothing")
        {
            _image.color = Color.white;
            _image.sprite = inventoryItem.Item.Image;
            _itemCount.color = Color.black;
            _itemCount.text = inventoryItem.ItemQuantity.ToString();
        }
    }
}