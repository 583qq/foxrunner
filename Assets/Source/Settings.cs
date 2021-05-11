using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ChunkMaximum
{
    public Bun bun;
    public int value;
}

public class Settings : MonoBehaviour
{
    public Player player;
    public Road road;

    // Too Far => reset to 0.
    [SerializeField] private float resetDistance = 10000;
    
    // Prefabs
    public List<ChunkMaximum> maxInChunks;  // Should use 'IGameBun' property?
    public List<Coin> coins;
    public List<Bomb> bombs;
    public List<Flask> flasks;

    public void Awake()
    {
        // References
        GameReferences.SetSettingsReference(this);
        GameReferences.SetPlayerReference(player);

        player.maxDistance = resetDistance;
        road.maxDistance = resetDistance;
    }
}