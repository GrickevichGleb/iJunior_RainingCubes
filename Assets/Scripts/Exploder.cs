using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionPower = 220f;
    [SerializeField] private float _explosionRadius = 3f;
    [SerializeField] private float _explosionUpwardsModif = 0.8f;
    
    public void Explode()
    {
        Vector3 explosionPosition = gameObject.transform.position;

        Collider[] colliders = Physics.OverlapSphere(explosionPosition, _explosionRadius);

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Rigidbody rb))
            {
                rb.AddExplosionForce(
                    _explosionPower, 
                    explosionPosition, 
                    _explosionRadius, 
                    _explosionUpwardsModif, 
                    ForceMode.Force);
            }
        }
        
        Destroy(gameObject);
    }
}
