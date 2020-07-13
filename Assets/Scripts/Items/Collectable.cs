using UnityEngine;

public class Collectable : ObjectInteractable
{
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

    private Vector3 _startPosition;
    private GameObject _plr;

    private float count = 0f;
    private readonly float _animationSpeed = 1.5f;
    private float _curveWidth = 1f;
    private float _curveHeight = 2f;
    private float _timeBeforeItemCanBeCollected = 0.5f;
    private float _spawnTimer;

    private void Start()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _startPosition = transform.position;
        _spawnTimer = Time.time;
        DropAnimationRandomizer();
        DropAnimation();
    }

    private void Update()
    {
        DropAnimation();
    }

    private void DropAnimation()
    {

        var point0 = _startPosition;
        var point2 = point0 + Vector3.right * _curveWidth;
        var point1 = point0 + (point2 - point0) / 2 + Vector3.up * _curveHeight;

        count += _animationSpeed * Time.deltaTime;

        Vector3 m1 = Vector3.Lerp(point0, point1, count);
        Vector3 m2 = Vector3.Lerp(point1, point2, count);
        transform.position = Vector3.Lerp(m1, m2, count);
    }

    private void DropAnimationRandomizer()
    {
        _curveHeight = Random.Range(1f, 2.5f);
        _curveWidth = Random.Range(-1f, 1f);
    }

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_spawnTimer + _timeBeforeItemCanBeCollected > Time.time)
            return;

        if (collision.gameObject == _plr)
        {
            var comp = GameObject.FindGameObjectWithTag("Inventory").GetComponent<ItemsAddRemoveSearch>();
            var name = ReadGameObjectName();
            comp.ItemAdd(name, _itemQuantity);
            Destroy(gameObject);
        }
    }
    public override void WhenPlayerInteracts()
    {
        throw new System.NotImplementedException();
    }
}