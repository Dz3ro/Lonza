using System;
using UnityEngine;

public class PlayerFacing : MonoBehaviour
{
    private GameObject _player;
    private Movement _mov;
    private ObjectInteractable _interaction;
    private ObjectInteractiveWithTool _toolUse;
    private Collider2D _currentCollision;



    public float YOnMovementHorizontal = -0.5f;
    public float XOnMovementHorizontal = 0.7f;
    public float YOnMovementUp = 0f;

    public float YOnMovementDown = -1.1f;

    private PlayerInventory _plrInv;
    private GameFreezer _gFrz;

    private string _tillableTilemapName = "DirtToTill";


    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _mov = _player.GetComponent<Movement>();
        _plrInv = _player.GetComponent<PlayerInventory>();
        _gFrz = _player.GetComponentInChildren<GameFreezer>();
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        MoveFacingBlock();
        //Interact();
    }

    public void TakeActionAlt()
    {
        Interact();
    }

    private void MoveFacingBlock()
    {
        var dir = _mov.PlayerFacing;

        if (dir == Direction.South)
            transform.position = _player.transform.position + new Vector3(0, YOnMovementDown, 0);
        else if (dir == Direction.North)
            transform.position = _player.transform.position + new Vector3(0, YOnMovementUp, 0);
        else if (dir == Direction.West)
            transform.position = _player.transform.position + new Vector3(-XOnMovementHorizontal, YOnMovementHorizontal, 0);
        else if (dir == Direction.East)
            transform.position = _player.transform.position + new Vector3(XOnMovementHorizontal, YOnMovementHorizontal, 0);

        CenterPostionToTileCenter();
    }

    private void Interact()
    {
        if ( _gFrz.PlayerActionIsFreezed || _gFrz.GameIsFreezed ||
            _interaction == null)
            
            return;

        _interaction.WhenPlayerInteracts();
        _interaction = null;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        _toolUse = collision.gameObject.GetComponent<ObjectInteractiveWithTool>();
        _interaction = collision.gameObject.GetComponent<ObjectInteractable>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        _currentCollision = collision;


        if (_interaction != null || _toolUse != null)
            return;

        _interaction = collision.gameObject.GetComponent<ObjectInteractable>();
        _toolUse = collision.gameObject.GetComponent<ObjectInteractiveWithTool>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _currentCollision = null;

        _interaction = null;
        _toolUse = null;
    }
    public void UseTool(Item tool)
    {
        if (_toolUse == null)
            return;
        _toolUse.OnToolUse(tool);
    }

    public bool FacingEmptyTileAbleToTill()
    {
        if (_currentCollision == null || 
            _currentCollision.gameObject.name != _tillableTilemapName)
            return false;
        return true;
    }

    public bool TileFacingIsEmpty()
    {
        return _currentCollision == null;
    }

    public Vector3 GetPlayerFacingPosition()
    {
        return gameObject.transform.position;
    }

    private void CenterPostionToTileCenter()
    {
        var posX = transform.position.x;
        var posY = transform.position.y;
        var finalX = 0;
        var finalY = 0;

        var dir = _mov.PlayerFacing;

        if (dir == Direction.South)
        {
            finalX = Mathf.RoundToInt(posX);
            finalY = Mathf.FloorToInt(posY);
        }
        else if (dir == Direction.North)
        {
            finalX = Mathf.RoundToInt(posX);
            finalY = Mathf.CeilToInt(posY);
        }
        else if (dir == Direction.West)
        {
            posY -= 0.3f;

            posX += 0.2f;

            finalX = Mathf.FloorToInt(posX);
            finalY = Mathf.RoundToInt(posY);
        }
        else if (dir == Direction.East)
        {
            posY -= 0.3f;

            posX -= 0.2f;


            finalX = Mathf.CeilToInt(posX);
            finalY = Mathf.RoundToInt(posY);
        }
        transform.position = new Vector3(finalX, finalY, 0);
    }


}