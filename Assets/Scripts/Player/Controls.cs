using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    private KeyCode _moveLeft = KeyCode.A;
    private KeyCode _moveRight = KeyCode.D;
    private KeyCode _moveUp = KeyCode.W;
    private KeyCode _moveDown = KeyCode.S;
    private KeyCode _invToggle = KeyCode.E;
    private KeyCode _actionMain = KeyCode.Mouse0;
    private KeyCode _actionAlt = KeyCode.Mouse1;


    private GameObject _plr;
    private GameObject _scrptUI;
    private GameObject _scrptInv;

    private Movement _plrMov;
    private InventoryToggler _invTog;
    private PlayerFacing _plrFac;
    private ToolUser _toolUser;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _scrptUI = GameObject.FindGameObjectWithTag("UIScripts");
        _scrptInv = GameObject.FindGameObjectWithTag("Inventory");

        _plrMov = _plr.GetComponent<Movement>();
        _invTog = _scrptUI.GetComponent<InventoryToggler>();
        _plrFac = _plr.GetComponentInChildren<PlayerFacing>();
        _toolUser = _plr.GetComponentInChildren<ToolUser>();
    }
    void Start()
    {
        
    }

    void Update()
    {
        Movement();
        ToggleInventory();
        TakeAction();
    }
    private void Movement()
    {
        if (!Input.GetKey(_moveLeft) &&
            !Input.GetKey(_moveRight))
            _plrMov.StopHorizontalMovement();
        else if (Input.GetKey(_moveLeft))
            _plrMov.MoveLeft();
        else if (Input.GetKey(_moveRight))
            _plrMov.MoveRight();

        if (!Input.GetKey(_moveUp) &&
            !Input.GetKey(_moveDown))
            _plrMov.StopVerticalMovement();
        else if (Input.GetKey(_moveUp))
            _plrMov.MoveUp();
        else if (Input.GetKey(_moveDown))
            _plrMov.MoveDown();
    }
    private void ToggleInventory()
    {
        if (Input.GetKeyDown(_invToggle))
            _invTog.ToggleInventoryUI();
    }
    private void TakeAction()
    {
        if (Input.GetKeyDown(_actionMain))
            _toolUser.UseItem();
        else if (Input.GetKeyDown(_actionAlt))
            _plrFac.TakeActionAlt();
    }

}
