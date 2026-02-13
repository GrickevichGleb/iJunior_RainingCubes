using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider _collider;

    private Vector3 _colliderCenter;
    private float _extentX;
    private float _extentZ;
    
    private void Awake()
    {
        gameObject.TryGetComponent(out _collider);
    }

    private void Start()
    {
        SetSpawnArea();
    }

    private void SetSpawnArea()
    {
        _colliderCenter = _collider.bounds.center;
        
        _extentX = _collider.bounds.extents.x;
        _extentZ = _collider.bounds.extents.z;
    }

    private Vector3 GetSpawnPoint()
    {
        float x = 
            UtilsRandom.GetRandomNumber(Convert.ToInt32(_colliderCenter.x - _extentX), 
                Convert.ToInt32(_colliderCenter.x + _extentX));
        float z = 
            UtilsRandom.GetRandomNumber(Convert.ToInt32(_colliderCenter.z - _extentZ), 
                Convert.ToInt32(_colliderCenter.z + _extentZ));

        return new Vector3(x, _colliderCenter.y, z);
    }
}
