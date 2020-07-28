using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private GameObject FishingBubble;
    private GameObject _fishingBubble;
    private Vector3 _bubblePosStart;
    private Vector3 _bubblePosEnd;

    private bool _bubbleThrowAxisIsHorizontal;
    private bool _waitingForFish = false;

    private float _fishingTimeStart;
    private float _throwBubbleDistance;
    private float _bubblePosCounter;
    private float _bubbleMoveSpeed = 1.5f;
    private Direction _dir;
    private bool _takingAction;
    private KeyCode _actionMain;
    private Item _equip;
    private FishingBubble _fishingBubbleComp;

    private PlayerInventory _plrInv;
    private Animator _animPlr;
    private Animator _animTool;
    private Movement _plrMov;
    private GameFreezer _gFrz;

    private void Awake()
    {
        var plr = GameObject.FindGameObjectWithTag("Player");
        var itemUser = GameObject.FindGameObjectWithTag("ItemUser");
        var invMng = GameObject.FindGameObjectWithTag("Inventory");

        _animPlr = plr.GetComponent<Animator>();
        _animTool = itemUser.GetComponent<Animator>();
        _plrMov = plr.GetComponent<Movement>();
        _gFrz = plr.GetComponentInChildren<GameFreezer>();
        _plrInv = invMng.GetComponent<PlayerInventory>();
        _actionMain = plr.GetComponent<Controls>().ActionMain;
    }

    private void Start()
    {
    }

    private void Update()
    {
        _dir = _plrMov.PlayerFacing;
        _takingAction = _plrMov.TakingAction;
        _equip = _plrInv.ItemEquiped.Item;
        MoveBubble();
        StartFishingAFterBubbleThrow();
        GetBackToIdleStateIFBubbleMissesWater();
        //WaitForFish();

        FinalFishingPart();
    }

    public void StartFishing()
    {
        PullRod();
    }

    public void CastFishingRod()
    {
        CastRod();
    }

    private void PullRod()
    {
        if (_waitingForFish)
            return;

        var dir = _plrMov.ShowDirectionAsIntForAnimator();
        _animPlr.SetInteger("Direction", dir);
        _animTool.SetInteger("Direction", dir);
        _animPlr.SetTrigger("FishingPrepareToThrow");
        _animTool.SetTrigger("FishingPrepareToThrow");
        _animTool.SetBool("UsingTool", true);
        _fishingTimeStart = Time.time;
    }

    private void CastRod()
    {
        if (_waitingForFish || _gFrz.GameIsFreezed || _gFrz.PlayerActionIsFreezed
            || _takingAction == false || _equip.Category != ItemCategory.Tool ||
            _fishingBubble != null || !Input.GetKeyUp(_actionMain))
            return;

        _animPlr.SetTrigger("FishingThrow");
        _animTool.SetTrigger("FishingThrow");

        var throwingPower = Time.time - _fishingTimeStart;
        throwingPower *= 3f;
        ThrowFishingBubble(throwingPower);
    }

    private void ThrowFishingBubble(float throwingPower)
    {
        var bubbleDistance = 3f + throwingPower;
        var maxPower = 5f;
        var minPower = 3f;

        if (bubbleDistance > maxPower)
            bubbleDistance = maxPower;
        else if (bubbleDistance < minPower)
            bubbleDistance = minPower;

        _throwBubbleDistance = bubbleDistance;
        _fishingBubble = Instantiate(FishingBubble);
        _fishingBubble.transform.SetParent(gameObject.transform);
        _fishingBubble.transform.position = gameObject.transform.position;
        _fishingBubbleComp = _fishingBubble.GetComponent<FishingBubble>();

        var dir = _plrMov.ShowDirectionAsIntForAnimator();
        if (dir == 0 || dir == 1)
            _throwBubbleDistance = -_throwBubbleDistance;

        if (dir == 0 || dir == 3)
        {
            _bubbleThrowAxisIsHorizontal = true;
            _fishingBubble.transform.position += Vector3.up * 0.8f;
        }
        else
            _bubbleThrowAxisIsHorizontal = false;

        _bubblePosStart = _fishingBubble.transform.position;
    }

    private void MoveBubble()
    {
        if (_waitingForFish || _fishingBubble == null)
            return;

        if (_bubbleThrowAxisIsHorizontal)
            MoveBubbleHorizontal();
        else
            MoveBubbleVertical();
    }

    private void MoveBubbleHorizontal()
    {
        var highestPoint = Mathf.Abs(_throwBubbleDistance) / 3;
        var posStart = _bubblePosStart;
        var posEnd = posStart + (Vector3.right * _throwBubbleDistance) +
            Vector3.down * 2f;
        posEnd += Vector3.left * 0.4f;

        _bubblePosEnd = posEnd;
        var posMidHighest = posStart + (posEnd - posStart) / 2 + Vector3.up *
            highestPoint;

        _bubblePosCounter += _bubbleMoveSpeed * Time.deltaTime;

        Vector3 m1 = Vector3.Lerp(posStart, posMidHighest, _bubblePosCounter);
        Vector3 m2 = Vector3.Lerp(posMidHighest, posEnd, _bubblePosCounter);
        _fishingBubble.transform.position = Vector3.Lerp(m1, m2, _bubblePosCounter);
    }

    private void MoveBubbleVertical()
    {
        var posStart = _bubblePosStart;
        var posEnd = posStart + Vector3.up * _throwBubbleDistance;
        posEnd += Vector3.down * 1.2f;
        _bubblePosEnd = posEnd;
        var posMid = posStart + (posEnd - posStart);
        _bubblePosCounter += _bubbleMoveSpeed * Time.deltaTime;

        Vector3 m1 = Vector3.Lerp(posStart, posMid, _bubblePosCounter);
        Vector3 m2 = Vector3.Lerp(posMid, posEnd, _bubblePosCounter);
        _fishingBubble.transform.position = Vector3.Lerp(m1, m2, _bubblePosCounter);
    }

    private void StartFishingAFterBubbleThrow()
    {
        if (_waitingForFish || _fishingBubble == null)
            return;

        if (_fishingBubble.transform.position == _bubblePosEnd)
            BeginWaitingGame();
    }

    private void BeginWaitingGame()
    {
        if (_waitingForFish || _fishingBubble == null)
            return;

        _animPlr.SetTrigger("FishAfterThrow");
        _animTool.SetTrigger("FishAfterThrow");
    }

    private void GetBackToIdleStateIFBubbleMissesWater()
    {
        if (_fishingBubbleComp == null)
            return;

        var inWater = _fishingBubbleComp.BubbleIsInWater();
        if (inWater && _fishingBubble.transform.position == _bubblePosEnd)
        {
            WaitForFish();
            return;
        }
        else if (_fishingBubble.transform.position == _bubblePosEnd)
            StopFishing();
    }

    private void StopFishing()
    {
        _animPlr.SetBool("UsingTool", false);
        _animTool.SetBool("UsingTool", false);
        _animPlr.SetTrigger("FishingFinish");
        _plrMov.TakingAction = false;
        _bubblePosCounter = 0f;
        Destroy(_fishingBubble);
    }


    private void WaitForFish()
    {
        if (_waitingForFish)
            return;
        _fishingBubbleComp.PullRodIn();
        _finalFishingPartHasStarted = true;
        _waitingForFish = true;



    }

    private bool _finalFishingPartHasStarted = false;

    private void FinalFishingPart()
    {
        if (!_finalFishingPartHasStarted || !Input.GetKey(_actionMain))
            return;
        _fishingBubbleComp.PullRodOut();
        StopFishing();
        _waitingForFish = false;
        _finalFishingPartHasStarted = false;
    }
}