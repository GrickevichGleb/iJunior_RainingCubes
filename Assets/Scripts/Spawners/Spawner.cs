using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour, IPoolStats where T : Spawnable
{
    [SerializeField] protected T SpawnablePref;
    [Space] 
    [SerializeField] protected int PoolCapacity = 10;
    [SerializeField] protected int PoolMaxSize = 100;

    protected ObjectPool<T> Pool;

    public event Action<T> Spawned;
    public event Action<bool, int, int> UpdateStats; 

    private void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: () => Instantiate(SpawnablePref),
            actionOnGet: (spawnable) => ActionOnGet(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => Destroy(spawnable.gameObject),
            collectionCheck: true,
            defaultCapacity: PoolCapacity,
            maxSize: PoolMaxSize);
    }
    
    protected virtual void ActionOnGet(Spawnable spawnable)
    {
        spawnable.Reset();
        spawnable.RequestRelease += OnRequestRelease;
        
        Spawned?.Invoke((T)spawnable);
        UpdateStats?.Invoke(true, Pool.CountAll, Pool.CountActive);
    }
    
    private void OnRequestRelease(Spawnable spawnable)
    {
        spawnable.RequestRelease -= OnRequestRelease;
        Pool.Release((T)spawnable);
        
        UpdateStats?.Invoke(false, Pool.CountAll, Pool.CountActive);
    }
}
