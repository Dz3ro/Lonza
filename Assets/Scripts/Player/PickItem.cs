using UnityEngine;

public class PickItem : MonoBehaviour
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

    private int _plrSortOrder;
    private float _pickOffset = 0.7f;

    private void Awake()
    {
        _anim = gameObject.GetComponent<Animator>();
        _mov = gameObject.GetComponent<Movement>();
        _itemPImg = _itemP.GetComponent<SpriteRenderer>();
        _plrSortOrder = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().sortingOrder;
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void PickUp(GameObject ItemPicked)
    {
        _itemPicked = ItemPicked;
        var rotation = _mov.PlayerFacing;
        _itemImg = ItemPicked.GetComponent<SpriteRenderer>().sprite;

        if (_anim == null)
            return;

        if (rotation == Direction.West)
        {
            _anim.Play("PickItem_Left");
        }
        else if (rotation == Direction.East)
        {
            _anim.Play("PickItem_Right");
        }
        else if (rotation == Direction.North)
        {
            _anim.Play("PickItem_Back");
        }
        else if (rotation == Direction.South)
        {
            _anim.Play("PickItem_Front");
        }
        else
            print("this should not log");
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
        _itemPImg.sprite = _itemImg;
        _itemPImg.sortingOrder = _plrSortOrder + 1;
        _itemP.transform.localPosition = new Vector3(_pickOffset, 0, 0);

        Destroy(_itemPicked);
    }

    public void PickRight3()
    {
    }

    public void PickRight4()
    {
        _itemP.transform.localPosition = new Vector3(0, _pickOffset, 0);
    }

    public void PickLeft0()
    {
    }

    public void PickLeft1()
    {
    }

    public void PickLeft2()
    {
        _itemPImg.sprite = _itemImg;
        _itemPImg.sortingOrder = _plrSortOrder + 1;
        _itemP.transform.localPosition = new Vector3(-_pickOffset, 0, 0);

        Destroy(_itemPicked);
    }

    public void PickLeft3()
    {
    }

    public void PickLeft4()
    {
        _itemP.transform.localPosition = new Vector3(0, _pickOffset, 0);
    }

    public void PickFront0()
    {
    }

    public void PickFront1()
    {
    }

    public void PickFront2()
    {
        _itemPImg.sprite = _itemImg;
        _itemPImg.sortingOrder = _plrSortOrder + 1;
        _itemP.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(_itemPicked);
    }

    public void PickFront3()
    {
    }

    public void PickFront4()
    {
        _itemP.transform.localPosition = new Vector3(0, _pickOffset, 0);
    }

    public void PickBack0()
    {
    }

    public void PickBack1()
    {
    }

    public void PickBack2()
    {
        _itemPImg.sprite = _itemImg;
        _itemPImg.sortingOrder = _plrSortOrder - 1;
        _itemP.transform.localPosition = new Vector3(0, 0, 0);

        Destroy(_itemPicked);
    }

    public void PickBack3()
    {
    }

    public void PickBack4()
    {
        _itemP.transform.localPosition = new Vector3(0, _pickOffset, 0);
    }
}