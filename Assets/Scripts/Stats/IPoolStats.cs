using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolStats
{
    public event Action<bool, int, int> UpdateStats;
}
