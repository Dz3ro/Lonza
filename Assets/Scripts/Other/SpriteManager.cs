using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject Detector;
    [SerializeField] private GameObject DetectorAfterDeath;
    [SerializeField] private List<Sprite> SpriteList;

    private bool _sameObjAbove = false;
    private bool _sameObjUnder = false;
    private bool _sameObjLeft = false;
    private bool _sameObjRight = false;
    private GameObject _objAbove = null;
    private GameObject _objUnder = null;
    private GameObject _objLeft = null;
    private GameObject _objRight = null;
    private SpriteRenderer _sprRen;

    private void Awake()
    {
        _sprRen = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(SetGraphicsAllAround());
    }

    private IEnumerator GetNeighboringObject(Direction directionToCheck)
    {
        var center = transform.position;

        var detector = Instantiate(Detector);
        detector.transform.position = center;

        if (directionToCheck == Direction.North)
            detector.transform.position += Vector3.up;
        else if (directionToCheck == Direction.South)
            detector.transform.position += Vector3.down;
        else if (directionToCheck == Direction.West)
            detector.transform.position += Vector3.left;
        else if (directionToCheck == Direction.East)
            detector.transform.position += Vector3.right;

        var detComp = detector.GetComponent<Detector>();
        yield return new WaitForSeconds(0.02f);

        if (!detComp.ColidingWithSomething())
        {
            Destroy(detector);

            if (directionToCheck == Direction.North)
                _objAbove = null;
            else if (directionToCheck == Direction.South)
                _objUnder = null;
            else if (directionToCheck == Direction.West)
                _objLeft = null;
            else if (directionToCheck == Direction.East)
                _objRight = null;

            yield break;
        }
        var objectChecking = detComp.GetColidingGameObject();

        if (objectChecking.name != gameObject.name)
            objectChecking = null;

        if (directionToCheck == Direction.North)
            _objAbove = objectChecking;
        else if (directionToCheck == Direction.South)
            _objUnder = objectChecking;
        else if (directionToCheck == Direction.West)
            _objLeft = objectChecking;
        else if (directionToCheck == Direction.East)
            _objRight = objectChecking;
        Destroy(detector);
        SetBoolsForNeighboringObjects();
    }

    private IEnumerator GetNeighboringObjects()
    {
        StartCoroutine(GetNeighboringObject(Direction.North));
        StartCoroutine(GetNeighboringObject(Direction.South));
        StartCoroutine(GetNeighboringObject(Direction.East));
        StartCoroutine(GetNeighboringObject(Direction.West));
        yield return new WaitForSeconds(0.05f);
    }

    private IEnumerator SetGraphicsSingle()
    {
        yield return StartCoroutine(GetNeighboringObjects());
        SetSpriteSingle();
    }

    private IEnumerator SetGraphicsAllAround()
    {
        yield return StartCoroutine(GetNeighboringObjects());
        StartCoroutine(SetSpritesAll());
    }

    private void SetBoolsForNeighboringObjects()
    {
        _sameObjAbove = _objAbove != null;
        _sameObjUnder = _objUnder != null;
        _sameObjLeft = _objLeft != null;
        _sameObjRight = _objRight != null;
    }

    private void SetSpriteSingle()
    {
        if (_sameObjAbove == false && _sameObjUnder == true &&
            _sameObjLeft == false && _sameObjRight == true)
            SetSprite(SpriteList[0]);
        else if (_sameObjAbove == false && _sameObjUnder == true &&
            _sameObjLeft == true && _sameObjRight == true)
            SetSprite(SpriteList[1]);
        else if (_sameObjAbove == false && _sameObjUnder == true &&
            _sameObjLeft == true && _sameObjRight == false)
            SetSprite(SpriteList[2]);
        else if (_sameObjAbove == true && _sameObjUnder == true &&
            _sameObjLeft == false && _sameObjRight == true)
            SetSprite(SpriteList[3]);
        else if (_sameObjAbove == true && _sameObjUnder == true &&
            _sameObjLeft == true && _sameObjRight == true)
            SetSprite(SpriteList[4]);
        else if (_sameObjAbove == true && _sameObjUnder == true &&
            _sameObjLeft == true && _sameObjRight == false)
            SetSprite(SpriteList[5]);
        else if (_sameObjAbove == true && _sameObjUnder == false &&
            _sameObjLeft == false && _sameObjRight == true)
            SetSprite(SpriteList[6]);
        else if (_sameObjAbove == true && _sameObjUnder == false &&
           _sameObjLeft == true && _sameObjRight == true)
            SetSprite(SpriteList[7]);
        else if (_sameObjAbove == true && _sameObjUnder == false &&
           _sameObjLeft == true && _sameObjRight == false)
            SetSprite(SpriteList[8]);
        else if (_sameObjAbove == false && _sameObjUnder == false &&
           _sameObjLeft == false && _sameObjRight == false)
            SetSprite(SpriteList[9]);
        else if (_sameObjAbove == false && _sameObjUnder == false &&
           _sameObjLeft == false && _sameObjRight == true)
            SetSprite(SpriteList[10]);
        else if (_sameObjAbove == false && _sameObjUnder == false &&
           _sameObjLeft == true && _sameObjRight == false)
            SetSprite(SpriteList[11]);
        else if (_sameObjAbove == false && _sameObjUnder == true &&
           _sameObjLeft == false && _sameObjRight == false)
            SetSprite(SpriteList[12]);
        else if (_sameObjAbove == true && _sameObjUnder == false &&
           _sameObjLeft == false && _sameObjRight == false)
            SetSprite(SpriteList[13]);
        else if (_sameObjAbove == false && _sameObjUnder == false &&
           _sameObjLeft == true && _sameObjRight == true)
            SetSprite(SpriteList[14]);
        else if (_sameObjAbove == true && _sameObjUnder == true &&
           _sameObjLeft == false && _sameObjRight == false)
            SetSprite(SpriteList[15]);
        else
            throw new Exception("somethign went wrong with setting sprite");
    }

    public void SetSprite(Sprite sprite)
    {
        _sprRen.sprite = sprite;
    }

    public IEnumerator SetSprite()
    {
        yield return StartCoroutine(SetGraphicsSingle());
    }

    private IEnumerator SetSpritesAll()
    {
        SetSpriteSingle();
        if (_objAbove != null)
            yield return _objAbove.GetComponent<SpriteManager>().SetSprite();
        if (_objUnder != null)
            yield return _objUnder.GetComponent<SpriteManager>().SetSprite();
        if (_objLeft != null)
            yield return _objLeft.GetComponent<SpriteManager>().SetSprite();
        if (_objRight != null)
            yield return _objRight.GetComponent<SpriteManager>().SetSprite();
    }

    public void SetSpriteManual(Direction direction)
    {
        if (direction == Direction.North)
        {
            _sameObjUnder = false;
            _objUnder = null;
        }
        else if (direction == Direction.South)
        {
            _sameObjAbove = false;
            _objAbove = null;
        }
        else if (direction == Direction.West)
        {
            _sameObjRight = false;
            _objRight = null;
        }
        else if (direction == Direction.East)
        {
            _sameObjLeft = false;
            _objLeft = null;
        }
        SetSpriteSingle();
    }
}