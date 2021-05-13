using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;


public interface IChunkGenerator
{
    void GenerateChunk(Vector3 position, Transform parent);
}


public class ClassicChunkGenerator : IChunkGenerator
{
    private IBunGenerator _bunGenerator;
    private List<GameObject> _chunkPrefabs;
    private LinkedList<RoadChunkObject> _chunkObjects;


    public ClassicChunkGenerator(IBunGenerator bunGenerator, List<GameObject> chunkPrefabs, LinkedList<RoadChunkObject> chunkObjects)
    {
        _bunGenerator = bunGenerator;
        _chunkPrefabs = chunkPrefabs;
        _chunkObjects = chunkObjects;
    }
    
    public void GenerateChunk(Vector3 position, Transform parent)
    {
        int prefabCount = _chunkPrefabs.Count;

        if(prefabCount < 1)
        {
            Debug.LogError("Chunk prefab list is empty!");
            return;
        }

        int selectedIndex = Random.Range(0, prefabCount);
        var selectedPrefab = _chunkPrefabs[selectedIndex];

        Debug.Log($"New Road Chunk => Selected Chunk: {selectedPrefab.name}");

        // Spawn new
        GameObject chunkGO = GameObject.Instantiate(selectedPrefab, position, Quaternion.identity, parent);

        // Destroy first 
        _chunkObjects.First.Value.Dispose();
        _chunkObjects.RemoveFirst();

        RoadChunkObject chunk = new RoadChunkObject(chunkGO);

        chunk.GenerateBuns(_bunGenerator);

        _chunkObjects.AddLast(chunk);
    }
}

public class InitialChunkGenerator : IChunkGenerator
{
    private IBunGenerator _bunGenerator;
    private GameObject _initialChunkPrefab;
    private LinkedList<RoadChunkObject> _chunkObjects; 

    public InitialChunkGenerator(IBunGenerator bunGenerator, GameObject initialChunkPrefab, LinkedList<RoadChunkObject> chunkObjects)
    {
        _bunGenerator = bunGenerator;
        _initialChunkPrefab = initialChunkPrefab;
        _chunkObjects = chunkObjects;
    }

    public void GenerateChunk(Vector3 position, Transform parent)
    {
        GameObject roadChunk = GameObject.Instantiate(_initialChunkPrefab, position, Quaternion.identity, parent);

        RoadChunkObject chunk = new RoadChunkObject(roadChunk);
            
        chunk.GenerateBuns(_bunGenerator);

        _chunkObjects.AddLast(chunk);
    }

}