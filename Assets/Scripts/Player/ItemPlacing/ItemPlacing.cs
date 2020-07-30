using System.Collections;
using UnityEngine;

public class ItemPlacing : MonoBehaviour
{
    private PlayerInventory _plrInv;
    private PlayerFacing _plrFac;
    private SpriteRenderer _sprRen;
    private KeyCode _actionMain;

    private Item _equip;
    private bool _tileIsEmpty;
    private Vector3 _plcPos;
    private bool _readyToPlace = false;

    private void Awake()
    {
        var player = GameObject.FindGameObjectWithTag("Player");

        _sprRen = GetComponent<SpriteRenderer>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<PlayerInventory>();
        _plrFac = player.GetComponentInChildren<PlayerFacing>();
        _actionMain = player.GetComponent<Controls>().ActionMain;
    }

    private void Start()
    {
    }

    private void Update()
    {
        StartCoroutine(WaitToGetReadyForItemPlacing());
        UpdateVarsFromOtherScripts();
        PlacingPreview();
        PlaceItem();
    }

    private void UpdateVarsFromOtherScripts()
    {
        _equip = _plrInv.ItemEquiped.Item;
        _tileIsEmpty = _plrFac.TileFacingIsEmpty();
    }

    private void PlacingPreview()
    {
        if (_equip.Category != ItemCategory.Placeable)
        {
            _sprRen.sprite = null;
            return;
        }
        _sprRen.sprite = null;

        var itemSprite = _equip.Image;

        transform.position = _plrFac.transform.position;
        _sprRen.sprite = itemSprite;
        _sprRen.color = SetRightColorForPlacingPreview();
    }

    private Color32 SetRightColorForPlacingPreview()
    {
        if (_tileIsEmpty)
            return new Color32(0, 255, 0, 100);
        else
            return new Color32(255, 0, 0, 100);
    }


    private bool _justPlaced = false;
    private void PlaceItem()
    {
        _tileIsEmpty = _plrFac.TileFacingIsEmpty();

        if (_equip.Category != ItemCategory.Placeable || !_tileIsEmpty ||
             !_readyToPlace || !Input.GetKey(_actionMain))
            return;

        if (_justPlaced)
            return;

        var itemToPlace = _equip.VrsPickable;
        var item = Instantiate(itemToPlace);
        item.transform.position = _plrFac.transform.position;
        _plrInv.ItemEquiped.ItemQuantity--;
        _readyToPlace = false;
        _justPlaced = true;
        StartCoroutine(PlacingCoolDown());

    }

    private IEnumerator WaitToGetReadyForItemPlacing()
    {
        var facPos = _plrFac.transform.position;

        if (facPos != _plcPos)
        {
            _readyToPlace = false;
            yield return new WaitForSeconds(0.1f);
            _readyToPlace = true;
            _plcPos = facPos;
        }
    }

    private IEnumerator PlacingCoolDown()
    {
        yield return new WaitForSeconds(0.1f);
        _justPlaced = false;
    }
}