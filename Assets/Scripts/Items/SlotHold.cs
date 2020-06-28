using TMPro;
using UnityEngine;
using UnityEngine.UI;

// takes care of displaying slot in hold item while managin inventory

public class SlotHold : MonoBehaviour
{
    // this offset is the distance counter from the image
    [SerializeField]
    private Vector3 _offSet = new Vector3(30, -30, 0);

    private PlayerInventory _plrInv;
    private Image _img;
    private TextMeshProUGUI _counter;

    private void Awake()
    {
        _plrInv = GameObject.FindGameObjectWithTag("Inventory").GetComponent<PlayerInventory>();
        _img = GetComponent<Image>();
        _counter = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _plrInv.OnSlotClick.AddListener(UpdateImage);
    }

    private void Update()
    {
        var mousePos = Input.mousePosition;
        transform.position = mousePos + _offSet;
        UpdateImage();
    }

    public void UpdateImage()
    {
        if (_plrInv.ItemHolding.Item.Name == "Nothing")
        {
            _img.color = Color.clear;
            _counter.color = Color.clear;
        }
        else
        {
            _img.color = Color.white;
            _counter.color = Color.white;
            _img.sprite = _plrInv.ItemHolding.Item.Image;
            _counter.text = _plrInv.ItemHolding.ItemQuantity.ToString();
        }
    }
}