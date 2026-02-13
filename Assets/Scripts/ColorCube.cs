using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCube : MonoBehaviour
{
    [SerializeField] private int _lifespanMin = 2;
    [SerializeField] private int _lifespanMax = 5;
    
    private const string PlatformTag = "Platform";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(PlatformTag))
            return;
        
        SetRandomColor();
        SetRandomLifespan();
    }

    private void SetRandomColor()
    {
        if (gameObject.TryGetComponent(out MeshRenderer meshRenderer))
        {
            meshRenderer.material.color = UtilsRandom.GetRandomColor();
        }
    }

    private void SetRandomLifespan()
    {
        float lifespan = UtilsRandom.GetRandomNumber(_lifespanMin, _lifespanMax);
        
        Debug.Log($"Lifespan: {lifespan}");
        Destroy(gameObject, lifespan);
    }
}
