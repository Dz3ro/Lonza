using UnityEngine;

public class FishingBubble : MonoBehaviour
{

    private PopupTexts _pTexts;

    private void Awake()
    {
        _pTexts = GameObject.FindGameObjectWithTag("Canvas")
            .GetComponent<PopupTexts>();
    }
    private void Start()
    {
    }

    private void Update()
    {
        FishOnHookEvents();
    }

    public void BubbleIsOnWater()
    {
    }

    private GameObject _touchingGameObj;

    private void OnTriggerStay2D(Collider2D collision)
    {
        _touchingGameObj = collision.gameObject;
    }

    public bool BubbleIsInWater()
    {
        if (_touchingGameObj == null)
            return false;
        return _touchingGameObj.GetComponent<Water>() != null;
    }

    public void CatchFish()
    {
        _touchingGameObj.GetComponent<Water>().GetFish();
    }

    private void FishingMiniGame()
    {
    }

    public void StopFishing()
    {
        _touchingGameObj = null;
    }

    private float _waitingStartTime;
    private float _waitingDurationBeforeFishShows;
    private float _durationOfFishBeingOnHook;

    private bool _fishOnHookEventHasStarted = false;

    public void WaitForFishToGetOnHook()
    {
    }

    private void GetFishOnHook()
    {
        _waitingStartTime = Time.time;
        _waitingDurationBeforeFishShows = Random.Range(2f, 10f);
        _durationOfFishBeingOnHook = Random.Range(2f, 5f);
        _fishOnHookEventHasStarted = true;
    }


    private bool FishIsOnHook()
    {
        if (!_fishOnHookEventHasStarted)
            return false;

        var fishTimeShow = _waitingStartTime + _waitingDurationBeforeFishShows;
        var fishTimeLeave = fishTimeShow + _durationOfFishBeingOnHook;

        if (Time.time >= fishTimeShow && Time.time <= fishTimeLeave)
            return true;

        return false;
    }

    private bool _turnedFishOnHookEventsOn = false;
    private bool _turnedFishOnHookEventsOff = false;


    private void FishOnHookEvents()
    {
        if ( !_turnedFishOnHookEventsOn && FishIsOnHook())
        {
            _pTexts.PopExclamationMark();
            _turnedFishOnHookEventsOn = true;
        }
        else if ( !_turnedFishOnHookEventsOff && _turnedFishOnHookEventsOn 
            && !FishIsOnHook())
        {
            _pTexts.RemoveExclamationMark();
            _turnedFishOnHookEventsOff = true;
        }
    }

    public void PullRodOut()
    {
        _pTexts.RemoveExclamationMark();

        if (FishIsOnHook())
        {
            _touchingGameObj.GetComponent<Water>().GetFish();
        }
    }

    public void PullRodIn()
    {
        GetFishOnHook();
    }
}