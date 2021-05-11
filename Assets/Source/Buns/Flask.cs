using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

[Serializable]
public class Flask : IGameBun
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _heal;
    [SerializeField] private int _maxInChunk;

    private Player player;

    public GameObject prefab { get => _prefab; }
    public int heal { get => _heal; }

    private FlaskComponent component;

    public Flask(GameObject flaskPrefab, int flaskHeal)
    {
        player = GameReferences.player;

        _prefab = flaskPrefab;
        _heal = flaskHeal;

        component = _prefab.AddComponent<FlaskComponent>();
        component.bun = this;
    }

    public void OnTake()
    {
        player.Heal(heal);
    }

    public int GetMaxInChunk() => _maxInChunk;
}

public class FlaskComponent : BaseBunComponent
{
    public override IGameBun bun {get; set;}

}

public class FlaskFactory : IBunFactory
{
    private List<Flask> _prefabs;

    public void InitPrefabs()
    {
        _prefabs = GameReferences.settings.flasks;
    }

    public IGameBun Produce(Vector3 position, Transform parent)
    {
        InitPrefabs();

        if(_prefabs.Count < 1)
        {
            Debug.Log("Flask prefabs are missing!");
            return new UnknownBun();
        }

        var prefabIndex = Random.Range(0, _prefabs.Count);
        var prefabObject = _prefabs[prefabIndex];

        var flaskObject = GameObject.Instantiate(prefabObject.prefab, position, Quaternion.identity, parent);

        return new Flask(flaskObject, prefabObject.heal);
    }
}