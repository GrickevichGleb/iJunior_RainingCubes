using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected Spawnable spawnablePref;
    [Space] 
    [SerializeField] protected int _poolCapacity = 10;
    [SerializeField] protected int _poolMaxSize = 100;

    protected ObjectPool<Spawnable> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<Spawnable>(
            createFunc: () => Instantiate(spawnablePref),
            actionOnGet: (spawnable) => ActionOnGet(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => Destroy(spawnable.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }
    
    protected virtual void ActionOnGet(Spawnable spawnable)
    {
        spawnable.Reset();
        spawnable.RequestRelease += OnRequestRelease;
    }
    
    private void OnRequestRelease(Spawnable spawnable)
    {
        spawnable.RequestRelease -= OnRequestRelease;
        _pool.Release(spawnable);
    }
}
