using UnityEngine;

public class Collectable : ObjectInteractable
{
    public bool JustDroppped = false;

    public int ItemQuantity
    {
        get { return _itemQuantity; }
        set
        {
            if (value >= 0)
                _itemQuantity = value;
            else
                _itemQuantity = 0;
        }
    }

    private int _itemQuantity = 1;

    //ADJUST THIS FLOATS FOR DROPANIMATION
    private readonly float _droppedAnimationSpeed = 3f;

    //this 2 floats represent the difference in x and Y
    //at end postion from start positions
    private readonly float _droppedAnimationDistX = 0.8f;

    private readonly float _droppedAnimationDistY = 0f;

    //how far from player the item first shows at drop
    private readonly float _droppedAnimationDistFromPlayerAtStart = 1.2f;

    private readonly float _curveHeight = 1f;

    private float count = 0f;


    ///
    private GameObject _plr;

    private void Start()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        RecentlyDroppedAnimation();
    }


    private void RecentlyDroppedAnimation()
    {
        if (!JustDroppped)
            return;

        var point0 = GameObject.FindGameObjectWithTag("Player").transform.position
            + Vector3.right * _droppedAnimationDistFromPlayerAtStart;
        var point2 = point0 + Vector3.right * _droppedAnimationDistX;
        point2 += Vector3.up * _droppedAnimationDistY;
        var point1 = point0 + (point2 - point0) / 2 + Vector3.up * _curveHeight;

        if (count < 50.0f)
        {
            count += _droppedAnimationSpeed * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(point0, point1, count);
            Vector3 m2 = Vector3.Lerp(point1, point2, count);
            transform.position = Vector3.Lerp(m1, m2, count);

            if (transform.position == m2)
                JustDroppped = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _plr)
        {
            var comp = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsAddRemoveSearch>();
            var name = ReadGameObjectName();
            comp.ItemAdd(name, _itemQuantity);
            Destroy(gameObject);
        }
    }
}