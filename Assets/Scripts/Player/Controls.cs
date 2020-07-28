using UnityEngine;

public class Controls : MonoBehaviour
{
    public readonly KeyCode MoveLeft = KeyCode.A;
    public readonly KeyCode MoveRight = KeyCode.D;
    public readonly KeyCode MoveUp = KeyCode.W;
    public readonly KeyCode MoveDown = KeyCode.S;
    public readonly KeyCode InvToggle = KeyCode.E;
    public readonly KeyCode ActionMain = KeyCode.Mouse0;
    public readonly KeyCode ActionAlt = KeyCode.Mouse1;
    public bool ScrollUp { get; private set; }
    public bool ScrollDown { get; private set; }



    private GameObject _plr;
    private GameObject _scrptUI;
    private GameObject _scrptInv;

    private Movement _plrMov;
    private InventoryToggler _invTog;
    private PlayerFacing _plrFac;
    private ToolAnimationEvents _toolUser;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _scrptUI = GameObject.FindGameObjectWithTag("UIScripts");
        _scrptInv = GameObject.FindGameObjectWithTag("Inventory");

        _plrMov = _plr.GetComponent<Movement>();
        _invTog = _scrptUI.GetComponent<InventoryToggler>();
        _plrFac = _plr.GetComponentInChildren<PlayerFacing>();
        _toolUser = _plr.GetComponentInChildren<ToolAnimationEvents>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        Movement();
        ToggleInventory();
        TakeAction();
        DetectScrolling();
    }

    private void DetectScrolling()
    {
        ScrollUp = Input.GetAxis("Mouse ScrollWheel") > 0;
        ScrollDown = Input.GetAxis("Mouse ScrollWheel") < 0;

    }

    private void Movement()
    {
        if (!Input.GetKey(MoveLeft) &&
            !Input.GetKey(MoveRight))
            _plrMov.StopHorizontalMovement();
        else if (Input.GetKey(MoveLeft))
            _plrMov.MoveLeft();
        else if (Input.GetKey(MoveRight))
            _plrMov.MoveRight();

        if (!Input.GetKey(MoveUp) &&
            !Input.GetKey(MoveDown))
            _plrMov.StopVerticalMovement();
        else if (Input.GetKey(MoveUp))
            _plrMov.MoveUp();
        else if (Input.GetKey(MoveDown))
            _plrMov.MoveDown();
    }

    private void ToggleInventory()
    {
        if (Input.GetKeyDown(InvToggle))
            _invTog.ToggleInventoryUI();
    }

    private void TakeAction()
    {
        if (Input.GetKeyDown(ActionMain))
            _toolUser.UseItem();
        else if (Input.GetKeyUp(ActionMain))
            _toolUser.UseItemExtras();
        else if (Input.GetKeyDown(ActionAlt))
            _plrFac.TakeActionAlt();
    }
}