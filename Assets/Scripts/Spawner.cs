using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Quaternion = UnityEngine.Quaternion;

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
    private bool _isWork = true;

    private void Awake()
    {
        _minPositionX = GetComponent<Collider>().bounds.min.x;
        _maxPositionX = GetComponent<Collider>().bounds.max.x;
        _positionY = GetComponent<Collider>().bounds.center.y;
        _minPositionZ = GetComponent<Collider>().bounds.min.z;
        _maxPositionZ = GetComponent<Collider>().bounds.max.z;

        _pool = new(() => Instantiate(_prefab), ActionOnGet, (cube) => cube.gameObject.SetActive(false), Destroy, true, _poolCapacity, _poolMaxSize);
    }

    private void OnEnable()
    {
        Cube.RemoveToPool += DestroyCube;
    }

    private void OnDisable()
    {
        Cube.RemoveToPool -= DestroyCube;
    }

    private void Start()
    {
        StartCoroutine(GetCube());
    }

    public void DestroyCube(Cube cube, float _timeLife)
    {
        StartCoroutine(ReturnToPool(cube, _timeLife));
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

    private IEnumerator GetCube()
    {
        while (_isWork)
        {
            _pool.Get();
            yield return new WaitForSeconds(_repeadRate);
        }
    }

    private IEnumerator ReturnToPool(Cube cube, float _timeLife)
    {
        yield return new WaitForSeconds(_timeLife);
        _pool.Release(cube);
    }
}
