using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnable : MonoBehaviour
{
    protected MeshRenderer _meshRenderer;
    protected Rigidbody _rigidbody;

    public event Action<Spawnable> RequestRelease;

    public virtual void Reset() { }
    
    protected void Release()
    {
        RequestRelease?.Invoke(this);
    }
}
