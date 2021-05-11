using System;
using System.Collections.Generic;
using UnityEngine;

using Random = UnityEngine.Random;


[Serializable]
public enum Bun
{
    Coin,
    Bomb,
    Flask
}

public interface IBunFactory
{
    IGameBun Produce(Vector3 position, Transform parent);
}

// ! OCP
public static class BunFactory
{
    public static IBunFactory GetFactory(Bun bun)
    {
        switch(bun)
        {
            case Bun.Coin:
                return new CoinFactory();

            case Bun.Bomb:
                return new BombFactory();
            
            case Bun.Flask:
                return new FlaskFactory();

            default:
                return new UknownBunFactory();
        }
    }
}

public abstract class BaseBunComponent : MonoBehaviour
{
    public abstract IGameBun bun { get; set; }

    public virtual void OnCollisionEnter(Collision collision)
    {
        bun.OnTake();

        Destroy(gameObject);
    }
}


public class UnknownBun : IGameBun
{
    public void OnTake()
    {
        Debug.Log("UKNOWN BUN TAKEN. SOMETHING WENT WRONG.");
    }

    public int GetMaxInChunk() => 0;
}

public class UknownBunFactory : IBunFactory
{
    public IGameBun Produce(Vector3 position, Transform parent)
    {
        return new UnknownBun();
    }
}