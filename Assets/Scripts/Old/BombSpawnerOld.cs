using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawnerOld : SpawnerOld
{
    [SerializeField] private SpawnerOld cubeSpawnerOld;
    
    private Vector3 _bombSpawnPos;

    private void OnEnable()
    {
        cubeSpawnerOld.Spawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        cubeSpawnerOld.Spawned -= OnCubeSpawned;
    }

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        spawnable.transform.position = _bombSpawnPos;
    }

    private void SpawnBomb(Vector3 spawnPos)
    {
        _bombSpawnPos = spawnPos;
        
        _pool.Get();
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
