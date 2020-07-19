using System.Collections.Generic;
using UnityEngine;

public class ToolUser : MonoBehaviour
{
    private PlayerInventory _plrInv;
    private Item _equip;
    private Movement _plrMov;
    private ItemsCreator _allItems;
    private Animator _anim;
    private Animator _plrAnim;
    private PlayerFacing _plrFac;
    private ToolsLogic _toolLog;
    private GameFreezer _gFrz;

    private Direction _dir;
    private GameObject _plr;
    private bool _takingAction;

    private Dictionary<Item, int> _toolAnimationsId;

    private SpriteRenderer _sprRen;
    private SortingOrderManager _sprOrdMan;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _plrMov = _plr.GetComponent<Movement>();
        _plrInv = GameObject.FindGameObjectWithTag("Inventory")
           .GetComponent<PlayerInventory>();
        _allItems = GameObject.FindGameObjectWithTag("Inventory")
           .GetComponent<ItemsCreator>();
        _anim = GetComponent<Animator>();
        _plrAnim = _plr.GetComponent<Animator>();
        _plrFac = _plr.GetComponentInChildren<PlayerFacing>();
        _toolLog = GetComponent<ToolsLogic>();
        _gFrz = _plr.GetComponentInChildren<GameFreezer>();
        _sprRen = GetComponent<SpriteRenderer>();
        _sprOrdMan = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<SortingOrderManager>();
    }

    private void Start()
    {
        SetAnimationsId();
    }

    private void Update()
    {
        _dir = _plrMov.PlayerFacing;
        _takingAction = _plrMov.TakingAction;
        _equip = _plrInv.ItemEquiped.Item;
    }

    private void LateUpdate()
    {
        _sprOrdMan.SetSortingOrderForPlayerItem(_sprRen);
    }

    public void ItemUsageStart()
    {
        _plrMov.TakingAction = true;
    }

    public void ItemUsageEnd()
    {
        _anim.SetBool("UsingTool", false);
    }

    public void UseItem()
    {
        StartPicking();
    }

    private void StartPicking()
    {
        if (_gFrz.GameIsFreezed || _gFrz.PlayerActionIsFreezed)
            return;

        if (_takingAction != false || _equip.Category != ItemCategory.Tool)
            return;

        SetAnimation();
    }

    public void ToolFrameLeft0()
    {
        SetTransforms(gameObject, 0.16f, 0.93f);
    }

    public void ToolFrameLeft1()
    {
        SetTransforms(gameObject, -0.03f, 0.81f);
    }

    public void ToolFrameLeft2()
    {
        SetTransforms(gameObject, -0.53f, 0.51f, 40f);
    }

    public void ToolFrameLeft3()
    {
        SetTransforms(gameObject, -0.84f, -0.03f, 90f);
    }

    public void ToolFrameLeft4()
    {
        SetTransforms(gameObject, -0.78f, -0.18f, 90f);
        //_plrFac.UseTool(_equip);
        _toolLog.UseTool(_equip);
    }

    public void ToolFrameRight0()
    {
        SetTransforms(gameObject, -0.16f, 0.93f);
    }

    public void ToolFrameRight1()
    {
        SetTransforms(gameObject, 0.03f, 0.81f);
    }

    public void ToolFrameRIght2()
    {
        SetTransforms(gameObject, 0.53f, 0.51f, -40f);
    }

    public void ToolFrameRight3()
    {
        SetTransforms(gameObject, 0.84f, -0.03f, -90f);
    }

    public void ToolFrameRight4()
    {
        SetTransforms(gameObject, 0.78f, -0.18f, -90f);
        //_plrFac.UseTool(_equip);
        _toolLog.UseTool(_equip);
    }

    public void ToolFrameFront0()
    {
        SetTransforms(gameObject, 0f, 0.94f);
    }

    public void ToolFrameFront1()
    {
        SetTransforms(gameObject, 0f, 0.87f);
    }

    public void ToolFrameFront2()
    {
        SetTransforms(gameObject, 0f, 0.83f);
    }

    public void ToolFrameFront3()
    {
        SetTransforms(gameObject, 0f, 0f);
    }

    public void ToolFrameFront4()
    {
        SetTransforms(gameObject, 0f, -0.29f);
        //_plrFac.UseTool(_equip);
        _toolLog.UseTool(_equip);
    }

    public void ToolFrameBack0()
    {
        var toomImg = gameObject.GetComponent<SpriteRenderer>();
        var plrImg = _plr.GetComponent<SpriteRenderer>();
        toomImg.sortingOrder = plrImg.sortingOrder - 1;
        SetTransforms(gameObject, 0f, 0.94f);
    }

    public void ToolFrameBack1()
    {
        SetTransforms(gameObject, 0f, 0.87f);
    }

    public void ToolFrameBack2()
    {
        SetTransforms(gameObject, 0f, 0.81f);
    }

    public void ToolFrameBack3()
    {
        SetTransforms(gameObject, 0f, 0.3f);
    }

    public void ToolFrameBack4()
    {
        SetTransforms(gameObject, 0f, 0);
        //_plrFac.UseTool(_equip);
        _toolLog.UseTool(_equip);
    }

    public void FinishUsingTool()
    {
        _anim.SetBool("UsingTool", false);
    }

    private void SetTransforms(GameObject gObj, float posX, float posY,
        float rotation = 0)
    {
        gObj.transform.localPosition = new Vector3(posX, posY, 0);
        gObj.transform.localEulerAngles = new Vector3(0, 0, rotation);
    }

    private void SetAnimation()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = _plr.GetComponent<SpriteRenderer>().sortingOrder + 1;
        var dir = _plrMov.ShowDirectionAsIntForAnimator();

        _plrAnim.SetBool("UsingTool", true);
        _anim.SetBool("UsingTool", true);
        _plrAnim.SetInteger("Direction", dir);
        _anim.SetInteger("Direction", dir);

        var ItemId = _toolAnimationsId[_equip];
        _anim.SetInteger("ToolId", ItemId);
    }

    private void SetAnimationsId()
    {
        _toolAnimationsId = new Dictionary<Item, int>();

        _toolAnimationsId.Add(_allItems.Pickaxe, 0);
        _toolAnimationsId.Add(_allItems.Axe, 1);
        _toolAnimationsId.Add(_allItems.Hoe, 2);
    }
}