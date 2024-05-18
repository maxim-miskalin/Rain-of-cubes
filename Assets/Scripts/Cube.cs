using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public static Action<Cube, float> RemovedToPool;

    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isOn = true;
    private Color _defaultColor;
    private Renderer _renderer;

    public bool IsOn => _isOn;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_isOn)
        {
            float timeLife = Random.Range(_minLifeTime, _maxLifeTime);

            TurnOff();
            RemovedToPool?.Invoke(this, timeLife);
        }
    }

    public void TurnOn()
    {
        _isOn = true;
        _renderer.material.color = _defaultColor;
    }

    private void TurnOff()
    {
        _isOn = false;
        _renderer.material.color = Random.ColorHSV();
    }
}
