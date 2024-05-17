using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(MeshCollider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeadRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;

    private float _maxRotationEuler = 361f;
    private float _minPositionX, _maxPositionX, _positionY, _minPositionZ, _maxPositionZ;
    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _minPositionX = GetComponent<Collider>().bounds.min.x;
        _maxPositionX = GetComponent<Collider>().bounds.max.x;
        _positionY = GetComponent<Collider>().bounds.center.y - 2;
        _minPositionZ = GetComponent<Collider>().bounds.min.z;
        _maxPositionZ = GetComponent<Collider>().bounds.max.z;

        _pool = new(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0, _repeadRate);
    }

    public void DestroyCube(Cube cube, float _timeLife)
    {
        cube.TurnOff();

        if (cube.IsOn == false)
        {
            StartCoroutine(ReturnToPool(cube, _timeLife));
        }
    }

    private void GetCube()
    {
        _pool.Get();
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 position = new(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
        Vector3 rotation = new(Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler));

        cube.TurnOn();
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.Euler(rotation);

        cube.gameObject.SetActive(true);
    }

    private IEnumerator ReturnToPool(Cube cube, float _timeLife)
    {
        yield return new WaitForSeconds(_timeLife);

        _pool.Release(cube);
    }
}
