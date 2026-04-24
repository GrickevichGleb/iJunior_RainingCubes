using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class Bomb : Spawnable
{
    [SerializeField] private int _delayMin = 2;
    [SerializeField] private int _delayMax = 5;

    private Collider _collider;
    private Exploder _exploder;

    private float _explosionDelay;
    private float _countdownStart;
    
    private Color _colorInit;
    
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _exploder = GetComponent<Exploder>();
        _colorInit = _meshRenderer.material.color;
    }

    private void Start()
    {
        _explosionDelay = UtilsRandom.GetRandomNumber(_delayMin, _delayMax);
        StartCoroutine(ExplodeAfterSeconds(_explosionDelay));
    }
    
    // private void OnCollisionEnter(Collision other)
    // {
    //     if (_hasHit == true) 
    //         return;
    //     
    //     if (!other.gameObject.TryGetComponent(out Ground _))
    //         return;
    //     
    //     _hasHit = true;
    //
    // }

    public override void Reset()
    {
        _rigidbody.velocity = Vector3.zero;
        _meshRenderer.material.color = _colorInit;
        
        gameObject.SetActive(true);
    }

    private IEnumerator ExplodeAfterSeconds(float delay)
    {
        StartCoroutine(FadeAlpha(delay));
        yield return new WaitForSeconds(delay);

        _rigidbody.isKinematic = true;
        _collider.enabled = false;
        _exploder.Explode();
        
        Release();
    }

    private IEnumerator FadeAlpha(float duration)
    {
        _meshRenderer.material.ToFadeMode();
        
        Color color = _meshRenderer.material.color;
        float alpha = color.a;
        
        _countdownStart = Time.time;
        
        while (Time.time - _countdownStart < duration)
        {
            alpha -= Time.deltaTime / duration;
            
            _meshRenderer.material.color =
                new Color(
                    color.r,
                    color.g,
                    color.b,
                    alpha);
            
            yield return null;
        }
    }
}
