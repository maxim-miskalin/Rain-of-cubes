using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeadRate = 1f;
    [SerializeField] private float _offsetY = 1f;

    private Collider _collider;
    private float _minPositionX;
    private float _maxPositionX;
    private float _minPositionZ;
    private float _maxPositionZ;

    private WaitForSeconds _sleep;
    private bool _isWork = true;
    private float _maxRotationEuler = 361f;

    protected override void Awake()
    {
        _collider = GetComponent<Collider>();

        _minPositionX = _collider.bounds.min.x;
        _maxPositionX = _collider.bounds.max.x;
        _minPositionZ = _collider.bounds.min.z;
        _maxPositionZ = _collider.bounds.max.z;

        _sleep = new(_repeadRate);

        base.Awake();
    }

    private void Start()
    {
        StartCoroutine(GetObject());
    }

    private IEnumerator GetObject()
    {
        while (_isWork)
        {
            Spawn();

            yield return _sleep;
        }
    }

    protected override void ActionOnGet(Cube cube)
    {
        Vector3 position = new(Random.Range(_minPositionX, _maxPositionX), transform.position.y - _offsetY, Random.Range(_minPositionZ, _maxPositionZ));
        Vector3 rotation = new(Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler));
        cube.transform.SetPositionAndRotation(position, Quaternion.Euler(rotation));
        base.ActionOnGet(cube);
    }
}
