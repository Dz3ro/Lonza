using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBig : ObjectInteractiveWithTool
{
    [SerializeField] private GameObject ItemToDrop;
    private int _hp = 50;
    bool startDeathTimer = false;

    public override void OnToolUse(Item item)
    {

        if (item.Name != "Axe")
            return;
        ItemHit();
    }

    private void ItemHit()
    {
        _hp -= 10;
        if (_hp <= 0)
        {
            DropItem(0, 0);
            DropItem(-0.3f, 0);
            DropItem(0.3f, 0);
            DropItem(0.5f, 0);
            DropItem(-0.5f, 0);

            DropItem(-0.3f, -0.3f);
            DropItem(0.3f, 0.3f);
            DropItem(0.5f, 0.3f);
            DropItem(-0.5f, -0.3f);




            StartDeathTimer();


        }
    }

    private void DropItem(float distFromParentX, float distFromParentY)
    {
        var item = Instantiate(ItemToDrop, transform.position, 
            Quaternion.identity);
        DropItemAnimation(item, distFromParentX, distFromParentY);
    }

    private float count;
    private void DropItemAnimation(GameObject item, 
        float distFromParentX, float distFromParentY, 
        float distJourneyX, float distJourneyY,
        float animationSpeed, float curveHeight)
    {
        var startX = distFromParentX;
        var startY = distFromParentY;
        var distX = distJourneyX;
        var distY = distJourneyY;
        var animSpeed = animationSpeed;
        var cHght = curveHeight;

        var point0 = gameObject.transform.position
            + Vector3.right * startX
            + Vector3.up * startY;
        var point2 = point0 + Vector3.right * distX;
        point2 += Vector3.up * distY;
        var point1 = point0 + (point2 - point0) / 2 + Vector3.up * cHght;

        if (count < 50.0f)
        {
            count += animSpeed * Time.deltaTime;

            Vector3 m1 = Vector3.Lerp(point0, point1, count);
            Vector3 m2 = Vector3.Lerp(point1, point2, count);
            item.transform.position = Vector3.Lerp(m1, m2, count);

            
        }
    }

    private void DropItemAnimation(GameObject item,
        float distFromParentX, float distFromParentY)
    {
        DropItemAnimation(item, distFromParentX, distFromParentY,
            2f, 2f, 2f, 2f);
    }

    float _deathCountStart;

    private void StartDeathTimer()
    {
        startDeathTimer = true;
        gameObject.GetComponent<SpriteRenderer>().color = Color.clear;
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void DeathTimer()
    {
        if (startDeathTimer == false)
            return;
        
        if (_deathCountStart == 0)
            _deathCountStart = Time.time;

        if (Time.time - 10f >= _deathCountStart)
        {
            Destroy(gameObject);
        }

    }
    private void Update()
    {
        DeathTimer();
    }


}
