using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour
{
    private const string PlatformTag = "Platform";
    
    [SerializeField] private int _lifespanMin = 2;
    [SerializeField] private int _lifespanMax = 5;

    private MeshRenderer _meshRenderer;
    
    private bool _hasHit = false;
    private Color _colorInit;
    public event Action<GameObject> RequestRelease;
    
    private void Awake()
    {
        if (gameObject.TryGetComponent(out _meshRenderer))
        {
            _colorInit = _meshRenderer.material.color;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(PlatformTag) == false)
            return;

        if (_hasHit == true) 
            return;
        
        _hasHit = true;
        
        SetRandomColor();
        SetRandomLifespan();
    }

    public void Reset()
    {
        if (gameObject.TryGetComponent(out Rigidbody rb))
            rb.velocity = Vector3.zero;
        
        if(_meshRenderer != null)
            _meshRenderer.material.color = _colorInit;
        
        _hasHit = false;
    }

    private void SetRandomColor()
    {
        if (_meshRenderer != null)
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
        
        RequestRelease?.Invoke(gameObject);
    }
}
