using System.Collections;
using UnityEngine;

public class Tree : ObjectInteractiveWithTool
{
    [SerializeField] private GameObject ItemToDrop;
    [SerializeField] private GameObject Particle0;
    [SerializeField] private GameObject Particle1;
    [SerializeField] private GameObject Particle2;
    [SerializeField] private GameObject Particle3;

    [SerializeField] private GameObject Leaf0;
    [SerializeField] private GameObject Leaf1;
    [SerializeField] private GameObject Leaf2;
    [SerializeField] private GameObject Leaf3;

    [SerializeField] private GameObject _treeUpperPart;

    private bool _timeForTreeToFall = false;
    private float _startOFTreeFalling;
    private bool _timeToStartDropsFromFallenTree = false;
    private bool _fallTreeRightSide;

    private void Awake()
    {
    }

    private new void Start()
    {
        base.Start();
        SetupProperties(200, 0, ItemType.Axe);
    }

    private new void Update()
    {
        base.Update();
        FallTree();
    }


    public override void OnToolUse(Item item)
    {
        ItemHit(item);
    }

    private void ItemHit(Item item)
    {
        if (NotUsingViableTool(item))
            return;

        DealDamage(item);
        DropWoodenParticles();
        ShakeTree();

        if (TreeHasNotFallenOrBeenCut())
            DropLeafes();

        if (CanPerformOneTimeActionBelow0())
            PerformOneTimeActionOnDeath();
        if (CanPerformOneTimeActionBelow40())
            PerformOneTimeActionBelow40();
    }

    private void ShakeTree()
    {
        var plrDir = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Movement>().PlayerFacing;

        var rnd = Random.Range(0, 10);

        if (plrDir == Direction.East)
            ShakeTreeDirection(true);
        else if (plrDir == Direction.East)
            ShakeTreeDirection(false);
        else
            ShakeTreeDirection(rnd > 10);
    }

    private void ShakeTreeDirection(bool shakeRight, float shakePower = 0.5f)
    {
        var direction = shakeRight ? 1 : -1;
        var rotation = direction * shakePower;

        if (_treeUpperPart == null)
            return;
        IEnumerator Rotation()
        {
            for (int i = 0; i < 3; i++)
            {
                if (_treeUpperPart == null)
                    continue;

                _treeUpperPart.transform.Rotate(0, 0, -rotation);
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < 3; i++)
            {
                if (_treeUpperPart == null)
                    continue;

                _treeUpperPart.transform.Rotate(0, 0, rotation);
                yield return new WaitForSeconds(0.01f);
            }
        }
        StartCoroutine(Rotation());
        if (_treeUpperPart == null)
            StopCoroutine(Rotation());
    }

    private void FallTree()
    {
        if (_treeUpperPart == null)
            return;
        if (!_timeForTreeToFall)
            return;

        float rotation;
        if (_fallTreeRightSide)
            rotation = -35f;
        else
            rotation = 35f;

        _treeUpperPart.transform.eulerAngles += new Vector3(0, 0,
            rotation) * Time.deltaTime;
        if (_startOFTreeFalling == 0)
            _startOFTreeFalling = Time.time;

        if (_startOFTreeFalling + 2.5f < Time.time)
        {
            Destroy(_treeUpperPart);
            KeepDroppingWoodWithPArticles();

            _timeForTreeToFall = false;
            _timeToStartDropsFromFallenTree = true;
        }
        IEnumerator KeepDroppingWoodWithPArticles()
        {
            for (int i = 0; i < 4; i++)
            {
                var placeX = transform.position.x;
                if (_fallTreeRightSide)
                    placeX += i + 2f;
                else
                    placeX -= i + 2f;

                var placeY = Random.Range(-1f, 1f);
                var position = new Vector3(placeX, transform.position.y + placeY, 0);

                DropItemOnPosition(ItemToDrop, position, 4);
                CreateParticleAtPosition(Particle0, Particle1,
                    Particle2, Particle3, 10, position);
                yield return new WaitForSeconds(0.2f);
            }
        }

        if (_timeToStartDropsFromFallenTree)
            StartCoroutine(KeepDroppingWoodWithPArticles());
    }

    private void SetupTreeFalling()
    {
        var plrDir = GameObject.FindGameObjectWithTag("Player")
            .GetComponent<Movement>().PlayerFacing;

        if (plrDir == Direction.East)
        {
            _fallTreeRightSide = true;
            return;
        }
        else if (plrDir == Direction.West)
        {
            _fallTreeRightSide = false;
            return;
        }

        var rnd = Random.Range(0, 10);

        _fallTreeRightSide = rnd > 5;
        //_timeForTreeToFall = true;
    }

    private void DropLeafes()
    {
        var rnd = Random.Range(1, 3);

        for (int i = 0; i < rnd; i++)
            DropLeaf();
    }

    private void DropLeaf()
    {
        var rnd = Random.Range(0, 3);
        var leaf = rnd == 0 ? Leaf0 : rnd == 1 ? Leaf1 : rnd == 2 ?
            Leaf2 : Leaf3;

        var posY = Random.Range(2f, 2.8f);
        var posX = Random.Range(-1f, 1f);

        var startPos = new Vector3(posX, posY, 0);
        var leafObj = Instantiate(leaf);
        leafObj.transform.SetParent(gameObject.transform);
        leafObj.transform.localPosition = startPos;
    }

    private void DropWoodenParticles()
    {
        base.CreateParticle(Particle0, Particle1,
            Particle2, Particle3, 8);
    }

    private bool TreeHasNotFallenOrBeenCut()
    {
        return _timeForTreeToFall == false && _treeUpperPart != null;
    }

    private new void PerformOneTimeActionBelow40()
    {
        base.PerformOneTimeActionBelow40();
        SetupTreeFalling();
        _timeForTreeToFall = true;
    }

    private new void PerformOneTimeActionOnDeath()
    {
        base.PerformOneTimeActionOnDeath();
        DropItemExactlyOnGameObj(ItemToDrop, 6);
        StartDeathTimer();
    }
}