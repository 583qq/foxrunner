using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
public class Coin : IGameBun
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _points;
    [SerializeField] private int _maxInChunk;

    public GameObject prefab { get => _prefab; }
    public int points { get => _points; }

    private Player player;
    private CoinComponent component;


    public Coin(GameObject coinPrefab, int coinPoints)
    {
        player = GameReferences.player;

        _prefab = coinPrefab;
        _points = coinPoints;
        
        component = _prefab.AddComponent<CoinComponent>();
        component.bun = this;
    }

    public void OnTake()
    {
        player.AddPoints(_points);
    }

    public int GetMaxInChunk() => _maxInChunk;
}

public class CoinComponent : BaseBunComponent
{
    public override IGameBun bun {get; set;}
}

public class CoinFactory : IBunFactory
{
    private List<Coin> _prefabs;
    
    public void InitPrefabs()
    {   
        _prefabs = GameReferences.settings.coins;
    }

    public IGameBun Produce(Vector3 position, Transform parent)
    {
        InitPrefabs();

        if(_prefabs.Count < 1)
        {
            Debug.Log("Coin prefabs are missing!");
            return new UnknownBun();
        }

        // Get random coin from coin prefabs
        var prefabIndex = Random.Range(0, _prefabs.Count);
        var prefab = _prefabs[prefabIndex];

        var coinObject = GameObject.Instantiate(prefab.prefab, position, Quaternion.identity, parent);

        return new Coin(coinObject, prefab.points);
    }
}