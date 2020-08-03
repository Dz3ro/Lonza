using System.Collections;
using UnityEngine;

public class SpriteManagerAfterDeath : MonoBehaviour
{
    [SerializeField] private GameObject Detector;

    private string _gameObjectName;
    private GameObject _objAbove = null;
    private GameObject _objUnder = null;
    private GameObject _objLeft = null;
    private GameObject _objRight = null;

    private void Start()
    {
    }

    private void Update()
    {
    }

    public void ChangeSpritesAround(string gameObjectName, Vector3 position)
    {
        transform.position = position;
        _gameObjectName = gameObjectName;
        StartCoroutine(ChangeSpritesAround());
    }

    private IEnumerator ChangeSpritesAround()
    {
        yield return StartCoroutine(GetNeighboringObjects());
        yield return new WaitForSeconds(0.1f);
        SetSpritesAroundOnly();
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

        if (objectChecking.name != _gameObjectName)
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
    }

    private IEnumerator GetNeighboringObjects()
    {
        StartCoroutine(GetNeighboringObject(Direction.North));
        StartCoroutine(GetNeighboringObject(Direction.South));
        StartCoroutine(GetNeighboringObject(Direction.East));
        StartCoroutine(GetNeighboringObject(Direction.West));
        yield break;
    }

    private void SetSpritesAroundOnly()
    {
        if (_objAbove != null)
            _objAbove.GetComponent<SpriteManager>().SetSpriteManual(Direction.North);
        if (_objUnder != null)
            _objUnder.GetComponent<SpriteManager>().SetSpriteManual(Direction.South);
        if (_objLeft != null)
            _objLeft.GetComponent<SpriteManager>().SetSpriteManual(Direction.West);
        if (_objRight != null)
            _objRight.GetComponent<SpriteManager>().SetSpriteManual(Direction.East);
    }
}