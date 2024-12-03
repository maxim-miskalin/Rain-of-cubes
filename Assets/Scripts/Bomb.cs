using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
public class Bomb : PoolableObject
{
    [SerializeField] private float _delay = 0.01f;

    private Renderer _renderer;
    private Color _defualtColor;
    private WaitForSeconds _wait;

    public event Action Exploded;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defualtColor = _renderer.material.color;
        _wait = new(_delay);
    }

    public override void ResetState()
    {
        _renderer.material.color = _defualtColor;
    }

    public void Activate()
    {
        StartCoroutine(WaitExplosion());
    }

    private IEnumerator WaitExplosion()
    {
        float timeDeactivation = Random.Range(MinLifeTime, MaxLifeTime);
        float timeLife = timeDeactivation;

        while (timeLife > 0)
        {
            SetAlfa(timeLife / timeDeactivation);
            yield return _wait;
            timeLife -= _delay;
        }

        SetAlfa(0);
        Exploded?.Invoke();
        Deactivate();
    }

    private void SetAlfa(float alfa)
    {
        Color color = _renderer.material.color;
        color.a = alfa;
        _renderer.material.color = color;
    }
}
