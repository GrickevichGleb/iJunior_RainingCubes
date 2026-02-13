using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour
{
    private const string PlatformTag = "Platform";

    public event Action<GameObject> RequestRelease;
    
    [SerializeField] private int _lifespanMin = 2;
    [SerializeField] private int _lifespanMax = 5;

    private bool _hasHit = false;

    public void Initialize()
    {
        if (gameObject.TryGetComponent(out Rigidbody rb))
            rb.velocity = Vector3.zero;
        
        _hasHit = false;
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

    private void SetRandomColor()
    {
        if (gameObject.TryGetComponent(out MeshRenderer meshRenderer))
            meshRenderer.material.color = UtilsRandom.GetRandomColor();
    }

    private void SetRandomLifespan()
    {
        float lifespan = UtilsRandom.GetRandomNumber(_lifespanMin, _lifespanMax);

        StartCoroutine(ReleaseAfter(lifespan));
    }

    private IEnumerator ReleaseAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        
        Debug.Log($"Requested release for {gameObject}");
        RequestRelease?.Invoke(gameObject);
    }
}
