using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractiveWithTool : MonoBehaviour
{
    protected int _maxHp = 10;
    protected int _currentHp = 10;
    protected int _requiedToolLevel = 0;
    private float _rangeOfDropsAroundGameObj;

    protected ItemType _requiedToolType;
    protected float _deathCountStart;
    protected bool _startDeathTimer = false;
    protected bool _itemsHaveDropped = false;

    protected bool _dealtActionOnBelow80 = false;
    protected bool _dealtActionOnBelow60 = false;
    protected bool _dealtActionOnBelow40 = false;
    protected bool _dealtActionOnBelow20 = false;
    protected bool _dealtActionOnNoHp = false;

    protected List<Particle> _particles;
    protected SpriteRenderer _sprRenParent;
    protected SpriteRenderer _sprRenChild;

    protected void Start()
    {
        _particles = new List<Particle>();
        SetSortingOrder();
    }

    protected void Update()
    {
        DeathTimer();
        MoveParticles();
    }

    public virtual void OnToolUse(Item item)
    {

    }


    protected void DropItemAroundGameObj(GameObject itemToDrop, int itemsAmount = 1)
    {
        void DropIt(Vector3 direction)
        {
            _rangeOfDropsAroundGameObj = Random.Range(1.5f, 2f);

            Instantiate(itemToDrop,
                    transform.position + direction * _rangeOfDropsAroundGameObj,
              Quaternion.identity);
        }

        for (int i = 0; i < itemsAmount; i++)
            if (i % 4 == 0)
                DropIt(Vector3.right);
            else if (i % 3 == 0)
                DropIt(Vector3.left);
            else if (i % 2 == 0)
                DropIt(Vector3.up);
            else
                DropIt(Vector3.down);
    }
    protected void DropItemExactlyOnGameObj(GameObject itemToDrop, int itemsAmount = 1)
    {
        for (int i = 0; i < itemsAmount; i++)
        Instantiate(itemToDrop, 
            transform.position, Quaternion.identity);
    }
    protected void DropItemOnPosition(GameObject itemToDrop, Vector3 position,
        int itemsAmount = 1)

    {
        GameObject z;
        for (int i = 0; i < itemsAmount; i++)
        {
            z = Instantiate(itemToDrop, position,
               Quaternion.identity);
            z.transform.SetParent(gameObject.transform);
            var zRgb = z.GetComponent<Rigidbody2D>();
            zRgb.transform.position = position;
            z.transform.SetParent(null);
        }
    }
    protected void StartDeathTimer()
    {
        _startDeathTimer = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        var colider = gameObject.GetComponent<BoxCollider2D>();
        Destroy(colider);
    }
    protected void DeathTimer()
    {
        if (_startDeathTimer == false)
            return;

        if (_deathCountStart == 0)
            _deathCountStart = Time.time;

        if (Time.time - 2f >= _deathCountStart)
        {
            Destroy(gameObject);
        }
    }
    protected void SetupProperties(int hp, int requiedLevel,
        ItemType requiedToolType)
    {
        this._currentHp = hp;
        this._maxHp = hp;
        this._requiedToolLevel = requiedLevel;
        this._requiedToolType = requiedToolType;
    }
    protected bool NotUsingViableTool(Item item)
    {
        if (this._requiedToolLevel > item.Level ||
            this._requiedToolType != item.Type)
            return true;
        return false;
    }
    protected void DealDamage(Item item)
    {
        if (NotUsingViableTool(item))
            return;
        _currentHp -= item.Damage;
    }
    private bool CanPerformActionBelow(float percentage, bool boolToChange)
    {
        if (_currentHp > _maxHp * percentage || boolToChange)
            return false;
        boolToChange = true;
        return true;
    }
    protected bool CanPerformActionBelow80()
    {
        return CanPerformActionBelow(0.8f, _dealtActionOnBelow80);
    }
    protected bool CanPerformActionBelow60()
    {
        return CanPerformActionBelow(0.6f, _dealtActionOnBelow60);
    }
    protected bool CanPerformActionBelow40()
    {
        return CanPerformActionBelow(0.4f, _dealtActionOnBelow40);
    }
    protected bool CanPerformActionBelow20()
    {
        return CanPerformActionBelow(0.2f, _dealtActionOnBelow20);
    }
    protected bool CanPerformActionBelow0()
    {
        return CanPerformActionBelow(0f, _dealtActionOnNoHp);
    }
    protected class Particle
    {
        public GameObject ParticleObj { get; private set; }
        public float FallAnimationCounter { get; set; }
        public Vector3 StartPosition { get; set; }
        public float SpawnTime { get; set; }
        public float LifeSpan { get; private set; }
        public float CurveHeight { get; set; }
        public float FallDepth { get; set; }
        public float CurveWidth { get; set; }

        public Particle(GameObject particle)
        {
            ParticleObj = particle;
            FallAnimationCounter = 0f;
            LifeSpan = UnityEngine.Random.Range(1f, 1.5f);
            CurveHeight = UnityEngine.Random.Range(1f, 1.5f);
            FallDepth = UnityEngine.Random.Range(-2f, 2f);
            CurveWidth = Random.Range(-1f, 1f);
        }

        public Particle(GameObject particle, float curveWidth)
        {
            ParticleObj = particle;
            FallAnimationCounter = 0f;
            LifeSpan = UnityEngine.Random.Range(1f, 1.5f);
            CurveHeight = UnityEngine.Random.Range(1f, 1.5f);
            FallDepth = UnityEngine.Random.Range(-2f, 2f);
            CurveWidth = curveWidth;
        }
    }
    protected void CreateParticle(GameObject particle, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            var myParticle = Instantiate(particle,
                gameObject.transform.position, Quaternion.identity);

            var ParticleObj = new Particle(myParticle);
            _particles.Add(ParticleObj);
            ParticleObj.StartPosition = transform.position;
            ParticleObj.SpawnTime = Time.time;
        }
    }
    protected void CreateParticle(GameObject particle0,
        GameObject particle1, int amount)
    {
        GameObject myParticle;
        for (int i = 0; i < amount; i++)
        {
            if (i % 2 == 0)
                myParticle = Instantiate(particle0,
                   gameObject.transform.position, Quaternion.identity);
            else
                myParticle = Instantiate(particle1,
                gameObject.transform.position, Quaternion.identity);

            var ParticleObj = new Particle(myParticle);
            _particles.Add(ParticleObj);
            ParticleObj.StartPosition = transform.position;
            ParticleObj.SpawnTime = Time.time;
        }
    }
    protected void CreateParticle(GameObject particle0,
        GameObject particle1, GameObject particle2, int amount)
    {
        GameObject myParticle;
        for (int i = 0; i < amount; i++)
        {
            if (i % 3 == 0)
                myParticle = Instantiate(particle0,
                   gameObject.transform.position, Quaternion.identity);
            else if (i % 2 == 0)
                myParticle = Instantiate(particle1,
                gameObject.transform.position, Quaternion.identity);
            else
                myParticle = Instantiate(particle2,
                gameObject.transform.position, Quaternion.identity);

            var ParticleObj = new Particle(myParticle);
            _particles.Add(ParticleObj);
            ParticleObj.StartPosition = transform.position;
            ParticleObj.SpawnTime = Time.time;
        }
    }
    protected void CreateParticle(GameObject particle0,
        GameObject particle1, GameObject particle2, GameObject particle3,
        int amount)
    {
        GameObject myParticle;
        for (int i = 0; i < amount; i++)
        {
            if (i % 4 == 0)
                myParticle = Instantiate(particle0,
                   gameObject.transform.position, Quaternion.identity);
            else if (i % 3 == 0)
                myParticle = Instantiate(particle1,
                gameObject.transform.position, Quaternion.identity);
            else if (i % 2 == 0)
                myParticle = Instantiate(particle2,
                gameObject.transform.position, Quaternion.identity);
            else
                myParticle = Instantiate(particle3,
                gameObject.transform.position, Quaternion.identity);

            var ParticleObj = new Particle(myParticle);
            _particles.Add(ParticleObj);
            ParticleObj.StartPosition = transform.position;
            ParticleObj.SpawnTime = Time.time;
        }
    }
    protected void CreateParticleAtPosition(GameObject particle0,
        GameObject particle1, GameObject particle2, GameObject particle3,
        int amount, Vector3 position)
    {
        GameObject myParticle;
        for (int i = 0; i < amount; i++)
        {
            if (i % 4 == 0)
                myParticle = Instantiate(particle0,
                   position, Quaternion.identity);
            else if (i % 3 == 0)
                myParticle = Instantiate(particle1,
                position, Quaternion.identity);
            else if (i % 2 == 0)
                myParticle = Instantiate(particle2,
                position, Quaternion.identity);
            else
                myParticle = Instantiate(particle3,
                position, Quaternion.identity);

            var ParticleObj = new Particle(myParticle);
            _particles.Add(ParticleObj);
            ParticleObj.StartPosition = position;
            ParticleObj.SpawnTime = Time.time;
        }
    }

    private void MoveParticles()
    {
        for (int i = 0; i < _particles.Count; i++)
        {
            if (_particles[i] == null)
                continue;

            MoveParticle(_particles[i], _particles[i].CurveWidth,
                _particles[i].CurveHeight, _particles[i].FallDepth);
            //DeleteOldParticles();
        }
        DeleteOldParticles();
    }
    private void MoveParticle(Particle particle, float curveWidth,
        float curveHeight, float fallDepth)
    {
        var animationSpeed = 1.5f;
        var animationCounter = particle.FallAnimationCounter;
        var _curveWidth = curveWidth;
        var _curveHeight = curveHeight;
        var _fallDepth = fallDepth;

        var point0 = particle.StartPosition;
        var point2 = point0 + Vector3.right * _curveWidth +
        Vector3.down * _fallDepth;
        var point1 = point0 + (point2 - point0) /
            2 + Vector3.up * _curveHeight;

        animationCounter += animationSpeed * Time.deltaTime;
        particle.FallAnimationCounter = animationCounter;

        Vector3 m1 = Vector3.Lerp(point0, point1, animationCounter);
        Vector3 m2 = Vector3.Lerp(point1, point2, animationCounter);

        particle.ParticleObj.transform.position =
            Vector3.Lerp(m1, m2, animationCounter);
    }
    private void DeleteOldParticles()
    {
        for (var i = 0; i < _particles.Count; i++)
        {
            var part = _particles[i];

            if (Time.time - part.SpawnTime > part.LifeSpan)
            {
                Destroy(part.ParticleObj);
                _particles.Remove(part);
            }
        }
    }
    private void SetSortingOrderWithChild()
    {
        var sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (var sprite in sprites)
        {
            if (sprite.gameObject == gameObject)
                _sprRenParent = sprite;
            else
                _sprRenChild = sprite;
        }
        var offset = 1f;
        _sprRenChild.sortingOrder = (int)(100 - (transform.position.y + offset)) * 2;
        _sprRenParent.sortingOrder = _sprRenChild.sortingOrder - 1;
    }
    private void SetSortingOrderWithNoChild()
    {
        _sprRenParent = GetComponent<SpriteRenderer>();
        _sprRenParent.sortingOrder = (int)(100 - (transform.position.y)) * 2;
    }
    private void SetSortingOrder()
    {
        if (gameObject.transform.childCount == 0)
            SetSortingOrderWithNoChild();
        else
            SetSortingOrderWithChild();
    }
}