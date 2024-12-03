using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Bomb))]
public class Explosion : MonoBehaviour
{
    [SerializeField] private float _radius = 50;
    [SerializeField] private float _force = 300;
    
    private Bomb _bomb;

    private void Awake()
    {
        _bomb = GetComponent<Bomb>();
    }

    private void OnEnable()
    {
        _bomb.Exploded += Explode;
    }

    private void OnDisable()
    {
        _bomb.Exploded -= Explode;
    }

    private void Explode()
    {
        foreach (Rigidbody rigidbody in GetExplodableObjects())
        {
            rigidbody.AddExplosionForce(_force / transform.localScale.y, transform.position, _radius / transform.localScale.y);
        }
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius);

        List<Rigidbody> cubes = new();

        foreach (Collider hit in hits)
            if (hit.attachedRigidbody != null)
                cubes.Add(hit.attachedRigidbody);

        return cubes;
    }
}
