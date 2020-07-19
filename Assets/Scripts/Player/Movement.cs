using UnityEngine;

public enum Direction { West, North, East, South };

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool TakingAction { get { return _takingAction; } set { _takingAction = value; } }
    private bool _takingAction = false;

    private Animator _anim;
    private Rigidbody2D _rigB;
    private GameFreezer _gFrz;

    public Direction PlayerFacing { get { return _playerFacing; } }

    private int _xMovement = 0;
    private int _yMovement = 0;
    private Direction _playerFacing = Direction.South;

    private SpriteRenderer _sprRen;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();
        _rigB = gameObject.GetComponent<Rigidbody2D>();
        _gFrz = GetComponentInChildren<GameFreezer>();
        _sprRen = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
        
    }

    private void FixedUpdate()
    {
        if (_takingAction || _gFrz.GameIsFreezed)
        {
            _xMovement = 0;
            _yMovement = 0;
            Animation();
            return;
        }
        SetupMovementAndDirection();
        MoveCharacter();
        Animation();
    }
    private void LateUpdate()
    {
        _sprRen.sortingOrder = (int)(100 - transform.position.y) * 2;
    }

    public void StopHorizontalMovement()
    {
        _xMovement = 0;
    }

    public void StopVerticalMovement()
    {
        _yMovement = 0;
    }

    public void MoveLeft()
    {
        _xMovement = -1;
    }

    public void MoveRight()
    {
        _xMovement = 1;
    }

    public void MoveUp()
    {
        _yMovement = 1;
    }

    public void MoveDown()
    {
        _yMovement = -1;
    }

    private void SetupMovementAndDirection()
    {
        //if (Input.GetAxis("Horizontal") == 0)
        //    _xMovement = 0;
        //else if (Input.GetAxis("Horizontal") > 0)
        //    _xMovement = 1;
        //else if (Input.GetAxis("Horizontal") < 0)
        //    _xMovement = -1;
        //if (Input.GetAxis("Vertical") == 0)
        //    _yMovement = 0;
        //else if (Input.GetAxis("Vertical") > 0)
        //    _yMovement = 1;
        //else if (Input.GetAxis("Vertical") < 0)
        //    _yMovement = -1;

        if (_xMovement == 0 && _yMovement == 0)
            return;

        if (_xMovement == 1 && _yMovement == 0)
            _playerFacing = Direction.East;
        else if (_xMovement == -1 && _yMovement == 0)
            _playerFacing = Direction.West;
        else if (_yMovement == 1 && _xMovement == 0)
            _playerFacing = Direction.North;
        else if (_yMovement == -1 && _xMovement == 0)
            _playerFacing = Direction.South;
    }

    private void MoveCharacter()
    {
        if (_xMovement == 0 && _yMovement == 0)
            return;
        var inputVector = new Vector3(_xMovement, _yMovement, 0);
        inputVector.Normalize();

        _rigB.transform.position = Vector3.MoveTowards(_rigB.transform.position, _rigB.transform.position + inputVector, Time.deltaTime * movementSpeed);
    }

    private void Animation()
    {
        if (_xMovement == 0 && _yMovement == 0)
        {
            _anim.SetBool("Moving", false);
            _anim.SetInteger("moveX", 0);
            _anim.SetInteger("moveY", 0);
        }
        ///
        if (_xMovement != 0 && _yMovement != 0)
        {
            _anim.SetBool("Moving", true);
            _anim.SetInteger("moveX", _xMovement);
            _anim.SetInteger("moveY", 0);
            return;
        }
        ///

        if (_yMovement != 0)
        {
            _anim.SetBool("Moving", true);
            _anim.SetInteger("moveY", _yMovement);
            _anim.SetInteger("moveX", 0);
        }
        else if (_xMovement != 0)
        {
            _anim.SetBool("Moving", true);
            _anim.SetInteger("moveX", _xMovement);
            _anim.SetInteger("moveY", 0);
        }
    }

    public int ShowDirectionAsIntForAnimator()
    {
        if (_playerFacing == Direction.West)
            return 0;
        if (_playerFacing == Direction.East)
            return 3;
        if (_playerFacing == Direction.North)
            return 2;
        return 1;
    }
}