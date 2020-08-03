using UnityEngine;

public class WoodenFloor : Pickable
{
    private new void Awake()
    {
        base.Awake();
    }

    private new void Start()
    {
        base.Start();
    }

    private void Update()
    {
    }

    public override void WhenPlayerInteracts()
    {
        var floorWooden = _allItems.FloorWooden;
        PickItem(floorWooden);
        GameObject.FindGameObjectWithTag("DetectorAfterDeath")
            .GetComponent<SpriteManagerAfterDeath>().ChangeSpritesAround(gameObject.name, transform.position);
    }
}