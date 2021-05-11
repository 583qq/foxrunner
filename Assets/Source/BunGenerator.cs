using UnityEngine;

using System;
using System.Collections.Generic;

using Random = UnityEngine.Random;


public interface IBunGenerator
{
    void GenerateBuns(Transform chunk);
}


public class ClassicBunGenerator : IBunGenerator
{
    private int _baseWidth;
    private int _baseHeight;

    private int _maxBuns => _baseWidth * _baseHeight;
    private List<IGameBun> _bunPrefabs;
    private List<ChunkMaximum> _bunChunkMax;

    private const int colOffset = -2;
    private const int rowOffset = 0;

    public ClassicBunGenerator(int baseWidth, int baseHeight)
    {
        _bunChunkMax = GameReferences.settings.maxInChunks;

        _baseWidth = baseWidth;
        _baseHeight = baseHeight;
    }

    public void GenerateBuns(Transform chunk)
    {
        int generatedBunCount = 0;
        bool[,] bunGrid = new bool[_baseWidth, _baseHeight];

        foreach(Bun bun in Enum.GetValues(typeof(Bun)))
        {
            if(_maxBuns < generatedBunCount)
                break;

            ChunkMaximum chunkMax = _bunChunkMax.Find(x => x.bun == bun);

            if(chunkMax == null)
            {
                Debug.Log($"No maxInChunk for {bun} was found.");
                continue;
            }

            int maxInChunk = chunkMax.value; 

            int buns = Random.Range(0, maxInChunk);
            generatedBunCount += buns;

            IBunFactory factory = BunFactory.GetFactory(bun);

            for(int i = 0; i < buns; i++)
            {
                int col = Random.Range(0, _baseWidth);
                int row = Random.Range(0, _baseHeight);

                if(bunGrid[col, row])
                    continue;

                bunGrid[col, row] = true;

                Vector3 offsetPosition = new Vector3(col + colOffset, 0, row + rowOffset);
                Vector3 position = chunk.position + offsetPosition;

                IGameBun generatedBun = factory.Produce(position, chunk);
            }
        }
    }

}