using UnityEngine;


public class Platform : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerStay(Collider cube)
    {
        if (cube != _collider && cube.GetComponent<Cube>().IsOn)
        {
            float timeLife = Random.Range(_minLifeTime, _maxLifeTime);

            _spawner.DestroyCube(cube.GetComponent<Cube>(), timeLife);
        }
    }
}
