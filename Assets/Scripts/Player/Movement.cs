using UnityEngine;

public enum Direction { West, North, East, South };

public class Movement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public bool TakingAction { get { return _takingAction; } set { _takingAction = value; } }
    private bool _takingAction = false;

    private Animator _anim;
    private Rigidbody2D _rigB;

    public Direction PlayerFacing { get { return _playerFacing; } }

    private int _xMovement = 0;
    private int _yMovement = 0;
    private Direction _playerFacing = Direction.South;

    private void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        _rigB = gameObject.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (_takingAction)
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

    private void SetupMovementAndDirection()
    {
        if (Input.GetAxis("Horizontal") == 0)
            _xMovement = 0;
        else if (Input.GetAxis("Horizontal") > 0)
            _xMovement = 1;
        else if (Input.GetAxis("Horizontal") < 0)
            _xMovement = -1;
        if (Input.GetAxis("Vertical") == 0)
            _yMovement = 0;
        else if (Input.GetAxis("Vertical") > 0)
            _yMovement = 1;
        else if (Input.GetAxis("Vertical") < 0)
            _yMovement = -1;

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

        if (_xMovement != 0 && _yMovement != 0)
            return;

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

    private void Testing()
    {
    }
}