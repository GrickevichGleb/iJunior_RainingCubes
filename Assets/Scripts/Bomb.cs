using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Exploder))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private int _delayMin = 2;
    [SerializeField] private int _delayMax = 5;
    
    private Rigidbody _rb;
    private Collider _collider;
    private MeshRenderer _meshRenderer;

    private Exploder _exploder;

    private float _explosionDelay;
    private float _countdownStart;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _collider = GetComponent<Collider>();
        _meshRenderer = GetComponent<MeshRenderer>();

        _exploder = GetComponent<Exploder>();
    }

    private void Start()
    {
        _explosionDelay = UtilsRandom.GetRandomNumber(_delayMin, _delayMax);
        StartCoroutine(ExplodeAfterSeconds(_explosionDelay));
    }

    private IEnumerator ExplodeAfterSeconds(float delay)
    {
        StartCoroutine(FadeAlpha(delay));
        yield return new WaitForSeconds(delay);

        _rb.isKinematic = true;
        _collider.enabled = false;
        _exploder.Explode();
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
