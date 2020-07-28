using UnityEngine;

public class PlayerAnimationEvents : MonoBehaviour
{
    /// <summary>
    /// SCRIPT RESPONSIBLE FOR VISUAL ITEMS PICKING AND DELETING ITEM FROM SPACE AFTER PICK
    /// HERE ARE METHODS THAT ARE PUT IN ANIMATION EVENTS FOR PICKING ITEMS CLIPS
    /// </summary>

    [SerializeField]
    private GameObject _itemP = null;

    private SpriteRenderer _itemPImg;
    private GameObject _itemPicked;

    public Animation _animsPick;

    private Animator _anim;
    private Movement _mov;
    private Sprite _itemImg;
    private Animator _animTool;

    private float _pickOffset = 0.7f;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();
        _mov = gameObject.GetComponent<Movement>();
        _itemPImg = _itemP.GetComponent<SpriteRenderer>();
        GetToolAnimatior();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void PickUp(GameObject ItemPicked)
    {
        _mov.TakingAction = true;

        _itemPicked = ItemPicked;
        var rotation = _mov.PlayerFacing;
        _itemImg = ItemPicked.GetComponent<SpriteRenderer>().sprite;

        if (_anim == null)
            return;

        if (rotation == Direction.West)
        {
            _anim.SetBool("Picking", true);
            _anim.SetInteger("Direction", 0);
        }
        else if (rotation == Direction.East)
        {
            _anim.SetBool("Picking", true);
            _anim.SetInteger("Direction", 1);
        }
        else if (rotation == Direction.North)
        {
            _anim.SetBool("Picking", true);
            _anim.SetInteger("Direction", 2);
        }
        else if (rotation == Direction.South)
        {
            _anim.SetBool("Picking", true);
            _anim.SetInteger("Direction", 3);
        }
    }

    public void ClearPick()
    {
        _itemPImg.sprite = null;
    }

    public void PickRight0()
    {
    }

    public void PickRight1()
    {
    }

    public void PickRight2()
    {
        PickItemFrame2(new Vector3(_pickOffset, 0, 0), true);
    }

    public void PickRight3()
    {
        PickItemFrame3(new Vector3(0, _pickOffset, 0));
    }

    public void PickRight4()
    {
        PickItemFrame4();
    }

    public void PickLeft0()
    {
    }

    public void PickLeft1()
    {
    }

    public void PickLeft2()
    {
        PickItemFrame2(new Vector3(-_pickOffset, 0, 0), true);
    }

    public void PickLeft3()
    {
        PickItemFrame3(new Vector3(0, _pickOffset, 0));
    }

    public void PickLeft4()
    {
        PickItemFrame4();
    }

    public void PickFront0()
    {
    }

    public void PickFront1()
    {
    }

    public void PickFront2()
    {
        PickItemFrame2(new Vector3(0, 0, 0), true);
    }

    public void PickFront3()
    {
        PickItemFrame3(new Vector3(0, _pickOffset, 0));
    }

    public void PickFront4()
    {
        PickItemFrame4();
    }

    public void PickBack0()
    {
    }

    public void PickBack1()
    {
    }

    public void PickBack2()
    {
        PickItemFrame2(new Vector3(0, 0, 0), false);
    }

    public void PickBack3()
    {
        PickItemFrame3(new Vector3(0, _pickOffset, 0));
    }

    public void PickBack4()
    {
        PickItemFrame4();
    }

    private void PickItemFrame2(Vector3 ItemLocalPosition,
        bool LayerItemInFrontOfCharacter)
    {
        var plrSortOrder = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<SpriteRenderer>().sortingOrder;
        _itemPImg.sprite = _itemImg;
        if (LayerItemInFrontOfCharacter)
            _itemPImg.sortingOrder = plrSortOrder + 1;
        else
            _itemPImg.sortingOrder = plrSortOrder - 1;
        _itemP.transform.localPosition = ItemLocalPosition;

        Destroy(_itemPicked);
    }

    private void PickItemFrame3(Vector3 ItemLocalPosition)
    {
        _itemP.transform.localPosition = ItemLocalPosition;
    }

    private void PickItemFrame4()
    {
        _mov.TakingAction = false;
        _anim.SetBool("Picking", false);
        ClearPick();
    }

    public void StartTakingAction()
    {
        _mov.TakingAction = true;
    }

    public void FinishTakingAction()
    {
        _mov.TakingAction = false;
        _anim.SetBool("UsingTool", false);
        _animTool.SetBool("UsingTool", false);
    }
    private void GetToolAnimatior()
    {
        var animators = gameObject.GetComponentsInChildren<Animator>();

        foreach (var animator in animators)
            if (animator.gameObject != gameObject)
                _animTool = animator;

    }

    public void StartFishingAfterThrowingBubble()
    {

    }
}