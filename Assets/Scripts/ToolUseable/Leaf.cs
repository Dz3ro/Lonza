using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leaf : MonoBehaviour
{
    private float _spawnTime;
    private float _lifeSpan = 3f;
    private float _rotationValue;
    private float _movementValueX;
    private float _movementValueY;
    void Start()
    {
        SetValues();
    }

    void Update()
    {
        MoveObject();
        KillYourSelf();
    }
    private void SetValues()
    {
        _spawnTime = Time.time;
        _lifeSpan = Random.Range(2f, 4f);
        _rotationValue = Random.Range(-200f, 200f);
        _movementValueX = Random.Range(-1f, 1f);
        _movementValueY = Random.Range(-0.2f, -0.8f);
    }
    private void MoveObject()
    {
        transform.position += new Vector3(_movementValueX, 
            _movementValueY, 0f) * Time.deltaTime;
        transform.eulerAngles += new Vector3(0, 0, _rotationValue)
            * Time.deltaTime;
    }
    private void KillYourSelf()
    {
        if (_spawnTime + _lifeSpan < Time.time)
            Destroy(gameObject);
    }
}
