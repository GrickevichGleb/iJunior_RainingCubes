using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class ColorCube : MonoBehaviour
{
    [SerializeField] private int _lifespanMin = 2;
    [SerializeField] private int _lifespanMax = 5;

    private MeshRenderer _meshRenderer;
    private Rigidbody _rigidbody;
    
    private bool _hasHit = false;
    private Color _colorInit;
    public event Action<ColorCube> RequestRelease;
    
    private void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _meshRenderer = gameObject.GetComponent<MeshRenderer>();
        
        _colorInit = _meshRenderer.material.color;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_hasHit == true) 
            return;
        
        if (!other.gameObject.TryGetComponent(out Ground _))
            return;
        
        _hasHit = true;
        
        SetRandomColor();
        SetRandomLifespan();
    }

    public void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _meshRenderer.material.color = _colorInit;
        _hasHit = false;
    }

    private void SetRandomColor()
    {
        _meshRenderer.material.color = UtilsRandom.GetRandomColor();
    }

    private void SetRandomLifespan()
    {
        float lifespan = UtilsRandom.GetRandomNumber(_lifespanMin, _lifespanMax);

        StartCoroutine(ReleaseAfter(lifespan));
    }

    private IEnumerator ReleaseAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        RequestRelease?.Invoke(this);
    }
}
