using UnityEngine;


public class RoadChunkObject
{
    public GameObject _baseRoadChunkObject {get; private set;}

    // private int _baseWidth = 5;
    // private int _baseHeight = 10;

    public RoadChunkObject(GameObject basePrefab)
    {
        _baseRoadChunkObject = basePrefab;
    }

    public void Dispose() => GameObject.Destroy(_baseRoadChunkObject);
    public bool isDisposed => _baseRoadChunkObject is null ? true : false;

    public void GenerateBuns(IBunGenerator generator)
    {
        generator.GenerateBuns(_baseRoadChunkObject.transform);
    }
}
