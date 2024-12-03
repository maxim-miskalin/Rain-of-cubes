using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(PoolableObject))]
public class ResetRigidbody : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PoolableObject _object;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _object = GetComponent<PoolableObject>();
    }

    private void OnEnable()
    {
        _object.Deactivated += StopVelocity;
    }

    private void OnDisable()
    {
        _object.Deactivated -= StopVelocity;
    }

    private void StopVelocity(PoolableObject _) => _rigidbody.velocity = Vector3.zero;
}
