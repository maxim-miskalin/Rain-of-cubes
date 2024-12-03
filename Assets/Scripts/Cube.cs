using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Cube : PoolableObject
{
    private Color _defaultColor;
    private bool _isOn = false;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.material.color;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Platform _) && _isOn)
            StartCoroutine(WaitDeactivation());
    }

    public override void ResetState()
    {
        _isOn = true;
        _renderer.material.color = _defaultColor;
    }

    private IEnumerator WaitDeactivation()
    {
        _isOn = false;
        _renderer.material.color = Random.ColorHSV();
        yield return new WaitForSeconds(Random.Range(MinLifeTime, MaxLifeTime));
        Deactivate();
    }
}
