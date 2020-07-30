using UnityEngine;

public class RockBig : ObjectInteractiveWithTool
{
    [SerializeField] private Sprite SpriteOnHp80;
    [SerializeField] private Sprite SpriteOnHp60;
    [SerializeField] private Sprite SpriteOnHp40;
    [SerializeField] private Sprite SpriteOnHp20;
    [SerializeField] private GameObject ItemToDrop;
    [SerializeField] private GameObject Particle0;
    [SerializeField] private GameObject Particle1;
    [SerializeField] private GameObject Particle2;
    [SerializeField] private GameObject Particle3;

    private new void Start()
    {
        base.Start();
        SetupProperties(200, 0, ItemType.Pickaxe);
    }

    private new void Update()
    {
        base.Update();
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
        DropRockParticles();

        if (CanPerformOneTimeActionBelow80())
            PerformOneTimeActionBelow80();
        if (CanPerformOneTimeActionBelow60())
            PerformOneTimeActionBelow60();
        if (CanPerformOneTimeActionBelow40())
            PerformOneTimeActionBelow40();
        if (CanPerformOneTimeActionBelow20())
            PerformOneTimeActionBelow20();
        if (CanPerformOneTimeActionBelow0())
            PerformOneTimeActionOnDeath();

    }

    private void DropRockParticles()
    {
        base.CreateParticle(Particle0, Particle1,
            Particle2, Particle3, 8);
    }

    private new void PerformOneTimeActionBelow80()
    {
        base.CanPerformOneTimeActionBelow80();
        ChangeSprite(SpriteOnHp80);
    }

    private new void PerformOneTimeActionBelow60()
    {
        base.CanPerformOneTimeActionBelow60();
        ChangeSprite(SpriteOnHp60);
    }

    private new void PerformOneTimeActionBelow40()
    {
        base.CanPerformOneTimeActionBelow40();
        ChangeSprite(SpriteOnHp40);
    }

    private new void PerformOneTimeActionBelow20()
    {
        base.CanPerformOneTimeActionBelow20();
        ChangeSprite(SpriteOnHp20);
    }

    private new void PerformOneTimeActionOnDeath()
    {
        base.PerformOneTimeActionOnDeath();
        DropItemExactlyOnGameObj(ItemToDrop, 6);
        StartDeathTimer();
    }

    private void ChangeSprite(Sprite sprite)
    {
        var spriteRen = GetComponent<SpriteRenderer>();
        spriteRen.sprite = sprite;
    }
}