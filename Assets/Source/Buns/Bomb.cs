using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;


[Serializable]
public class Bomb : IGameBun
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _damage;
    [SerializeField] private int losePoints = 100;
    [SerializeField] private int _maxInChunk;

    public GameObject prefab { get => _prefab; }
    public int damage { get => _damage; }

    private Player player;

    private BombComponent component;

    public Bomb(GameObject bombPrefab, int bombDamage)
    {
        player = GameReferences.player;

        _prefab = bombPrefab;
        _damage = bombDamage;

        component = _prefab.AddComponent<BombComponent>();
        component.bun = this;
    }

    public void OnTake()
    {
        player.TakeDamage(damage);
        player.AddPoints(losePoints);
    }

    public int GetMaxInChunk() => _maxInChunk;
}


public class BombComponent : BaseBunComponent
{
    public override IGameBun bun {get; set;} 
}

public class BombFactory : IBunFactory
{
    private List<Bomb> _prefabs;

    public void InitPrefabs()
    {
        _prefabs = GameReferences.settings.bombs;
    }

    public IGameBun Produce(Vector3 position, Transform parent)
    {
        InitPrefabs();

        if(_prefabs.Count < 1)
        {
            Debug.Log("Bomb prefabs are missing!");
            return new UnknownBun();
        }

        var prefabIndex = Random.Range(0, _prefabs.Count);
        var prefabObject = _prefabs[prefabIndex];

        var prefabRotation = prefabObject.prefab.transform.rotation;

        var bombObject = GameObject.Instantiate(prefabObject.prefab, position, prefabRotation, parent);


        return new Bomb(bombObject, prefabObject.damage);
    }
}