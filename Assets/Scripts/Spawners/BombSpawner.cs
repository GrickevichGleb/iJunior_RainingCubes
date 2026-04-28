using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb> 
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    
    private Vector3 _bombSpawnPos;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= OnCubeSpawned;
    }

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        spawnable.transform.position = _bombSpawnPos;
    }

    private void SpawnBomb(Vector3 spawnPos)
    {
        _bombSpawnPos = spawnPos;
        
        Pool.Get();
    }

    private void OnCubeSpawned(Spawnable colorCube)
    {
        colorCube.RequestRelease += OnCubeReleased;
    }

    private void OnCubeReleased(Spawnable colorCube)
    {
        colorCube.RequestRelease -= OnCubeReleased;
        
        SpawnBomb(colorCube.transform.position);
    }
}
