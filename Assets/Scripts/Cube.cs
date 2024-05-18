using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Cube : MonoBehaviour
{
    public static Action<Cube, float> RemoveToPool;

    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private bool _isOn = true;
    private Color _defaultColor;

    public bool IsOn => _isOn;

    private void Awake()
    {
        _defaultColor = GetComponent<Renderer>().material.color;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (_isOn)
        {
            float timeLife = Random.Range(_minLifeTime, _maxLifeTime);

            TurnOff();
            RemoveToPool?.Invoke(this, timeLife);
        }
    }

    public void TurnOn()
    {
        _isOn = true;
        GetComponent<Renderer>().material.color = _defaultColor;
    }

    private void TurnOff()
    {
        _isOn = false;
        GetComponent<Renderer>().material.color = Random.ColorHSV();
    }
}
