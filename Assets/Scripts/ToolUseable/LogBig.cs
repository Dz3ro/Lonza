using UnityEngine;

public class LogBig : ObjectInteractiveWithTool
{
    [SerializeField] private GameObject ItemToDrop;
    private int _hp = 50;
    private bool startDeathTimer = false;

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
            DropItem(4);
            StartDeathTimer();
        }
    }

    private void DropItem(int itemsAmount = 1)
    {
        for (int i = 0; i < itemsAmount; i++)
            Instantiate(ItemToDrop, transform.position,
               Quaternion.identity);
    }

    private float _deathCountStart;

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