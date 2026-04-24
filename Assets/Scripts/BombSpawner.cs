using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner
{
    private Vector3 _bombSpawnPos;
    
    public void SpawnBomb(Vector3 spawnPos)
    {
        _bombSpawnPos = spawnPos;
        
        _pool.Get();
    }

    protected override void ActionOnGet(Spawnable spawnable)
    {
        base.ActionOnGet(spawnable);

        spawnable.transform.position = _bombSpawnPos;
    }
}
