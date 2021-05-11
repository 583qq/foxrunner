using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;

public class Road : MonoBehaviour
{
    // Road chunks container
    private LinkedList<RoadChunkObject> _roadChunks;

    [SerializeReference] private List<GameObject> _chunkPrefabs;
    [SerializeField] private GameObject _startChunkPrefab;
    [SerializeField] private int _chunksInFOV;

    private Vector3 _startPosition = Vector3.zero;
    private Vector3 _position;

    private bool _isLastChunk;

    [SerializeField] private float _roadMovement = 5;
    [SerializeField] private float _roadChunkSpawnTime = 2.5f;

    [HideInInspector] public float maxDistance;

    private IBunGenerator _bunGenerator;
    private IChunkGenerator _chunkGenerator;
    private IChunkGenerator _initChunkGenerator;

    public void Start()
    {
        Random.InitState((int) DateTime.Now.Ticks);

        _isLastChunk = false;

        _roadChunks = new LinkedList<RoadChunkObject>();

        _position = _startPosition;

        _bunGenerator = new ClassicBunGenerator(5, 10);
        _chunkGenerator = new ClassicChunkGenerator(_bunGenerator, _chunkPrefabs, _roadChunks);
        _initChunkGenerator = new InitialChunkGenerator(_bunGenerator, _startChunkPrefab, _roadChunks);

        InstantiateFOVChunks();

        StartCoroutine(InstantiateNextChunk());
    }

    private void InstantiateFOVChunks()
    {
        Debug.Log($"New {_chunksInFOV} road chunks created.");

        for(int i = 0; i < _chunksInFOV; i++)
        {
           _initChunkGenerator.GenerateChunk(_position, transform);
           _position += Vector3.forward * _roadMovement;
        }
    }

    public void Update()
    {
        // We are somewhat too far, so reset
        if(_position.z > maxDistance)
        {
            _position = _startPosition;
            InstantiateFOVChunks();
        }
    }

    IEnumerator InstantiateNextChunk()
    {
        yield return new WaitForSeconds(_roadChunkSpawnTime);

        SpawnChunk();

        _position += Vector3.forward * _roadMovement;

        if(_isLastChunk)
            yield break;
            
        StartCoroutine(InstantiateNextChunk());
    }

    private void SpawnChunk()
    {
        _chunkGenerator.GenerateChunk(_position, transform);
    }

}