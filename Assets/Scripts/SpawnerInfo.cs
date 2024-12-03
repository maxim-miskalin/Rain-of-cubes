using System;
using System.Collections;
using UnityEngine;

public class SpawnerInfo : MonoBehaviour
{
    private int _countCreate = 0;
    private int _countSpawn = 0;
    private int _countActive = 0;

    public event Action<int> ObjectsCreated;
    public event Action<int> ObjectsSpawned;
    public event Action<int> ObjectsActived;

    public void IncreaseCountCreate()
    {
        _countCreate++;
        ObjectsCreated?.Invoke(_countCreate);
    }

    public void IncreaseCountSpawn()
    {
        _countSpawn++;
        ObjectsSpawned?.Invoke(_countSpawn);
    }

    public void IncreaseCountActive(int count)
    {
        if (_countActive != count)
        {
            _countActive = count;
            ObjectsActived?.Invoke(_countActive);
        }
    }
}
