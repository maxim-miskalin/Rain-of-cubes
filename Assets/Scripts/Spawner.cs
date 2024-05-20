using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Quaternion = UnityEngine.Quaternion;

[RequireComponent(typeof(Collider))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeadRate = 1f;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 10;
    [SerializeField] private float _minLifeTimeCube = 2f;
    [SerializeField] private float _maxLifeTimeCube = 5f;

    private Collider _collider;
    private ObjectPool<Cube> _pool;
    private float _offsetY = 1f;
    private bool _isWork = true;
    private float _maxRotationEuler = 361f;

    private float _minPositionX;
    private float _maxPositionX;
    private float _positionY;
    private float _minPositionZ;
    private float _maxPositionZ;

    private void Awake()
    {
        _collider = GetComponent<Collider>();

        _minPositionX = _collider.bounds.min.x;
        _maxPositionX = _collider.bounds.max.x;
        _positionY = _collider.bounds.center.y - _offsetY;
        _minPositionZ = _collider.bounds.min.z;
        _maxPositionZ = _collider.bounds.max.z;

        _pool = new(CreateCube, ContinueAction, (cube) => cube.gameObject.SetActive(false), DestroyCube, true, _poolCapacity, _poolMaxSize);
    }

    private void Start()
    {
        StartCoroutine(GetCube());
    }

    private IEnumerator GetCube()
    {
        while (_isWork)
        {
            _pool.Get();
            yield return new WaitForSeconds(_repeadRate);
        }
    }

    private void ReturnCube(Cube cube)
    {
        StartCoroutine(ReturnToPool(cube));
    }

    private Cube CreateCube()
    {
        Cube cube = Instantiate(_prefab);
        cube.RemovedToPool += ReturnCube;
        cube.gameObject.SetActive(false);

        return cube;
    }

    private void ContinueAction(Cube cube)
    {
        Vector3 position = new(Random.Range(_minPositionX, _maxPositionX), _positionY, Random.Range(_minPositionZ, _maxPositionZ));
        Vector3 rotation = new(Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler), Random.Range(0, _maxRotationEuler));

        cube.TurnOn();
        cube.transform.position = position;
        cube.transform.rotation = Quaternion.Euler(rotation);
        cube.gameObject.SetActive(true);
    }

    private void DestroyCube(Cube cube)
    {
        cube.RemovedToPool -= ReturnCube;
        Destroy(cube.gameObject);
    }

    private IEnumerator ReturnToPool(Cube cube)
    {
        float timeLife = Random.Range(_minLifeTimeCube, _maxLifeTimeCube);
        yield return new WaitForSeconds(timeLife);
        _pool.Release(cube);
    }
}
