using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.ObjectDespawned += GetBomb;
    }

    private void OnDisable()
    {
        _cubeSpawner.ObjectDespawned -= GetBomb;
    }

    private void GetBomb(Transform cube)
    {
        Bomb bomb = Spawn();
        bomb.transform.SetLocalPositionAndRotation(cube.position, cube.rotation);
        bomb.Activate();
    }
}
