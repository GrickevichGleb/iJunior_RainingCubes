using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _colorCubePref;
    [Space] 
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private float _spawnRate = 1f;
    
    private Collider _collider;

    private Vector3 _colliderCenter;

    private float _spawnInterval;
    private float _extentX;
    private float _extentZ;

    private ObjectPool<GameObject> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_colorCubePref),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
        
        gameObject.TryGetComponent(out _collider);

        _spawnInterval = 1 / _spawnRate;
    }

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0f, _spawnInterval);
    }

    private void SpawnCube()
    {
        _pool.Get();
    }

    private void ActionOnGet(GameObject obj)
    {
        obj.transform.position = GetSpawnPoint();

        if (obj.TryGetComponent(out ColorCube cube))
        {
            cube.Reset();
            cube.RequestRelease += OnRequestRelease;
        }
            
        obj.SetActive(true);
    }
    
    private void SetSpawnArea()
    {
        _colliderCenter = _collider.bounds.center;
        
        _extentX = _collider.bounds.extents.x;
        _extentZ = _collider.bounds.extents.z;
    }

    private Vector3 GetSpawnPoint()
    {
        SetSpawnArea();
        
        float x = 
            UtilsRandom.GetRandomNumber(Convert.ToInt32(_colliderCenter.x - _extentX), 
                Convert.ToInt32(_colliderCenter.x + _extentX));
        float z = 
            UtilsRandom.GetRandomNumber(Convert.ToInt32(_colliderCenter.z - _extentZ), 
                Convert.ToInt32(_colliderCenter.z + _extentZ));

        return new Vector3(x, _colliderCenter.y, z);
    }

    private void OnRequestRelease(GameObject obj)
    {
        if (obj.TryGetComponent(out ColorCube cube))
            cube.RequestRelease -= OnRequestRelease;
        
        _pool.Release(obj);
    }
}
