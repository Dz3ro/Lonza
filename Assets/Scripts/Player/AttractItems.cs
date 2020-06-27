using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractItems : MonoBehaviour
{
    private float _attractionSpeed = 0.06f;
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

        var justDropped = collision.gameObject
            .GetComponent<Collectable>().JustDroppped;

        if (justDropped)
            return;

        collision.gameObject.transform.position = 
            Vector3.MoveTowards(collision.gameObject.transform.position, 
            transform.position, _attractionSpeed);
    }


}
