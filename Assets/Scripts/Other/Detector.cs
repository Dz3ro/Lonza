using UnityEngine;

public class Detector : MonoBehaviour
{
    private GameObject _plr;

    private Collider2D _currentCollision;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (ColidingWithPlayerOrHisExtras(collision) || ColidingWithOtherDetector(collision))
            return;
        _currentCollision = collision;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ColidingWithPlayerOrHisExtras(collision) || ColidingWithOtherDetector(collision))
            return;

        _currentCollision = collision;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (ColidingWithPlayerOrHisExtras(collision) || ColidingWithOtherDetector(collision))
            return;
        _currentCollision = null;
    }

    private bool ColidingWithOtherDetector(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.name == gameObject.name)
            return true;
        return false;
    }

    private bool ColidingWithPlayerOrHisExtras(Collider2D collision)
    {
        var gameObj = collision.gameObject;

        if (gameObj.name == _plr.name || (gameObj.transform.parent != null &&
             gameObj.transform.parent.name == _plr.name))
            return true;
        return false;
    }

    public bool ColidingWithSomething()
    {
        return _currentCollision != null;
    }

    public GameObject GetColidingGameObject()
    {
        return _currentCollision.gameObject;
    }
}