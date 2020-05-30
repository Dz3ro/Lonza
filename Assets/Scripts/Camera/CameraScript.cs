using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float MoveSpeed = 5f;
    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

   

    private void LateUpdate()
    {
        //var playerPos = _player.transform.position;
        //var targetPos = playerPos + new Vector3(0f, 0f, -10f);
        //transform.position = Vector3.Lerp(transform.position, targetPos, MoveSpeed);
    }
    private void Update()
    {
        var playerPos = _player.transform.position;
        var targetPos = playerPos + new Vector3(0f, 0f, -10f);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, MoveSpeed);
    }
}
