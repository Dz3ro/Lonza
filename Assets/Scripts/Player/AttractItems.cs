﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractItems : MonoBehaviour
{
    private readonly float _attractionSpeed = 0f;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Collectable")
            return;

        

        

        collision.gameObject.transform.position = 
            Vector3.MoveTowards(collision.gameObject.transform.position, 
            transform.position, _attractionSpeed);
    }


}
