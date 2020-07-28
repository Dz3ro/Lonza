using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Water : MonoBehaviour
{
    private List<Item> _fishesInWater;
    protected ItemsCreator _allItems;

    private void Awake()
    {
        _allItems = GameObject.FindGameObjectWithTag("Inventory")
            .GetComponent<ItemsCreator>();
    }

    protected void Start()
    {
        _fishesInWater = new List<Item>();
    }

    private void Update()
    {
    }

    protected void AddFishToWater(Item fish, int ChanceToAppearInPercentage)
    {
        for (int i = 0; i < ChanceToAppearInPercentage; i++)
            _fishesInWater.Add(fish);

        if (_fishesInWater.Count > 100)
            throw new Exception("total percentage of fishes is more than 100%");
    }

    protected Item GetRandomFish()
    {
        var rnd = UnityEngine.Random.Range(0, _fishesInWater.Count + 1);

        return _fishesInWater[rnd];
    }

    public virtual void GetFish()
    {

    }
}