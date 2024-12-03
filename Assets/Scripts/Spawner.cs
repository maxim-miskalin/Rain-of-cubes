using System;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(SpawnerInfo))]
public class Spawner<T> : MonoBehaviour where T : PoolableObject
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 20;

    private ObjectPool<T> _pool;
    private SpawnerInfo _spawnerInfo;

    public event Action<Transform> ObjectDespawned;

    protected virtual void Awake()
    {
        _spawnerInfo = GetComponent<SpawnerInfo>();
        _pool = new ObjectPool<T>(
            CreateObject,
            ActionOnGet,
            (obj) => obj.gameObject.SetActive(false),
            DestroyObject,
            true,
            _poolCapacity,
            _poolMaxSize);
    }

    private T CreateObject()
    {
        T obj = Instantiate(_prefab);
        _spawnerInfo.CountCreate();
        obj.transform.SetParent(transform);
        obj.Deactivated += ReturnObject;
        return obj;
    }

    protected virtual void ActionOnGet(T obj)
    {
        _spawnerInfo.CountSpawn();
        _spawnerInfo.CountActive(_pool.CountActive);
        obj.ResetState();
        obj.gameObject.SetActive(true);
    }

    private void DestroyObject(T obj)
    {
        obj.Deactivated -= ReturnObject;
        Destroy(obj.gameObject);
    }

    private void ReturnObject(PoolableObject obj)
    {
        ObjectDespawned?.Invoke(obj.transform);
        _spawnerInfo.CountActive(_pool.CountActive);
        _pool.Release(obj as T);
    }

    protected T Spawn()
    {
        return _pool.Get();
    }
}