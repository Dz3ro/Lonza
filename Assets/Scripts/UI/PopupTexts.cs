using UnityEngine;

public class PopupTexts : MonoBehaviour
{
    [SerializeField] private GameObject ExclamationMark;

    private float _markPosYAbovePlr = 55f;
    private float _markShakeDistance = 10f;
    private float _movementSpeed = 60f;

    private Vector3 _posLower;
    private Vector3 _posUpper;
    private bool _timeToShake = false;
    private bool _moveMarkUp = true;

    private GameObject _plr;
    private GameObject _exclamationMark;
    private Camera _cam;

    private void Awake()
    {
        _plr = GameObject.FindGameObjectWithTag("Player");
        _cam = GameObject.FindGameObjectWithTag("MainCamera")
            .GetComponent<Camera>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (Input.GetKeyDown("t"))
            PopExclamationMarkLogic();

        if (Input.GetKeyDown("r"))
            RemoveExclamationMarkLogic();

        ShakeMark();
    }

    public void PopExclamationMark()
    {
        PopExclamationMarkLogic();
    }

    public void RemoveExclamationMark()
    {
        RemoveExclamationMarkLogic();
    }


    private void PopExclamationMarkLogic()
    {
        if (_exclamationMark != null)
            return;

        var playerPos = _plr.transform.position;
        var playerPosInScreen = _cam.WorldToScreenPoint(playerPos);
        var mark = Instantiate(ExclamationMark);

        var markPos = mark.GetComponent<RectTransform>();
        markPos.SetParent(gameObject.GetComponent<RectTransform>());
        markPos.localScale = new Vector3(1f, 1f, 1f);

        var finalPos = playerPosInScreen + Vector3.up * _markPosYAbovePlr;
        markPos.position = new Vector2(finalPos.x, finalPos.y);
        _exclamationMark = mark;
        StartShakeMark();
    }

    private void StartShakeMark()
    {
        _posLower = _exclamationMark.transform.position;
        _posUpper = _posLower + Vector3.up * _markShakeDistance;
        _timeToShake = true;
    }

    private void ShakeMark()
    {
        if (!_timeToShake || _exclamationMark == null)
            return;

        if (_moveMarkUp)
            _exclamationMark.transform.position +=
                Vector3.up * _movementSpeed * Time.deltaTime;
        else
            _exclamationMark.transform.position +=
                Vector3.down * _movementSpeed * Time.deltaTime;

        if (_exclamationMark.transform.position.y >= _posUpper.y)
            _moveMarkUp = false;
        else if (_exclamationMark.transform.position.y <= _posLower.y)
            _moveMarkUp = true;
    }

    private void RemoveExclamationMarkLogic()
    {
        if (_exclamationMark == null)
            return;
        Destroy(_exclamationMark);
    }
}