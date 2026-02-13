using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ColorCube _colorCubePref;
    [Space] 
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 100;
    [SerializeField] private float _spawnRate = 1f;
    
    private Collider _collider;

    private Vector3 _colliderCenter;

    private float _spawnInterval;
    private float _extentX;
    private float _extentZ;

    private bool _isSpawning = true;

    private ObjectPool<ColorCube> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<ColorCube>(
            createFunc: () => Instantiate(_colorCubePref),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
        
        gameObject.TryGetComponent(out _collider);

        _spawnInterval = 1 / _spawnRate;
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes(_spawnInterval));
    }
    
    private void ActionOnGet(ColorCube cube)
    {
        cube.Reset();
        cube.transform.position = GetSpawnPoint();
        cube.RequestRelease += OnRequestRelease;
        cube.gameObject.SetActive(true);
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

    private void OnRequestRelease(ColorCube cube)
    {
        cube.RequestRelease -= OnRequestRelease;
        _pool.Release(cube);
    }

    private IEnumerator SpawnCubes(float interval)
    {
        var delaySeconds = new WaitForSeconds(interval);
        
        while (_isSpawning)
        {
            yield return delaySeconds;
            
            _pool.Get();
        }
    }
}
