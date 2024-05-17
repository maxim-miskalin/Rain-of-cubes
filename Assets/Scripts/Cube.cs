using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    private bool _isOn = true;
    private Color _defaultColor;

    public bool IsOn => _isOn;

    private void Awake()
    {
        _defaultColor = GetComponent<Renderer>().material.color;
    }

    public void TurnOn()
    {
        if (!_isOn)
        {
            _isOn = true;
            GetComponent<Renderer>().material.color = _defaultColor;
        }
    }

    public void TurnOff()
    {
        if (_isOn)
        {
            _isOn = false;
            GetComponent<Renderer>().material.color = Random.ColorHSV();
        }
    }
}
