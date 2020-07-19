using UnityEngine;
using UnityEngine.EventSystems;

public class LogBig : ObjectInteractiveWithTool
{
    [SerializeField] private GameObject ItemToDrop;
    [SerializeField] private GameObject particle0;
    [SerializeField] private GameObject particle1;
    [SerializeField] private GameObject particle2;
    [SerializeField] private GameObject particle3;



    public override void OnToolUse(Item item)
    {
        base.DealDamage(item);
        base.CreateParticle(particle0, particle1, particle2, particle3, 
            4);

        if (CanPerformActionBelow0())
        {
            base.DropItemAroundGameObj(ItemToDrop, 4);
            base.StartDeathTimer();
        }
    }

    private new void Awake()
    {
        base.SetupProperties(100, 0, ItemType.Axe);
    }

    private new void Start()
    {
        base.Start();
    }

    private new void Update()
    {
        base.Update();
    }
}