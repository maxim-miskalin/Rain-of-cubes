using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer), typeof(Collider))]
public class Cube : MonoBehaviour
{
    private bool _isOn = true;
    private Color _defaultColor;
    private Renderer _renderer;
    private Collider _collider;

    public event Action<Cube> RemovedToPool;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_collider != collider)
        {
            if (_isOn)
            {
                TurnOff();
                RemovedToPool?.Invoke(this);
            }
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
